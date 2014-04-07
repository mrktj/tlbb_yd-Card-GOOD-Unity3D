#!/usr/bin/python
#-*- coding: utf-8 -*-

#生成Java和CSharp所需要的MessageID文件

import os
import sys
#设置初始ID值,这里必须从20002开始，server端定义login方法如果不是20002就返回错误,fuck!!!
start_index = 20002

java_class_name = "HOpCodeEx.java"
cs_class_name = "MessageID.cs"

proto_file = open("PBMessage.proto","r")
lines = proto_file.readlines()

#删除旧文件
if os.path.exists(java_class_name):
	os.remove(java_class_name)
if os.path.exists(cs_class_name):
	os.remove(cs_class_name)
	
#-------------------------临时添加PacketDistributed在这里开始-------------------------
PacketDistributed_class_name = "PacketDistributed.cs"
if os.path.exists(PacketDistributed_class_name):
	os.remove(PacketDistributed_class_name)
#-------------------------临时添加PacketDistributed在这里结束-------------------------

# 初始化CS文件
newCSharplines = []
newCSharpFile = open(cs_class_name,"wb") 
newCSharpFile.write("//Auto Generate File, Do NOT Modify!!!!!!!!!!!!!!!\n")
newCSharpFile.write("using System;\n")
newCSharpFile.write("namespace xjgame.message\n")
newCSharpFile.write("{\n")
newCSharpFile.write("\tpublic enum MessageID\n")
newCSharpFile.write("\t{\n")


#初始化JAVA文件
newJavaFile = open(java_class_name,"wb") 
newJavaFile.write("package xjgame.server;\n")
newJavaFile.write("//Auto Generate File, Do NOT Modify!!!!!!!!!!!!!!!\n")
newJavaFile.write("import cyou.mrd.io.http.HOpCode;\n")
newJavaFile.write("public class HOpCodeEx extends HOpCode {\n")
#遍历生成ID
for line in lines:
	#if line.startswith("//"):
		#newCSharpFile.write( "%s" % (line))
		#newJavaFile.write( "%s" % (line))
	if line.startswith("message"):
		text = line.split(' ')
		if text[1].find("\n") > 0:
			message_name = text[1].split("\n")
		else:
			message_name = text[1]
		newCSharpFile.write( "\t\t%s = %s,\n" % (message_name[0],start_index))
		newJavaFile.write( "\tpublic static final short %s = %s;\n" % (message_name[0],start_index))
		start_index = start_index + 1
		print message_name[0]	
		
#java文件结束
newJavaFile.write("\n}\n")
newJavaFile.close()

#c sharp文件结束
newCSharpFile.write("\n\t}\n")

#-------------------------临时添加Enum在这里开始-------------------------
inEnum = False
for line in lines:
	if line.find("enum ") > 0:
		inEnum = True
		newCSharpFile.write( "\tpublic %s" % (line))
		print line
	elif inEnum == True:
		if line.find("}") > 0:
			inEnum = False
		if line.find(";") > 0:
			line = line.split(';')
			line = "%s,\n" % (line[0])
		newCSharpFile.write( "\t%s" % (line))
		print line
#-------------------------临时添加Enum在这里结束-------------------------

#-------------------------临时添加PacketDistributed在这里开始-------------------------
#初始化
PacketDistributedFile = open(PacketDistributed_class_name,"wb") 
PacketDistributedFile.write("//Auto Generate File, Do NOT Modify!!!!!!!!!!!!!!!\n")
PacketDistributedFile.write(
'''
using System.IO;
using System;
using System.Net.Sockets;
using Google.ProtocolBuffers;
using xjgame.message; 
using card.net;

    public abstract class PacketDistributed\n
    {

        public static PacketDistributed CreatePacket(MessageID packetID)
        {
            PacketDistributed packet = null;
            switch (packetID)
            {
''')
#遍历生成case
for line in lines:
	if line.startswith("message"):
		text = line.split(' ')
		if text[1].find("\n") > 0:
			message_name = text[1].split("\n")
		else:
			message_name = text[1]
		PacketDistributedFile.write( "\t\t\tcase MessageID.%s: { packet = new %s();}break;\n" % (message_name[0],message_name[0]))
PacketDistributedFile.write(
'''
            }
            if (null != packet)
            {
                packet.packetID = packetID;
            }
            //netActionTime = DateTime.Now.ToFileTimeUtc();
            return packet;
        }
       
        public byte[] ToByteArray()
        {
            //Check must init
            if (!IsInitialized())
            {
                throw InvalidProtocolBufferException.ErrorMsg("Request data have not set");
            }
            byte[] data = new byte[SerializedSize()];
            CodedOutputStream output = CodedOutputStream.CreateInstance(data);
            WriteTo(output);
            output.CheckNoSpaceLeft();
            return data;
        }
        public PacketDistributed ParseFrom(byte[] data)
        {
            CodedInputStream input = CodedInputStream.CreateInstance(data);
            PacketDistributed inst = MergeFrom(input,this);
            input.CheckLastTagWas(0);
            return inst;
        }

        public abstract int SerializedSize();
        public abstract void WriteTo(CodedOutputStream data);
        public abstract PacketDistributed MergeFrom(CodedInputStream input,PacketDistributed _Inst);
        public abstract bool IsInitialized();

        protected MessageID packetID;
	
	 	public MessageID getMessageID()
        { 
            return packetID;
        }
    }
'''
)
PacketDistributedFile.close()
#-------------------------临时添加PacketDistributed在这里结束-------------------------

newCSharpFile.write("}\n")
newCSharpFile.close()



proto_file.close()
