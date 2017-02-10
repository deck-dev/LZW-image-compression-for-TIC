using System;
using System.Collections.Generic;
using System.Text;

namespace LZWConverter
{
    public static class TicCode
    {
        public static string lwzdemoPre = @"
local function lzwDecompress(txt)		
	function initDict()
		-- init dictionary
		local dict = {}
		
		dict[0] = '0'
		dict[1] = '1'
		dict[2] = '2'
		dict[3] = '3'
		dict[4] = '4'
		dict[5] = '5'
		dict[6] = '6'
		dict[7] = '7'
		dict[8] = '8'
		dict[9] = '9'
		dict[10] = 'a'
		dict[11] = 'b'
		dict[12] = 'c'
		dict[13] = 'd'
		dict[14] = 'e'
		dict[15] = 'f'
		
		return 15, dict
	end
	
	-- start with decompression
	local dictIndx, dict = initDict()
	
	-- process
	local out = ''
	local indxCode = 7	-- first 6 chars are image width and heigth
	local prevCode = -1
	local currCode = -1
	
	-- extract width and height of image
	local w = tonumber(string.sub(txt, 1, 3), 16)
	local h = tonumber(string.sub(txt, 4, 6), 16)
	
	prevCode = tonumber(string.sub(txt, indxCode, indxCode + 2), 16)
	indxCode = indxCode + 3
	out = out..dict[prevCode]
	
	while indxCode < #txt do
		currCode = tonumber(string.sub(txt, indxCode, indxCode + 2), 16)
		indxCode = indxCode + 3

		if currCode == 4095 then
			-- flush dictionary
			dictIndx, dict = initDict()
			prevCode = tonumber(string.sub(txt, indxCode, indxCode + 2), 16)
			indxCode = indxCode + 3
			out = out..dict[prevCode]
		elseif dict[currCode] ~= nil then
			out = out..dict[currCode]
			dictIndx = dictIndx + 1
			dict[dictIndx] = dict[prevCode]..string.sub(dict[currCode],1,1)
			prevCode = currCode
		else
			dictIndx = dictIndx + 1
			dict[dictIndx] = dict[prevCode]..string.sub(dict[prevCode],1,1)
			out = out..dict[currCode]
			prevCode = currCode
		end
	end
	
	-- found that is more efficient to manipulate table instead of strings
	local img = {}
		for i=1, string.len(out) do
			img[i] = tonumber(string.sub(out,i,i),16)
		end
	return img, w, h
end

-- compressed data, here you have to copy your processed image
";
        public static string lwzdemoimgData = "local comprImg = '{0}'";

        public static string lwzdemoPost = @"
-- image information, composed by the sequence of color indexes and the dimensions of the image
local img, w, h = lzwDecompress(comprImg)

-- used to move around the screen
local x = 0
local y = 0

-- used to get some rendering info
local memory = false
local prev = 0
local cur = 0
local maxTime = 0

local function updateScreen(xofs, yofs, w, h, alpha)
	if memory then
		-- render the image in screen by using memory mapping
        local n = 0
		for y=0, 136 do
			for x=0, 240 do
				-- if you remove bound condition you will get a tile effect
				if xofs+x >= 0 and yofs+y >= 0 and xofs+x<w and yofs+y<h then

                    local ofs=(yofs+y)* w+(xofs+x)+1
					n = img[ofs]
                    n = n ~= alpha and n or 0
				else
					n = 0
				end
                poke4(0x8000+240*y+x, n)

            end
        end
	else
		-- render the image in screen by using the pix() method
		for y=0, 136 do
			for x=0, 240 do
				-- if you remove bound condition you will get a tile effect
				if xofs+x >= 0 and yofs+y >= 0 and xofs+x<w and yofs+y<h then

                    local ofs=(yofs+y)* w+(xofs+x)+1
					local n = img[ofs]
					if n ~= alpha then pix(x, y, n) end
                  end

            end
        end

    end
end

function TIC()

    prev = time()


    cls(0)
	
	-- update image

    updateScreen(x, y, w, h, 14)
	
	-- move memory if necessary
	if memory then memcpy(0x0000, 0x4000,240*136//2) end
	
	-- move in the screen
	if btn(0) then y = y - 5 end
	if btn(1) then y = y + 5 end
	if btn(2) then x = x - 5 end
	if btn(3) then x = x + 5 end
	
	-- try to change rendering type to analyze performance
	-- seems that pix() is little more efficient :o
	if btnp(4) then
        memory = not memory
        maxTime = 0

    end
	
	-- some stats

    print(memory and 'memory' or 'normal')

    cur = time()

    maxTime = (cur-prev)>maxTime and(cur-prev) or maxTime

    print(''..maxTime,0,10)
end
";
    }
}
