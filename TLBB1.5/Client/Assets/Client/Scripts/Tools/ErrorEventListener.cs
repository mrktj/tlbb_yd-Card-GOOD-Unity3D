using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ErrorEventListener{
	private static ErrorEventListener _errorListener = new ErrorEventListener();
	private ErrorEventListener()
	{
		
	}
	
	
	public delegate void OnErrorProduce();
	private Dictionary<int,OnErrorProduce> errorHandlers = new Dictionary<int, OnErrorProduce>();
	
	
	public static void SetHandler(int errorType, OnErrorProduce handler)
	{
		_errorListener.errorHandlers[errorType] = handler;
	}
	public static void OnProduceErrorEvent(int errorType)
	{
		if(_errorListener.errorHandlers.ContainsKey(errorType) && _errorListener.errorHandlers[errorType] != null)
		{
			_errorListener.errorHandlers[errorType]();
		}
		else
		{
			Debug.LogError("The error type: " + errorType + "don't have handler");
		}
	}
	
}
