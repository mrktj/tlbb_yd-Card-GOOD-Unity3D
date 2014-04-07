#ifndef __RESUPDATEINTERFACE_H__
#define __RESUPDATEINTERFACE_H__

//#include "UpdateProcedure.h"

#ifdef WIN32
	#ifdef RESUPDATE_EXPORTS
		#define _ResupdateExport _declspec(dllexport)
		#pragma message("update lib export")
	#elif RESUPDATE_IMPORTS
		#define _ResupdateExport _declspec(dllimport)
		#pragma message("update lib import")
	#else
		#define _ResupdateExport
	#pragma message("update lib not in win32")
	#endif
#else
	#pragma message("update lib un import export")
	#define _ResupdateExport
#endif

extern "C"
{

	// 初始化更新程序
	_ResupdateExport void  InitUpdate();

	// 设置项目在本地的工作目录，与update.ru是同级目录
	_ResupdateExport void SetAppWorkDir(const char *dir);

	// 设置资源更新服务器的url，例如http://127.0.0.1:8080,参数到此即可,不要加反斜线和url参数,也可以使用http://域名:端口
	_ResupdateExport void SetResourceUrl(const char *url);

	// 设置资源服务器的根目录，例如root为mc/,设置的时候不要忘记加/
	_ResupdateExport void SetAppRoot(const char *root);

	// 设置资源格式，需要保证在资源服务器上有相应的目录，例如ios或者android
	_ResupdateExport  void SetResourceFormat(const char *format);

	// 设置安装程序中的版本号，格式为x.x.x.x
	_ResupdateExport void SetAppVersion(const char *version);

	// 获取安装程序中的版本号
	_ResupdateExport const char* GetAppVersion();
	// tick
	_ResupdateExport void UpdateTick();

	// 当前事件，程序中需要循环的捕捉事件，必要的时候作出响应
	_ResupdateExport	 int GetPushEventType();
	
	/*
	enum MESSAGETYPE			//!< 下载过程中用到的提示信息，与downloadDictionary.txt内容一一对应
	{
	MSG_INVALID = -1,     // 目前没有事件
	//		MSG_OK = 0,				//!< 确定
	//		MSG_CANCEL,				//!< 取消
	MSG_NETERROR = 10001 ,			//!< 网络连接异常
	MSG_NEWAPP,				//!< 程序更新啦
	MSG_NOSDCARD,			//!< 没有SD卡
	MSG_SPACENOTENOUGH,		//!< 内存满了
	MSG_NOTDEFFINEERROR,	//!< 未定义错误
	MSG_TOTALNUM,			//!< 无用信息 表示有多少条提示
	MSG_UPDATE_APP,    // 需要更新程序
	MSG_UPDATE_RES,      // 需要更新必要资源
	MSG_UPDATING,          //  资源更新中
	MSG_UPDATE_FINNISH, // 资源更新完成
	MSG_VERSIONERROR,		//!< 版本号错误
	MSG_DOWN_MD5_ERR, // 下载Md5.ru错误
	MSG_READ_JSON_ERR, // 无法读取json对象
	MSG_JSON_FORMAT_ERR, // 读取json的key失败
	MSG_PARSE_HTTP_ERR, // 解析http头出错
	MSG_CHECK_MD5_ERR, // check下载的MD5出错
	MSG_COPY_UPDATE_ERR, // 拷贝update.ru失败
	MSG_NOT_UPDATE,  // 不需要更新
	MSG_CANNOT_CONNECT_SERVER, // 无法连接到更新服务器,放弃连接
	MSG_RECONNECT_SERVER, // 正在尝试重连更新服务器
	MSG_UPADTE_EXIST,     // 已经有一个update程序在运行
	MSG_DOWN_LIST_ERR, // 下载之前计算需要下载的文件总数出错
	MSG_UPDATE_UN_RES, // 需要更新非必要资源
	MSG_UPDATE_ALL_RES, // 需要更新必要资源和非必要资源
	MSG_UPDATING_APP, // 正在更新程序(安桌)    
	MSG_INSTALLING_APP, // 正在安装程序
	};

	*/

	// 设置是否更新，在设置之前需要捕捉到需要更新的事件
	_ResupdateExport void SetIsUpdate(bool isupdate);

	// 获取需要更新的文件大小，应该在捕捉到需要更新的事件以后调用
	_ResupdateExport unsigned int GetDownSize();

	// 获取需要下载几个文件，应该在捕捉到需要更新的事件以后调用
	_ResupdateExport int GetTotalDownNumber();

	// 获取当前下载的是第几个文件
	_ResupdateExport int GetCurrentDownNumber();

	// 获取当前文件更新进度,百分制, 1为1%
	_ResupdateExport int GetPercent();


	// 退出资源更新程序，在堆中销毁对象
	_ResupdateExport void ExitUpdate();

};



#endif