echo on
echo  

del PBMessage.cs

rem ProtoGen\protogen.exe -i:PBMessage.proto -o:PBMessage.cs

CodeEngine.exe -i:PBMessage.proto -o:PBMessage.cs -c:csharp

protoc --java_out=.\\ PBMessage.proto

python ID_Generator.py

echo copy PBMessage class
copy /y PBMessage.cs ..\Client\Assets\Client\Scripts\NetModule\PBMessage.cs
copy /y xjgame\message\PBMessage.java ..\Server\src\xjgame\message\PBMessage.java
echo copy ID class
copy /y MessageID.cs ..\Client\Assets\Client\Scripts\NetModule\MessageID.cs
copy /y HOpCodeEx.java ..\Server\src\xjgame\server\HOpCodeEx.java
echo copy PacketDistributed
copy /y PacketDistributed.cs ..\Client\Assets\Client\Scripts\NetModule\PacketDistributed.cs
pause