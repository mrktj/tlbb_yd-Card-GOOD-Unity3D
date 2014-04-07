echo off

del /Q Public\*.*
del /S /Q /F CodeTable
 
set courcedir=".\Datas"
set pathonPath="python.exe"
mkdir Public
copy /Y %courcedir%\client\*.xlsx .\Public\
copy /Y %courcedir%\public\*.xlsx .\Public\
copy /Y %courcedir%\server\*.xlsx .\Public\

%pathonPath% DataConverter.py

PlistTableCode.exe -charp


cd ..

echo 拷贝C#代码
copy /Y  .\tabletools\CodeTable\CSharp\*.cs .\Client\Assets\Client\Scripts\Table\

echo 拷贝Tabletxt数据
copy /Y .\tabletools\Public\*.txt .\Client\Assets\Client\Resources\


cd tabletools

del /Q Public\*.*
del /S /Q /F CodeTable

copy /Y %courcedir%\public\*.xlsx .\Public\
copy /Y %courcedir%\server\*.xlsx .\Public\

%pathonPath% DataConverter.py
PlistTableCode.exe -charp -java

cd ..


echo 拷贝java代码
copy /Y  .\tabletools\CodeTable\Java\xjgame\table\*.java .\server\src\xjgame\table\

echo 拷贝GameData数据
copy /Y .\tabletools\Public\*.txt .\GameData\

cd tabletools
pause




