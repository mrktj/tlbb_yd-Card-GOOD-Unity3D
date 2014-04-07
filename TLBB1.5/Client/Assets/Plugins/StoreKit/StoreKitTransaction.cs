using System;
using System.Collections;
using System.Collections.Generic;
using Prime31;


#if UNITY_IPHONE
public class StoreKitTransaction
{
    public string productIdentifier;
	public string transactionIdentifier;
    public string base64EncodedTransactionReceipt;
    public int quantity;
	
	
	
	public static List<StoreKitTransaction> transactionsFromJson( string json )
	{
		var transactionList = new List<StoreKitTransaction>();
		
		var transactions = json.listFromJson();
		if( transactions == null )
			return transactionList;
		
		foreach( Dictionary<string,object> dict in transactions )
			transactionList.Add( transactionFromDictionary( dict ) );
		
		return transactionList;
	}
	

    public static StoreKitTransaction transactionFromJson( string json )
    {
		var dict = json.dictionaryFromJson();
		
		if( dict == null )
			return new StoreKitTransaction();
		
		return transactionFromDictionary( json.dictionaryFromJson() );
    }
	
	
    public static StoreKitTransaction transactionFromDictionary( Dictionary<string,object> dict )
    {
        var transaction = new StoreKitTransaction();
		
		if( dict.ContainsKey( "productIdentifier" ) )
        	transaction.productIdentifier = dict["productIdentifier"].ToString();

		if( dict.ContainsKey( "transactionIdentifier" ) )
        	transaction.transactionIdentifier = dict["transactionIdentifier"].ToString();
		
		if( dict.ContainsKey( "base64EncodedReceipt" ) )
        	transaction.base64EncodedTransactionReceipt = dict["base64EncodedReceipt"].ToString();
		
		if( dict.ContainsKey( "quantity" ) )
        	transaction.quantity = int.Parse( dict["quantity"].ToString() );

        return transaction;
    }
	
	
	public override string ToString()
	{
		return string.Format( "<StoreKitTransaction> ID: {0}, quantity: {1}, transactionIdentifier: {2}", productIdentifier, quantity, transactionIdentifier );
	}

}
#endif
