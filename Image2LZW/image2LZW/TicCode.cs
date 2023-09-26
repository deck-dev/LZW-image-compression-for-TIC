namespace LZWConverter
{
    public static class TicCode
    {
        #region Decompression function
        public static string lwzdemoPre = @"
local function LZWDecompress(txt)	
	function InitDict()
		-- init dictionary
		local dict={}
		
		dict[0]='0'
		dict[1]='1'
		dict[2]='2'
		dict[3]='3'
		dict[4]='4'
		dict[5]='5'
		dict[6]='6'
		dict[7]='7'
		dict[8]='8'
		dict[9]='9'
		dict[10]='a'
		dict[11]='b'
		dict[12]='c'
		dict[13]='d'
		dict[14]='e'
		dict[15]='f'
		
		return 15,dict
	end
	
	-- start with decompression
	local dictIndx,dict=InitDict()
	
	-- process
	local out=''
	local indxCode=7	-- first 6 chars are image width and height
	local prevCode=-1
	local currCode=-1
	
	-- extract width and height of image
	local w=tonumber(string.sub(txt,1,3),16)
	local h=tonumber(string.sub(txt,4,6),16)
	
	prevCode=tonumber(string.sub(txt,indxCode,indxCode+2),16)
	indxCode=indxCode+3
	out=out..dict[prevCode]
	
	while indxCode<#txt do
		currCode=tonumber(string.sub(txt,indxCode,indxCode+2),16)
		indxCode=indxCode+3

		if currCode==4095 then
			-- flush dictionary
			dictIndx,dict=InitDict()
			prevCode=tonumber(string.sub(txt,indxCode,indxCode+2),16)
			indxCode=indxCode+3
			out=out..dict[prevCode]
		elseif dict[currCode]~=nil then
			out=out..dict[currCode]
			dictIndx=dictIndx+1
			dict[dictIndx]=dict[prevCode]..string.sub(dict[currCode],1,1)
			prevCode=currCode
		else
			dictIndx=dictIndx+1
			dict[dictIndx]=dict[prevCode]..string.sub(dict[prevCode],1,1)
			out=out..dict[currCode]
			prevCode=currCode
		end
	end
	
	-- is more efficient to manipulate table instead of strings
	local img={}
		for i=1,string.len(out) do
			img[i]=tonumber(string.sub(out,i,i),16)
		end
	return img,w,h
end
";
        #endregion

        #region Compressed image
        public static string lwzdemoimgData = @"
-- example

-- compressed image data
-- here is cut for a better visualization, copy from the compressed data to get entire image
local comprImg = ""{0}""
";
        #endregion

        #region Example in tic
        public static string lwzdemoPost = @"
-- image information, composed by the sequence of color indexes and the dimensions of the image
local img,w,h=LZWDecompress(comprImg)

-- to move in the screen
local x=0
local y=0

local function RenderImage(xofs,yofs,w,h,alpha)
	-- take the portion of the image starting by xofs, yofs that fits the screen
	-- w, h are the whole dimensions of the image
	for y=0,136 do -- screen height
		for x=0,240 do -- screen width
			if xofs+x>=0 and yofs+y>=0 and xofs+x<w and yofs+y<h then
				local ofs=(yofs+y)*w+(xofs+x)+1
				local n=img[ofs]
				if n~=alpha then pix(x,y,n) end
			end
		end
	end
end

function TIC()
	-- clear the screen
	cls(0)
	
	-- update image
	RenderImage(x,y,w,h,14)
	
	-- navigate through the image
	if btn(0) then y=y-5 end
	if btn(1) then y=y+5 end
	if btn(2) then x=x-5 end
	if btn(3) then x=x+5 end
end
";
        #endregion
    }
}
