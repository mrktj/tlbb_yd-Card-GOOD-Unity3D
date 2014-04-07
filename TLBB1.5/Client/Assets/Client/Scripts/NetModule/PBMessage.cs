//This code create by CodeEngine mrd.cyou.com ,don't modify

using System;
 using scg = global::System.Collections.Generic;
 using pb = global::Google.ProtocolBuffers;
 using pbc = global::Google.ProtocolBuffers.Collections;
 #pragma warning disable

namespace xjgame.message
{

[Serializable]
public class ActivityInfo : PacketDistributed
{

public const int changeCardFlagFieldNumber = 1;
 private bool hasChangeCardFlag;
 private Int32 changeCardFlag_ = 0;
 public bool HasChangeCardFlag {
 get { return hasChangeCardFlag; }
 }
 public Int32 ChangeCardFlag {
 get { return changeCardFlag_; }
 set { SetChangeCardFlag(value); }
 }
 public void SetChangeCardFlag(Int32 value) { 
 hasChangeCardFlag = true;
 changeCardFlag_ = value;
 }

public const int worldBossFlagFieldNumber = 2;
 private bool hasWorldBossFlag;
 private Int32 worldBossFlag_ = 0;
 public bool HasWorldBossFlag {
 get { return hasWorldBossFlag; }
 }
 public Int32 WorldBossFlag {
 get { return worldBossFlag_; }
 set { SetWorldBossFlag(value); }
 }
 public void SetWorldBossFlag(Int32 value) { 
 hasWorldBossFlag = true;
 worldBossFlag_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasChangeCardFlag) {
size += pb::CodedOutputStream.ComputeInt32Size(1, ChangeCardFlag);
}
 if (HasWorldBossFlag) {
size += pb::CodedOutputStream.ComputeInt32Size(2, WorldBossFlag);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasChangeCardFlag) {
output.WriteInt32(1, ChangeCardFlag);
}
 
if (HasWorldBossFlag) {
output.WriteInt32(2, WorldBossFlag);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 ActivityInfo _inst = (ActivityInfo) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.ChangeCardFlag = input.ReadInt32();
break;
}
   case  16: {
 _inst.WorldBossFlag = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class BattleCard : PacketDistributed
{

public const int cardguidFieldNumber = 1;
 private bool hasCardguid;
 private UInt64 cardguid_ = 0;
 public bool HasCardguid {
 get { return hasCardguid; }
 }
 public UInt64 Cardguid {
 get { return cardguid_; }
 set { SetCardguid(value); }
 }
 public void SetCardguid(UInt64 value) { 
 hasCardguid = true;
 cardguid_ = value;
 }

public const int cardIDFieldNumber = 2;
 private bool hasCardID;
 private Int32 cardID_ = 0;
 public bool HasCardID {
 get { return hasCardID; }
 }
 public Int32 CardID {
 get { return cardID_; }
 set { SetCardID(value); }
 }
 public void SetCardID(Int32 value) { 
 hasCardID = true;
 cardID_ = value;
 }

public const int place_idxFieldNumber = 3;
 private bool hasPlace_idx;
 private Int32 place_idx_ = 0;
 public bool HasPlace_idx {
 get { return hasPlace_idx; }
 }
 public Int32 Place_idx {
 get { return place_idx_; }
 set { SetPlace_idx(value); }
 }
 public void SetPlace_idx(Int32 value) { 
 hasPlace_idx = true;
 place_idx_ = value;
 }

public const int stateFieldNumber = 4;
 private bool hasState;
 private Int32 state_ = 0;
 public bool HasState {
 get { return hasState; }
 }
 public Int32 State {
 get { return state_; }
 set { SetState(value); }
 }
 public void SetState(Int32 value) { 
 hasState = true;
 state_ = value;
 }

public const int isfriendFieldNumber = 5;
 private bool hasIsfriend;
 private Int32 isfriend_ = 0;
 public bool HasIsfriend {
 get { return hasIsfriend; }
 }
 public Int32 Isfriend {
 get { return isfriend_; }
 set { SetIsfriend(value); }
 }
 public void SetIsfriend(Int32 value) { 
 hasIsfriend = true;
 isfriend_ = value;
 }

public const int bagFieldNumber = 6;
 private bool hasBag;
 private DropBag bag_ =  new DropBag();
 public bool HasBag {
 get { return hasBag; }
 }
 public DropBag Bag {
 get { return bag_; }
 set { SetBag(value); }
 }
 public void SetBag(DropBag value) { 
 hasBag = true;
 bag_ = value;
 }

public const int init_hpFieldNumber = 7;
 private bool hasInit_hp;
 private Int64 init_hp_ = 0;
 public bool HasInit_hp {
 get { return hasInit_hp; }
 }
 public Int64 Init_hp {
 get { return init_hp_; }
 set { SetInit_hp(value); }
 }
 public void SetInit_hp(Int64 value) { 
 hasInit_hp = true;
 init_hp_ = value;
 }

public const int commSkillIdFieldNumber = 8;
 private bool hasCommSkillId;
 private Int32 commSkillId_ = 0;
 public bool HasCommSkillId {
 get { return hasCommSkillId; }
 }
 public Int32 CommSkillId {
 get { return commSkillId_; }
 set { SetCommSkillId(value); }
 }
 public void SetCommSkillId(Int32 value) { 
 hasCommSkillId = true;
 commSkillId_ = value;
 }

public const int volSkillIdFieldNumber = 9;
 private bool hasVolSkillId;
 private Int32 volSkillId_ = 0;
 public bool HasVolSkillId {
 get { return hasVolSkillId; }
 }
 public Int32 VolSkillId {
 get { return volSkillId_; }
 set { SetVolSkillId(value); }
 }
 public void SetVolSkillId(Int32 value) { 
 hasVolSkillId = true;
 volSkillId_ = value;
 }

public const int combSkillIdFieldNumber = 10;
 private bool hasCombSkillId;
 private Int32 combSkillId_ = 0;
 public bool HasCombSkillId {
 get { return hasCombSkillId; }
 }
 public Int32 CombSkillId {
 get { return combSkillId_; }
 set { SetCombSkillId(value); }
 }
 public void SetCombSkillId(Int32 value) { 
 hasCombSkillId = true;
 combSkillId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasCardguid) {
size += pb::CodedOutputStream.ComputeUInt64Size(1, Cardguid);
}
 if (HasCardID) {
size += pb::CodedOutputStream.ComputeInt32Size(2, CardID);
}
 if (HasPlace_idx) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Place_idx);
}
 if (HasState) {
size += pb::CodedOutputStream.ComputeInt32Size(4, State);
}
 if (HasIsfriend) {
size += pb::CodedOutputStream.ComputeInt32Size(5, Isfriend);
}
{
int subsize = Bag.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)6) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 if (HasInit_hp) {
size += pb::CodedOutputStream.ComputeInt64Size(7, Init_hp);
}
 if (HasCommSkillId) {
size += pb::CodedOutputStream.ComputeInt32Size(8, CommSkillId);
}
 if (HasVolSkillId) {
size += pb::CodedOutputStream.ComputeInt32Size(9, VolSkillId);
}
 if (HasCombSkillId) {
size += pb::CodedOutputStream.ComputeInt32Size(10, CombSkillId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasCardguid) {
output.WriteUInt64(1, Cardguid);
}
 
if (HasCardID) {
output.WriteInt32(2, CardID);
}
 
if (HasPlace_idx) {
output.WriteInt32(3, Place_idx);
}
 
if (HasState) {
output.WriteInt32(4, State);
}
 
if (HasIsfriend) {
output.WriteInt32(5, Isfriend);
}
{
output.WriteTag((int)6, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)Bag.SerializedSize());
Bag.WriteTo(output);

}
 
if (HasInit_hp) {
output.WriteInt64(7, Init_hp);
}
 
if (HasCommSkillId) {
output.WriteInt32(8, CommSkillId);
}
 
if (HasVolSkillId) {
output.WriteInt32(9, VolSkillId);
}
 
if (HasCombSkillId) {
output.WriteInt32(10, CombSkillId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 BattleCard _inst = (BattleCard) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Cardguid = input.ReadUInt64();
break;
}
   case  16: {
 _inst.CardID = input.ReadInt32();
break;
}
   case  24: {
 _inst.Place_idx = input.ReadInt32();
break;
}
   case  32: {
 _inst.State = input.ReadInt32();
break;
}
   case  40: {
 _inst.Isfriend = input.ReadInt32();
break;
}
    case  50: {
DropBag subBuilder =  new DropBag();
 input.ReadMessage(subBuilder);
_inst.Bag = subBuilder;
break;
}
   case  56: {
 _inst.Init_hp = input.ReadInt64();
break;
}
   case  64: {
 _inst.CommSkillId = input.ReadInt32();
break;
}
   case  72: {
 _inst.VolSkillId = input.ReadInt32();
break;
}
   case  80: {
 _inst.CombSkillId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasCardguid) return false;
 if (!hasState) return false;
  if (HasBag) {
if (!Bag.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class ButtonInfo : PacketDistributed
{

public const int idFieldNumber = 1;
 private bool hasId;
 private Int32 id_ = 0;
 public bool HasId {
 get { return hasId; }
 }
 public Int32 Id {
 get { return id_; }
 set { SetId(value); }
 }
 public void SetId(Int32 value) { 
 hasId = true;
 id_ = value;
 }

public const int valueFieldNumber = 2;
 private bool hasValue;
 private Int32 value_ = 0;
 public bool HasValue {
 get { return hasValue; }
 }
 public Int32 Value {
 get { return value_; }
 set { SetValue(value); }
 }
 public void SetValue(Int32 value) { 
 hasValue = true;
 value_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Id);
}
 if (HasValue) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Value);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasId) {
output.WriteInt32(1, Id);
}
 
if (HasValue) {
output.WriteInt32(2, Value);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 ButtonInfo _inst = (ButtonInfo) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Id = input.ReadInt32();
break;
}
   case  16: {
 _inst.Value = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasId) return false;
 if (!hasValue) return false;
 return true;
 }

	}


[Serializable]
public class CS20038 : PacketDistributed
{

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CS20038 _inst = (CS20038) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
 
 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class CS20039 : PacketDistributed
{

public const int orderIdFieldNumber = 1;
 private bool hasOrderId;
 private string orderId_ = "";
 public bool HasOrderId {
 get { return hasOrderId; }
 }
 public string OrderId {
 get { return orderId_; }
 set { SetOrderId(value); }
 }
 public void SetOrderId(string value) { 
 hasOrderId = true;
 orderId_ = value;
 }

public const int goodsIdFieldNumber = 2;
 private bool hasGoodsId;
 private Int32 goodsId_ = 0;
 public bool HasGoodsId {
 get { return hasGoodsId; }
 }
 public Int32 GoodsId {
 get { return goodsId_; }
 set { SetGoodsId(value); }
 }
 public void SetGoodsId(Int32 value) { 
 hasGoodsId = true;
 goodsId_ = value;
 }

public const int waresIdFieldNumber = 3;
 private bool hasWaresId;
 private string waresId_ = "";
 public bool HasWaresId {
 get { return hasWaresId; }
 }
 public string WaresId {
 get { return waresId_; }
 set { SetWaresId(value); }
 }
 public void SetWaresId(string value) { 
 hasWaresId = true;
 waresId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasOrderId) {
size += pb::CodedOutputStream.ComputeStringSize(1, OrderId);
}
 if (HasGoodsId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, GoodsId);
}
 if (HasWaresId) {
size += pb::CodedOutputStream.ComputeStringSize(3, WaresId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasOrderId) {
output.WriteString(1, OrderId);
}
 
if (HasGoodsId) {
output.WriteInt32(2, GoodsId);
}
 
if (HasWaresId) {
output.WriteString(3, WaresId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CS20039 _inst = (CS20039) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  10: {
 _inst.OrderId = input.ReadString();
break;
}
   case  16: {
 _inst.GoodsId = input.ReadInt32();
break;
}
   case  26: {
 _inst.WaresId = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasOrderId) return false;
 if (!hasGoodsId) return false;
 if (!hasWaresId) return false;
 return true;
 }

	}


[Serializable]
public class CS20040 : PacketDistributed
{

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CS20040 _inst = (CS20040) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
 
 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class CS20041 : PacketDistributed
{

public const int goodsIdFieldNumber = 1;
 private bool hasGoodsId;
 private Int32 goodsId_ = 0;
 public bool HasGoodsId {
 get { return hasGoodsId; }
 }
 public Int32 GoodsId {
 get { return goodsId_; }
 set { SetGoodsId(value); }
 }
 public void SetGoodsId(Int32 value) { 
 hasGoodsId = true;
 goodsId_ = value;
 }

public const int productIdFieldNumber = 2;
 private bool hasProductId;
 private string productId_ = "";
 public bool HasProductId {
 get { return hasProductId; }
 }
 public string ProductId {
 get { return productId_; }
 set { SetProductId(value); }
 }
 public void SetProductId(string value) { 
 hasProductId = true;
 productId_ = value;
 }

public const int transactionIdFieldNumber = 3;
 private bool hasTransactionId;
 private string transactionId_ = "";
 public bool HasTransactionId {
 get { return hasTransactionId; }
 }
 public string TransactionId {
 get { return transactionId_; }
 set { SetTransactionId(value); }
 }
 public void SetTransactionId(string value) { 
 hasTransactionId = true;
 transactionId_ = value;
 }

public const int receiptFieldNumber = 4;
 private bool hasReceipt;
 private string receipt_ = "";
 public bool HasReceipt {
 get { return hasReceipt; }
 }
 public string Receipt {
 get { return receipt_; }
 set { SetReceipt(value); }
 }
 public void SetReceipt(string value) { 
 hasReceipt = true;
 receipt_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasGoodsId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, GoodsId);
}
 if (HasProductId) {
size += pb::CodedOutputStream.ComputeStringSize(2, ProductId);
}
 if (HasTransactionId) {
size += pb::CodedOutputStream.ComputeStringSize(3, TransactionId);
}
 if (HasReceipt) {
size += pb::CodedOutputStream.ComputeStringSize(4, Receipt);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasGoodsId) {
output.WriteInt32(1, GoodsId);
}
 
if (HasProductId) {
output.WriteString(2, ProductId);
}
 
if (HasTransactionId) {
output.WriteString(3, TransactionId);
}
 
if (HasReceipt) {
output.WriteString(4, Receipt);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CS20041 _inst = (CS20041) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.GoodsId = input.ReadInt32();
break;
}
   case  18: {
 _inst.ProductId = input.ReadString();
break;
}
   case  26: {
 _inst.TransactionId = input.ReadString();
break;
}
   case  34: {
 _inst.Receipt = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasGoodsId) return false;
 if (!hasProductId) return false;
 if (!hasTransactionId) return false;
 if (!hasReceipt) return false;
 return true;
 }

	}


[Serializable]
public class CSADDFriend : PacketDistributed
{

public const int friend_guidFieldNumber = 1;
 private bool hasFriend_guid;
 private Int64 friend_guid_ = 0;
 public bool HasFriend_guid {
 get { return hasFriend_guid; }
 }
 public Int64 Friend_guid {
 get { return friend_guid_; }
 set { SetFriend_guid(value); }
 }
 public void SetFriend_guid(Int64 value) { 
 hasFriend_guid = true;
 friend_guid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasFriend_guid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Friend_guid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasFriend_guid) {
output.WriteInt64(1, Friend_guid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSADDFriend _inst = (CSADDFriend) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Friend_guid = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasFriend_guid) return false;
 return true;
 }

	}


[Serializable]
public class CSAskActivity : PacketDistributed
{

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSAskActivity _inst = (CSAskActivity) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
 
 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class CSAskChangeCardList : PacketDistributed
{

public const int playerGuidFieldNumber = 1;
 private bool hasPlayerGuid;
 private Int64 playerGuid_ = 0;
 public bool HasPlayerGuid {
 get { return hasPlayerGuid; }
 }
 public Int64 PlayerGuid {
 get { return playerGuid_; }
 set { SetPlayerGuid(value); }
 }
 public void SetPlayerGuid(Int64 value) { 
 hasPlayerGuid = true;
 playerGuid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasPlayerGuid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, PlayerGuid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasPlayerGuid) {
output.WriteInt64(1, PlayerGuid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSAskChangeCardList _inst = (CSAskChangeCardList) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.PlayerGuid = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasPlayerGuid) return false;
 return true;
 }

	}


[Serializable]
public class CSAskPVPList : PacketDistributed
{

public const int heroGuidFieldNumber = 1;
 private bool hasHeroGuid;
 private Int32 heroGuid_ = 0;
 public bool HasHeroGuid {
 get { return hasHeroGuid; }
 }
 public Int32 HeroGuid {
 get { return heroGuid_; }
 set { SetHeroGuid(value); }
 }
 public void SetHeroGuid(Int32 value) { 
 hasHeroGuid = true;
 heroGuid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasHeroGuid) {
size += pb::CodedOutputStream.ComputeInt32Size(1, HeroGuid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasHeroGuid) {
output.WriteInt32(1, HeroGuid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSAskPVPList _inst = (CSAskPVPList) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.HeroGuid = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasHeroGuid) return false;
 return true;
 }

	}


[Serializable]
public class CSAskScoreShopFresh : PacketDistributed
{

public const int guidFieldNumber = 1;
 private bool hasGuid;
 private Int32 guid_ = 0;
 public bool HasGuid {
 get { return hasGuid; }
 }
 public Int32 Guid {
 get { return guid_; }
 set { SetGuid(value); }
 }
 public void SetGuid(Int32 value) { 
 hasGuid = true;
 guid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasGuid) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Guid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasGuid) {
output.WriteInt32(1, Guid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSAskScoreShopFresh _inst = (CSAskScoreShopFresh) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Guid = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasGuid) return false;
 return true;
 }

	}


[Serializable]
public class CSAskUserData : PacketDistributed
{

public const int isNewUserFieldNumber = 1;
 private bool hasIsNewUser;
 private Int32 isNewUser_ = 0;
 public bool HasIsNewUser {
 get { return hasIsNewUser; }
 }
 public Int32 IsNewUser {
 get { return isNewUser_; }
 set { SetIsNewUser(value); }
 }
 public void SetIsNewUser(Int32 value) { 
 hasIsNewUser = true;
 isNewUser_ = value;
 }

public const int userNameFieldNumber = 2;
 private bool hasUserName;
 private string userName_ = "";
 public bool HasUserName {
 get { return hasUserName; }
 }
 public string UserName {
 get { return userName_; }
 set { SetUserName(value); }
 }
 public void SetUserName(string value) { 
 hasUserName = true;
 userName_ = value;
 }

public const int cardTempletIdFieldNumber = 3;
 private bool hasCardTempletId;
 private Int32 cardTempletId_ = 0;
 public bool HasCardTempletId {
 get { return hasCardTempletId; }
 }
 public Int32 CardTempletId {
 get { return cardTempletId_; }
 set { SetCardTempletId(value); }
 }
 public void SetCardTempletId(Int32 value) { 
 hasCardTempletId = true;
 cardTempletId_ = value;
 }

public const int finishehGuideStepFieldNumber = 4;
 private bool hasFinishehGuideStep;
 private Int32 finishehGuideStep_ = 0;
 public bool HasFinishehGuideStep {
 get { return hasFinishehGuideStep; }
 }
 public Int32 FinishehGuideStep {
 get { return finishehGuideStep_; }
 set { SetFinishehGuideStep(value); }
 }
 public void SetFinishehGuideStep(Int32 value) { 
 hasFinishehGuideStep = true;
 finishehGuideStep_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasIsNewUser) {
size += pb::CodedOutputStream.ComputeInt32Size(1, IsNewUser);
}
 if (HasUserName) {
size += pb::CodedOutputStream.ComputeStringSize(2, UserName);
}
 if (HasCardTempletId) {
size += pb::CodedOutputStream.ComputeInt32Size(3, CardTempletId);
}
 if (HasFinishehGuideStep) {
size += pb::CodedOutputStream.ComputeInt32Size(4, FinishehGuideStep);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasIsNewUser) {
output.WriteInt32(1, IsNewUser);
}
 
if (HasUserName) {
output.WriteString(2, UserName);
}
 
if (HasCardTempletId) {
output.WriteInt32(3, CardTempletId);
}
 
if (HasFinishehGuideStep) {
output.WriteInt32(4, FinishehGuideStep);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSAskUserData _inst = (CSAskUserData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.IsNewUser = input.ReadInt32();
break;
}
   case  18: {
 _inst.UserName = input.ReadString();
break;
}
   case  24: {
 _inst.CardTempletId = input.ReadInt32();
break;
}
   case  32: {
 _inst.FinishehGuideStep = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasIsNewUser) return false;
 return true;
 }

	}


[Serializable]
public class CSAskWorldBossBattle : PacketDistributed
{

public const int bossidFieldNumber = 1;
 private bool hasBossid;
 private Int32 bossid_ = 0;
 public bool HasBossid {
 get { return hasBossid; }
 }
 public Int32 Bossid {
 get { return bossid_; }
 set { SetBossid(value); }
 }
 public void SetBossid(Int32 value) { 
 hasBossid = true;
 bossid_ = value;
 }

public const int roundFieldNumber = 2;
 private bool hasRound;
 private Int32 round_ = 0;
 public bool HasRound {
 get { return hasRound; }
 }
 public Int32 Round {
 get { return round_; }
 set { SetRound(value); }
 }
 public void SetRound(Int32 value) { 
 hasRound = true;
 round_ = value;
 }

public const int intoFieldNumber = 3;
 private pbc::PopsicleList<BattleCard> into_ = new pbc::PopsicleList<BattleCard>();
 public scg::IList<BattleCard> intoList {
 get { return pbc::Lists.AsReadOnly(into_); }
 }
 
 public int intoCount {
 get { return into_.Count; }
 }
 
public BattleCard GetInto(int index) {
 return into_[index];
 }
 public void AddInto(BattleCard value) {
 into_.Add(value);
 }

public const int zhufuTimesFieldNumber = 4;
 private bool hasZhufuTimes;
 private Int32 zhufuTimes_ = 0;
 public bool HasZhufuTimes {
 get { return hasZhufuTimes; }
 }
 public Int32 ZhufuTimes {
 get { return zhufuTimes_; }
 set { SetZhufuTimes(value); }
 }
 public void SetZhufuTimes(Int32 value) { 
 hasZhufuTimes = true;
 zhufuTimes_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasBossid) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Bossid);
}
 if (HasRound) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Round);
}
{
foreach (BattleCard element in intoList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 if (HasZhufuTimes) {
size += pb::CodedOutputStream.ComputeInt32Size(4, ZhufuTimes);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasBossid) {
output.WriteInt32(1, Bossid);
}
 
if (HasRound) {
output.WriteInt32(2, Round);
}

do{
foreach (BattleCard element in intoList) {
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 
if (HasZhufuTimes) {
output.WriteInt32(4, ZhufuTimes);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSAskWorldBossBattle _inst = (CSAskWorldBossBattle) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Bossid = input.ReadInt32();
break;
}
   case  16: {
 _inst.Round = input.ReadInt32();
break;
}
    case  26: {
BattleCard subBuilder =  new BattleCard();
input.ReadMessage(subBuilder);
_inst.AddInto(subBuilder);
break;
}
   case  32: {
 _inst.ZhufuTimes = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasBossid) return false;
 if (!hasRound) return false;
foreach (BattleCard element in intoList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class CSAskWorldBossInfo : PacketDistributed
{

public const int guidFieldNumber = 1;
 private bool hasGuid;
 private Int64 guid_ = 0;
 public bool HasGuid {
 get { return hasGuid; }
 }
 public Int64 Guid {
 get { return guid_; }
 set { SetGuid(value); }
 }
 public void SetGuid(Int64 value) { 
 hasGuid = true;
 guid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasGuid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Guid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasGuid) {
output.WriteInt64(1, Guid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSAskWorldBossInfo _inst = (CSAskWorldBossInfo) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Guid = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasGuid) return false;
 return true;
 }

	}


[Serializable]
public class CSBGZ : PacketDistributed
{

public const int player_guidFieldNumber = 1;
 private bool hasPlayer_guid;
 private Int32 player_guid_ = 0;
 public bool HasPlayer_guid {
 get { return hasPlayer_guid; }
 }
 public Int32 Player_guid {
 get { return player_guid_; }
 set { SetPlayer_guid(value); }
 }
 public void SetPlayer_guid(Int32 value) { 
 hasPlayer_guid = true;
 player_guid_ = value;
 }

public const int timesFieldNumber = 2;
 private bool hasTimes;
 private Int32 times_ = 0;
 public bool HasTimes {
 get { return hasTimes; }
 }
 public Int32 Times {
 get { return times_; }
 set { SetTimes(value); }
 }
 public void SetTimes(Int32 value) { 
 hasTimes = true;
 times_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasPlayer_guid) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Player_guid);
}
 if (HasTimes) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Times);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasPlayer_guid) {
output.WriteInt32(1, Player_guid);
}
 
if (HasTimes) {
output.WriteInt32(2, Times);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSBGZ _inst = (CSBGZ) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Player_guid = input.ReadInt32();
break;
}
   case  16: {
 _inst.Times = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasPlayer_guid) return false;
 if (!hasTimes) return false;
 return true;
 }

	}


[Serializable]
public class CSBattleData : PacketDistributed
{

public const int missionIDFieldNumber = 1;
 private bool hasMissionID;
 private Int64 missionID_ = 0;
 public bool HasMissionID {
 get { return hasMissionID; }
 }
 public Int64 MissionID {
 get { return missionID_; }
 set { SetMissionID(value); }
 }
 public void SetMissionID(Int64 value) { 
 hasMissionID = true;
 missionID_ = value;
 }

public const int roundFieldNumber = 2;
 private bool hasRound;
 private Int32 round_ = 0;
 public bool HasRound {
 get { return hasRound; }
 }
 public Int32 Round {
 get { return round_; }
 set { SetRound(value); }
 }
 public void SetRound(Int32 value) { 
 hasRound = true;
 round_ = value;
 }

public const int friendguidFieldNumber = 3;
 private bool hasFriendguid;
 private UInt64 friendguid_ = 0;
 public bool HasFriendguid {
 get { return hasFriendguid; }
 }
 public UInt64 Friendguid {
 get { return friendguid_; }
 set { SetFriendguid(value); }
 }
 public void SetFriendguid(UInt64 value) { 
 hasFriendguid = true;
 friendguid_ = value;
 }

public const int infoFieldNumber = 4;
 private pbc::PopsicleList<BattleCard> info_ = new pbc::PopsicleList<BattleCard>();
 public scg::IList<BattleCard> infoList {
 get { return pbc::Lists.AsReadOnly(info_); }
 }
 
 public int infoCount {
 get { return info_.Count; }
 }
 
public BattleCard GetInfo(int index) {
 return info_[index];
 }
 public void AddInfo(BattleCard value) {
 info_.Add(value);
 }

public const int cost_moneyFieldNumber = 5;
 private bool hasCost_money;
 private Int32 cost_money_ = 0;
 public bool HasCost_money {
 get { return hasCost_money; }
 }
 public Int32 Cost_money {
 get { return cost_money_; }
 set { SetCost_money(value); }
 }
 public void SetCost_money(Int32 value) { 
 hasCost_money = true;
 cost_money_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasMissionID) {
size += pb::CodedOutputStream.ComputeInt64Size(1, MissionID);
}
 if (HasRound) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Round);
}
 if (HasFriendguid) {
size += pb::CodedOutputStream.ComputeUInt64Size(3, Friendguid);
}
{
foreach (BattleCard element in infoList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)4) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 if (HasCost_money) {
size += pb::CodedOutputStream.ComputeInt32Size(5, Cost_money);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasMissionID) {
output.WriteInt64(1, MissionID);
}
 
if (HasRound) {
output.WriteInt32(2, Round);
}
 
if (HasFriendguid) {
output.WriteUInt64(3, Friendguid);
}

do{
foreach (BattleCard element in infoList) {
output.WriteTag((int)4, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 
if (HasCost_money) {
output.WriteInt32(5, Cost_money);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSBattleData _inst = (CSBattleData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.MissionID = input.ReadInt64();
break;
}
   case  16: {
 _inst.Round = input.ReadInt32();
break;
}
   case  24: {
 _inst.Friendguid = input.ReadUInt64();
break;
}
    case  34: {
BattleCard subBuilder =  new BattleCard();
input.ReadMessage(subBuilder);
_inst.AddInfo(subBuilder);
break;
}
   case  40: {
 _inst.Cost_money = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasMissionID) return false;
 if (!hasRound) return false;
 if (!hasFriendguid) return false;
foreach (BattleCard element in infoList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class CSBindAccount : PacketDistributed
{

public const int accountidFieldNumber = 1;
 private bool hasAccountid;
 private Int32 accountid_ = 0;
 public bool HasAccountid {
 get { return hasAccountid; }
 }
 public Int32 Accountid {
 get { return accountid_; }
 set { SetAccountid(value); }
 }
 public void SetAccountid(Int32 value) { 
 hasAccountid = true;
 accountid_ = value;
 }

public const int usernameFieldNumber = 2;
 private bool hasUsername;
 private string username_ = "";
 public bool HasUsername {
 get { return hasUsername; }
 }
 public string Username {
 get { return username_; }
 set { SetUsername(value); }
 }
 public void SetUsername(string value) { 
 hasUsername = true;
 username_ = value;
 }

public const int passwordFieldNumber = 3;
 private bool hasPassword;
 private string password_ = "";
 public bool HasPassword {
 get { return hasPassword; }
 }
 public string Password {
 get { return password_; }
 set { SetPassword(value); }
 }
 public void SetPassword(string value) { 
 hasPassword = true;
 password_ = value;
 }

public const int typeFieldNumber = 4;
 private bool hasType;
 private Int32 type_ = 0;
 public bool HasType {
 get { return hasType; }
 }
 public Int32 Type {
 get { return type_; }
 set { SetType(value); }
 }
 public void SetType(Int32 value) { 
 hasType = true;
 type_ = value;
 }

public const int deviceSystemFieldNumber = 5;
 private bool hasDeviceSystem;
 private string deviceSystem_ = "";
 public bool HasDeviceSystem {
 get { return hasDeviceSystem; }
 }
 public string DeviceSystem {
 get { return deviceSystem_; }
 set { SetDeviceSystem(value); }
 }
 public void SetDeviceSystem(string value) { 
 hasDeviceSystem = true;
 deviceSystem_ = value;
 }

public const int downloadTypeFieldNumber = 6;
 private bool hasDownloadType;
 private string downloadType_ = "";
 public bool HasDownloadType {
 get { return hasDownloadType; }
 }
 public string DownloadType {
 get { return downloadType_; }
 set { SetDownloadType(value); }
 }
 public void SetDownloadType(string value) { 
 hasDownloadType = true;
 downloadType_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasAccountid) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Accountid);
}
 if (HasUsername) {
size += pb::CodedOutputStream.ComputeStringSize(2, Username);
}
 if (HasPassword) {
size += pb::CodedOutputStream.ComputeStringSize(3, Password);
}
 if (HasType) {
size += pb::CodedOutputStream.ComputeInt32Size(4, Type);
}
 if (HasDeviceSystem) {
size += pb::CodedOutputStream.ComputeStringSize(5, DeviceSystem);
}
 if (HasDownloadType) {
size += pb::CodedOutputStream.ComputeStringSize(6, DownloadType);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasAccountid) {
output.WriteInt32(1, Accountid);
}
 
if (HasUsername) {
output.WriteString(2, Username);
}
 
if (HasPassword) {
output.WriteString(3, Password);
}
 
if (HasType) {
output.WriteInt32(4, Type);
}
 
if (HasDeviceSystem) {
output.WriteString(5, DeviceSystem);
}
 
if (HasDownloadType) {
output.WriteString(6, DownloadType);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSBindAccount _inst = (CSBindAccount) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Accountid = input.ReadInt32();
break;
}
   case  18: {
 _inst.Username = input.ReadString();
break;
}
   case  26: {
 _inst.Password = input.ReadString();
break;
}
   case  32: {
 _inst.Type = input.ReadInt32();
break;
}
   case  42: {
 _inst.DeviceSystem = input.ReadString();
break;
}
   case  50: {
 _inst.DownloadType = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasAccountid) return false;
 if (!hasUsername) return false;
 if (!hasPassword) return false;
 if (!hasType) return false;
 if (!hasDeviceSystem) return false;
 if (!hasDownloadType) return false;
 return true;
 }

	}


[Serializable]
public class CSBuyMoney : PacketDistributed
{

public const int player_guidFieldNumber = 1;
 private bool hasPlayer_guid;
 private Int32 player_guid_ = 0;
 public bool HasPlayer_guid {
 get { return hasPlayer_guid; }
 }
 public Int32 Player_guid {
 get { return player_guid_; }
 set { SetPlayer_guid(value); }
 }
 public void SetPlayer_guid(Int32 value) { 
 hasPlayer_guid = true;
 player_guid_ = value;
 }

public const int timesFieldNumber = 2;
 private bool hasTimes;
 private Int32 times_ = 0;
 public bool HasTimes {
 get { return hasTimes; }
 }
 public Int32 Times {
 get { return times_; }
 set { SetTimes(value); }
 }
 public void SetTimes(Int32 value) { 
 hasTimes = true;
 times_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasPlayer_guid) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Player_guid);
}
 if (HasTimes) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Times);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasPlayer_guid) {
output.WriteInt32(1, Player_guid);
}
 
if (HasTimes) {
output.WriteInt32(2, Times);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSBuyMoney _inst = (CSBuyMoney) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Player_guid = input.ReadInt32();
break;
}
   case  16: {
 _inst.Times = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasPlayer_guid) return false;
 if (!hasTimes) return false;
 return true;
 }

	}


[Serializable]
public class CSBuyPower : PacketDistributed
{

public const int player_guidFieldNumber = 1;
 private bool hasPlayer_guid;
 private Int32 player_guid_ = 0;
 public bool HasPlayer_guid {
 get { return hasPlayer_guid; }
 }
 public Int32 Player_guid {
 get { return player_guid_; }
 set { SetPlayer_guid(value); }
 }
 public void SetPlayer_guid(Int32 value) { 
 hasPlayer_guid = true;
 player_guid_ = value;
 }

public const int timesFieldNumber = 2;
 private bool hasTimes;
 private Int32 times_ = 0;
 public bool HasTimes {
 get { return hasTimes; }
 }
 public Int32 Times {
 get { return times_; }
 set { SetTimes(value); }
 }
 public void SetTimes(Int32 value) { 
 hasTimes = true;
 times_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasPlayer_guid) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Player_guid);
}
 if (HasTimes) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Times);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasPlayer_guid) {
output.WriteInt32(1, Player_guid);
}
 
if (HasTimes) {
output.WriteInt32(2, Times);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSBuyPower _inst = (CSBuyPower) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Player_guid = input.ReadInt32();
break;
}
   case  16: {
 _inst.Times = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasPlayer_guid) return false;
 if (!hasTimes) return false;
 return true;
 }

	}


[Serializable]
public class CSCYouPayVerifyCharge : PacketDistributed
{

public const int orderIdFieldNumber = 1;
 private bool hasOrderId;
 private string orderId_ = "";
 public bool HasOrderId {
 get { return hasOrderId; }
 }
 public string OrderId {
 get { return orderId_; }
 set { SetOrderId(value); }
 }
 public void SetOrderId(string value) { 
 hasOrderId = true;
 orderId_ = value;
 }

public const int goodsIdFieldNumber = 2;
 private bool hasGoodsId;
 private Int32 goodsId_ = 0;
 public bool HasGoodsId {
 get { return hasGoodsId; }
 }
 public Int32 GoodsId {
 get { return goodsId_; }
 set { SetGoodsId(value); }
 }
 public void SetGoodsId(Int32 value) { 
 hasGoodsId = true;
 goodsId_ = value;
 }

public const int goodPriceFieldNumber = 3;
 private bool hasGoodPrice;
 private Int32 goodPrice_ = 0;
 public bool HasGoodPrice {
 get { return hasGoodPrice; }
 }
 public Int32 GoodPrice {
 get { return goodPrice_; }
 set { SetGoodPrice(value); }
 }
 public void SetGoodPrice(Int32 value) { 
 hasGoodPrice = true;
 goodPrice_ = value;
 }

public const int productIdFieldNumber = 4;
 private bool hasProductId;
 private string productId_ = "";
 public bool HasProductId {
 get { return hasProductId; }
 }
 public string ProductId {
 get { return productId_; }
 set { SetProductId(value); }
 }
 public void SetProductId(string value) { 
 hasProductId = true;
 productId_ = value;
 }

public const int channelIdFieldNumber = 5;
 private bool hasChannelId;
 private Int32 channelId_ = 0;
 public bool HasChannelId {
 get { return hasChannelId; }
 }
 public Int32 ChannelId {
 get { return channelId_; }
 set { SetChannelId(value); }
 }
 public void SetChannelId(Int32 value) { 
 hasChannelId = true;
 channelId_ = value;
 }

public const int payWayFieldNumber = 6;
 private bool hasPayWay;
 private string payWay_ = "";
 public bool HasPayWay {
 get { return hasPayWay; }
 }
 public string PayWay {
 get { return payWay_; }
 set { SetPayWay(value); }
 }
 public void SetPayWay(string value) { 
 hasPayWay = true;
 payWay_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasOrderId) {
size += pb::CodedOutputStream.ComputeStringSize(1, OrderId);
}
 if (HasGoodsId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, GoodsId);
}
 if (HasGoodPrice) {
size += pb::CodedOutputStream.ComputeInt32Size(3, GoodPrice);
}
 if (HasProductId) {
size += pb::CodedOutputStream.ComputeStringSize(4, ProductId);
}
 if (HasChannelId) {
size += pb::CodedOutputStream.ComputeInt32Size(5, ChannelId);
}
 if (HasPayWay) {
size += pb::CodedOutputStream.ComputeStringSize(6, PayWay);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasOrderId) {
output.WriteString(1, OrderId);
}
 
if (HasGoodsId) {
output.WriteInt32(2, GoodsId);
}
 
if (HasGoodPrice) {
output.WriteInt32(3, GoodPrice);
}
 
if (HasProductId) {
output.WriteString(4, ProductId);
}
 
if (HasChannelId) {
output.WriteInt32(5, ChannelId);
}
 
if (HasPayWay) {
output.WriteString(6, PayWay);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSCYouPayVerifyCharge _inst = (CSCYouPayVerifyCharge) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  10: {
 _inst.OrderId = input.ReadString();
break;
}
   case  16: {
 _inst.GoodsId = input.ReadInt32();
break;
}
   case  24: {
 _inst.GoodPrice = input.ReadInt32();
break;
}
   case  34: {
 _inst.ProductId = input.ReadString();
break;
}
   case  40: {
 _inst.ChannelId = input.ReadInt32();
break;
}
   case  50: {
 _inst.PayWay = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class CSCYouProductList : PacketDistributed
{

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSCYouProductList _inst = (CSCYouProductList) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
 
 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class CSCYouVerifyCharge : PacketDistributed
{

public const int goodsIdFieldNumber = 1;
 private bool hasGoodsId;
 private Int32 goodsId_ = 0;
 public bool HasGoodsId {
 get { return hasGoodsId; }
 }
 public Int32 GoodsId {
 get { return goodsId_; }
 set { SetGoodsId(value); }
 }
 public void SetGoodsId(Int32 value) { 
 hasGoodsId = true;
 goodsId_ = value;
 }

public const int productIdFieldNumber = 2;
 private bool hasProductId;
 private string productId_ = "";
 public bool HasProductId {
 get { return hasProductId; }
 }
 public string ProductId {
 get { return productId_; }
 set { SetProductId(value); }
 }
 public void SetProductId(string value) { 
 hasProductId = true;
 productId_ = value;
 }

public const int paywayFieldNumber = 3;
 private bool hasPayway;
 private Int32 payway_ = 0;
 public bool HasPayway {
 get { return hasPayway; }
 }
 public Int32 Payway {
 get { return payway_; }
 set { SetPayway(value); }
 }
 public void SetPayway(Int32 value) { 
 hasPayway = true;
 payway_ = value;
 }

public const int orderIdFieldNumber = 4;
 private bool hasOrderId;
 private string orderId_ = "";
 public bool HasOrderId {
 get { return hasOrderId; }
 }
 public string OrderId {
 get { return orderId_; }
 set { SetOrderId(value); }
 }
 public void SetOrderId(string value) { 
 hasOrderId = true;
 orderId_ = value;
 }

public const int goodPriceFieldNumber = 5;
 private bool hasGoodPrice;
 private Int32 goodPrice_ = 0;
 public bool HasGoodPrice {
 get { return hasGoodPrice; }
 }
 public Int32 GoodPrice {
 get { return goodPrice_; }
 set { SetGoodPrice(value); }
 }
 public void SetGoodPrice(Int32 value) { 
 hasGoodPrice = true;
 goodPrice_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasGoodsId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, GoodsId);
}
 if (HasProductId) {
size += pb::CodedOutputStream.ComputeStringSize(2, ProductId);
}
 if (HasPayway) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Payway);
}
 if (HasOrderId) {
size += pb::CodedOutputStream.ComputeStringSize(4, OrderId);
}
 if (HasGoodPrice) {
size += pb::CodedOutputStream.ComputeInt32Size(5, GoodPrice);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasGoodsId) {
output.WriteInt32(1, GoodsId);
}
 
if (HasProductId) {
output.WriteString(2, ProductId);
}
 
if (HasPayway) {
output.WriteInt32(3, Payway);
}
 
if (HasOrderId) {
output.WriteString(4, OrderId);
}
 
if (HasGoodPrice) {
output.WriteInt32(5, GoodPrice);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSCYouVerifyCharge _inst = (CSCYouVerifyCharge) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.GoodsId = input.ReadInt32();
break;
}
   case  18: {
 _inst.ProductId = input.ReadString();
break;
}
   case  24: {
 _inst.Payway = input.ReadInt32();
break;
}
   case  34: {
 _inst.OrderId = input.ReadString();
break;
}
   case  40: {
 _inst.GoodPrice = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasGoodsId) return false;
 if (!hasProductId) return false;
 if (!hasPayway) return false;
 if (!hasOrderId) return false;
 if (!hasGoodPrice) return false;
 return true;
 }

	}


[Serializable]
public class CSCardCombining : PacketDistributed
{

public const int hero_cardguidFieldNumber = 1;
 private bool hasHero_cardguid;
 private Int64 hero_cardguid_ = 0;
 public bool HasHero_cardguid {
 get { return hasHero_cardguid; }
 }
 public Int64 Hero_cardguid {
 get { return hero_cardguid_; }
 set { SetHero_cardguid(value); }
 }
 public void SetHero_cardguid(Int64 value) { 
 hasHero_cardguid = true;
 hero_cardguid_ = value;
 }

public const int swallowed_cardguidFieldNumber = 2;
 private pbc::PopsicleList<Int64> swallowed_cardguid_ = new pbc::PopsicleList<Int64>();
 public scg::IList<Int64> swallowed_cardguidList {
 get { return pbc::Lists.AsReadOnly(swallowed_cardguid_); }
 }
 
 public int swallowed_cardguidCount {
 get { return swallowed_cardguid_.Count; }
 }
 
public Int64 GetSwallowed_cardguid(int index) {
 return swallowed_cardguid_[index];
 }
 public void AddSwallowed_cardguid(Int64 value) {
 swallowed_cardguid_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasHero_cardguid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Hero_cardguid);
}
{
int dataSize = 0;
foreach (Int64 element in swallowed_cardguidList) {
dataSize += pb::CodedOutputStream.ComputeInt64SizeNoTag(element);
}
size += dataSize;
size += 1 * swallowed_cardguid_.Count;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasHero_cardguid) {
output.WriteInt64(1, Hero_cardguid);
}
{
if (swallowed_cardguid_.Count > 0) {
foreach (Int64 element in swallowed_cardguidList) {
output.WriteInt64(2,element);
}
}

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSCardCombining _inst = (CSCardCombining) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Hero_cardguid = input.ReadInt64();
break;
}
   case  16: {
 _inst.AddSwallowed_cardguid(input.ReadInt64());
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasHero_cardguid) return false;
 return true;
 }

	}


[Serializable]
public class CSCardEvolve : PacketDistributed
{

public const int evolve_cardguidFieldNumber = 1;
 private bool hasEvolve_cardguid;
 private Int64 evolve_cardguid_ = 0;
 public bool HasEvolve_cardguid {
 get { return hasEvolve_cardguid; }
 }
 public Int64 Evolve_cardguid {
 get { return evolve_cardguid_; }
 set { SetEvolve_cardguid(value); }
 }
 public void SetEvolve_cardguid(Int64 value) { 
 hasEvolve_cardguid = true;
 evolve_cardguid_ = value;
 }

public const int material_cardguidFieldNumber = 2;
 private pbc::PopsicleList<Int64> material_cardguid_ = new pbc::PopsicleList<Int64>();
 public scg::IList<Int64> material_cardguidList {
 get { return pbc::Lists.AsReadOnly(material_cardguid_); }
 }
 
 public int material_cardguidCount {
 get { return material_cardguid_.Count; }
 }
 
public Int64 GetMaterial_cardguid(int index) {
 return material_cardguid_[index];
 }
 public void AddMaterial_cardguid(Int64 value) {
 material_cardguid_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasEvolve_cardguid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Evolve_cardguid);
}
{
int dataSize = 0;
foreach (Int64 element in material_cardguidList) {
dataSize += pb::CodedOutputStream.ComputeInt64SizeNoTag(element);
}
size += dataSize;
size += 1 * material_cardguid_.Count;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasEvolve_cardguid) {
output.WriteInt64(1, Evolve_cardguid);
}
{
if (material_cardguid_.Count > 0) {
foreach (Int64 element in material_cardguidList) {
output.WriteInt64(2,element);
}
}

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSCardEvolve _inst = (CSCardEvolve) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Evolve_cardguid = input.ReadInt64();
break;
}
   case  16: {
 _inst.AddMaterial_cardguid(input.ReadInt64());
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasEvolve_cardguid) return false;
 return true;
 }

	}


[Serializable]
public class CSCardStrengthen : PacketDistributed
{

public const int streng_cardguidFieldNumber = 1;
 private bool hasStreng_cardguid;
 private Int64 streng_cardguid_ = 0;
 public bool HasStreng_cardguid {
 get { return hasStreng_cardguid; }
 }
 public Int64 Streng_cardguid {
 get { return streng_cardguid_; }
 set { SetStreng_cardguid(value); }
 }
 public void SetStreng_cardguid(Int64 value) { 
 hasStreng_cardguid = true;
 streng_cardguid_ = value;
 }

public const int streng_typeFieldNumber = 2;
 private bool hasStreng_type;
 private Int32 streng_type_ = 0;
 public bool HasStreng_type {
 get { return hasStreng_type; }
 }
 public Int32 Streng_type {
 get { return streng_type_; }
 set { SetStreng_type(value); }
 }
 public void SetStreng_type(Int32 value) { 
 hasStreng_type = true;
 streng_type_ = value;
 }

public const int streng_timesFieldNumber = 3;
 private bool hasStreng_times;
 private Int32 streng_times_ = 0;
 public bool HasStreng_times {
 get { return hasStreng_times; }
 }
 public Int32 Streng_times {
 get { return streng_times_; }
 set { SetStreng_times(value); }
 }
 public void SetStreng_times(Int32 value) { 
 hasStreng_times = true;
 streng_times_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasStreng_cardguid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Streng_cardguid);
}
 if (HasStreng_type) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Streng_type);
}
 if (HasStreng_times) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Streng_times);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasStreng_cardguid) {
output.WriteInt64(1, Streng_cardguid);
}
 
if (HasStreng_type) {
output.WriteInt32(2, Streng_type);
}
 
if (HasStreng_times) {
output.WriteInt32(3, Streng_times);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSCardStrengthen _inst = (CSCardStrengthen) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Streng_cardguid = input.ReadInt64();
break;
}
   case  16: {
 _inst.Streng_type = input.ReadInt32();
break;
}
   case  24: {
 _inst.Streng_times = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasStreng_cardguid) return false;
 if (!hasStreng_type) return false;
 return true;
 }

	}


[Serializable]
public class CSChangeCardConfirm : PacketDistributed
{

public const int changeCardInfoIDFieldNumber = 1;
 private bool hasChangeCardInfoID;
 private Int32 changeCardInfoID_ = 0;
 public bool HasChangeCardInfoID {
 get { return hasChangeCardInfoID; }
 }
 public Int32 ChangeCardInfoID {
 get { return changeCardInfoID_; }
 set { SetChangeCardInfoID(value); }
 }
 public void SetChangeCardInfoID(Int32 value) { 
 hasChangeCardInfoID = true;
 changeCardInfoID_ = value;
 }

public const int HaveCardflagFieldNumber = 2;
 private bool hasHaveCardflag;
 private Int32 HaveCardflag_ = 0;
 public bool HasHaveCardflag {
 get { return hasHaveCardflag; }
 }
 public Int32 HaveCardflag {
 get { return HaveCardflag_; }
 set { SetHaveCardflag(value); }
 }
 public void SetHaveCardflag(Int32 value) { 
 hasHaveCardflag = true;
 HaveCardflag_ = value;
 }

public const int info_1FieldNumber = 3;
 private pbc::PopsicleList<Int64> info_1_ = new pbc::PopsicleList<Int64>();
 public scg::IList<Int64> info_1List {
 get { return pbc::Lists.AsReadOnly(info_1_); }
 }
 
 public int info_1Count {
 get { return info_1_.Count; }
 }
 
public Int64 GetInfo_1(int index) {
 return info_1_[index];
 }
 public void AddInfo_1(Int64 value) {
 info_1_.Add(value);
 }

public const int info_2FieldNumber = 4;
 private pbc::PopsicleList<Int64> info_2_ = new pbc::PopsicleList<Int64>();
 public scg::IList<Int64> info_2List {
 get { return pbc::Lists.AsReadOnly(info_2_); }
 }
 
 public int info_2Count {
 get { return info_2_.Count; }
 }
 
public Int64 GetInfo_2(int index) {
 return info_2_[index];
 }
 public void AddInfo_2(Int64 value) {
 info_2_.Add(value);
 }

public const int info_3FieldNumber = 5;
 private pbc::PopsicleList<Int64> info_3_ = new pbc::PopsicleList<Int64>();
 public scg::IList<Int64> info_3List {
 get { return pbc::Lists.AsReadOnly(info_3_); }
 }
 
 public int info_3Count {
 get { return info_3_.Count; }
 }
 
public Int64 GetInfo_3(int index) {
 return info_3_[index];
 }
 public void AddInfo_3(Int64 value) {
 info_3_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasChangeCardInfoID) {
size += pb::CodedOutputStream.ComputeInt32Size(1, ChangeCardInfoID);
}
 if (HasHaveCardflag) {
size += pb::CodedOutputStream.ComputeInt32Size(2, HaveCardflag);
}
{
int dataSize = 0;
foreach (Int64 element in info_1List) {
dataSize += pb::CodedOutputStream.ComputeInt64SizeNoTag(element);
}
size += dataSize;
size += 1 * info_1_.Count;
}
{
int dataSize = 0;
foreach (Int64 element in info_2List) {
dataSize += pb::CodedOutputStream.ComputeInt64SizeNoTag(element);
}
size += dataSize;
size += 1 * info_2_.Count;
}
{
int dataSize = 0;
foreach (Int64 element in info_3List) {
dataSize += pb::CodedOutputStream.ComputeInt64SizeNoTag(element);
}
size += dataSize;
size += 1 * info_3_.Count;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasChangeCardInfoID) {
output.WriteInt32(1, ChangeCardInfoID);
}
 
if (HasHaveCardflag) {
output.WriteInt32(2, HaveCardflag);
}
{
if (info_1_.Count > 0) {
foreach (Int64 element in info_1List) {
output.WriteInt64(3,element);
}
}

}
{
if (info_2_.Count > 0) {
foreach (Int64 element in info_2List) {
output.WriteInt64(4,element);
}
}

}
{
if (info_3_.Count > 0) {
foreach (Int64 element in info_3List) {
output.WriteInt64(5,element);
}
}

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSChangeCardConfirm _inst = (CSChangeCardConfirm) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.ChangeCardInfoID = input.ReadInt32();
break;
}
   case  16: {
 _inst.HaveCardflag = input.ReadInt32();
break;
}
   case  24: {
 _inst.AddInfo_1(input.ReadInt64());
break;
}
   case  32: {
 _inst.AddInfo_2(input.ReadInt64());
break;
}
   case  40: {
 _inst.AddInfo_3(input.ReadInt64());
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasChangeCardInfoID) return false;
 if (!hasHaveCardflag) return false;
 return true;
 }

	}


[Serializable]
public class CSChangeMember : PacketDistributed
{

public const int cardguidListFieldNumber = 1;
 private pbc::PopsicleList<Int64> cardguidList_ = new pbc::PopsicleList<Int64>();
 public scg::IList<Int64> cardguidListList {
 get { return pbc::Lists.AsReadOnly(cardguidList_); }
 }
 
 public int cardguidListCount {
 get { return cardguidList_.Count; }
 }
 
public Int64 GetCardguidList(int index) {
 return cardguidList_[index];
 }
 public void AddCardguidList(Int64 value) {
 cardguidList_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int dataSize = 0;
foreach (Int64 element in cardguidListList) {
dataSize += pb::CodedOutputStream.ComputeInt64SizeNoTag(element);
}
size += dataSize;
size += 1 * cardguidList_.Count;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
if (cardguidList_.Count > 0) {
foreach (Int64 element in cardguidListList) {
output.WriteInt64(1,element);
}
}

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSChangeMember _inst = (CSChangeMember) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.AddCardguidList(input.ReadInt64());
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class CSChangeName : PacketDistributed
{

public const int player_guidFieldNumber = 1;
 private bool hasPlayer_guid;
 private Int32 player_guid_ = 0;
 public bool HasPlayer_guid {
 get { return hasPlayer_guid; }
 }
 public Int32 Player_guid {
 get { return player_guid_; }
 set { SetPlayer_guid(value); }
 }
 public void SetPlayer_guid(Int32 value) { 
 hasPlayer_guid = true;
 player_guid_ = value;
 }

public const int nameFieldNumber = 2;
 private bool hasName;
 private string name_ = "";
 public bool HasName {
 get { return hasName; }
 }
 public string Name {
 get { return name_; }
 set { SetName(value); }
 }
 public void SetName(string value) { 
 hasName = true;
 name_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasPlayer_guid) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Player_guid);
}
 if (HasName) {
size += pb::CodedOutputStream.ComputeStringSize(2, Name);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasPlayer_guid) {
output.WriteInt32(1, Player_guid);
}
 
if (HasName) {
output.WriteString(2, Name);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSChangeName _inst = (CSChangeName) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Player_guid = input.ReadInt32();
break;
}
   case  18: {
 _inst.Name = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasPlayer_guid) return false;
 if (!hasName) return false;
 return true;
 }

	}


[Serializable]
public class CSClearBattleData : PacketDistributed
{

public const int copyDetialFieldNumber = 1;
 private bool hasCopyDetial;
 private Int32 copyDetial_ = 0;
 public bool HasCopyDetial {
 get { return hasCopyDetial; }
 }
 public Int32 CopyDetial {
 get { return copyDetial_; }
 set { SetCopyDetial(value); }
 }
 public void SetCopyDetial(Int32 value) { 
 hasCopyDetial = true;
 copyDetial_ = value;
 }

public const int copyFatherFieldNumber = 2;
 private bool hasCopyFather;
 private Int32 copyFather_ = 0;
 public bool HasCopyFather {
 get { return hasCopyFather; }
 }
 public Int32 CopyFather {
 get { return copyFather_; }
 set { SetCopyFather(value); }
 }
 public void SetCopyFather(Int32 value) { 
 hasCopyFather = true;
 copyFather_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasCopyDetial) {
size += pb::CodedOutputStream.ComputeInt32Size(1, CopyDetial);
}
 if (HasCopyFather) {
size += pb::CodedOutputStream.ComputeInt32Size(2, CopyFather);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasCopyDetial) {
output.WriteInt32(1, CopyDetial);
}
 
if (HasCopyFather) {
output.WriteInt32(2, CopyFather);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSClearBattleData _inst = (CSClearBattleData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.CopyDetial = input.ReadInt32();
break;
}
   case  16: {
 _inst.CopyFather = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class CSClearPaiTaBattleData : PacketDistributed
{

public const int guidFieldNumber = 1;
 private bool hasGuid;
 private Int64 guid_ = 0;
 public bool HasGuid {
 get { return hasGuid; }
 }
 public Int64 Guid {
 get { return guid_; }
 set { SetGuid(value); }
 }
 public void SetGuid(Int64 value) { 
 hasGuid = true;
 guid_ = value;
 }

public const int clearflagFieldNumber = 2;
 private bool hasClearflag;
 private Int32 clearflag_ = 0;
 public bool HasClearflag {
 get { return hasClearflag; }
 }
 public Int32 Clearflag {
 get { return clearflag_; }
 set { SetClearflag(value); }
 }
 public void SetClearflag(Int32 value) { 
 hasClearflag = true;
 clearflag_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasGuid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Guid);
}
 if (HasClearflag) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Clearflag);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasGuid) {
output.WriteInt64(1, Guid);
}
 
if (HasClearflag) {
output.WriteInt32(2, Clearflag);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSClearPaiTaBattleData _inst = (CSClearPaiTaBattleData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Guid = input.ReadInt64();
break;
}
   case  16: {
 _inst.Clearflag = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasGuid) return false;
 if (!hasClearflag) return false;
 return true;
 }

	}


[Serializable]
public class CSCommonProductList : PacketDistributed
{

public const int channelidFieldNumber = 1;
 private bool hasChannelid;
 private Int32 channelid_ = 0;
 public bool HasChannelid {
 get { return hasChannelid; }
 }
 public Int32 Channelid {
 get { return channelid_; }
 set { SetChannelid(value); }
 }
 public void SetChannelid(Int32 value) { 
 hasChannelid = true;
 channelid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasChannelid) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Channelid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasChannelid) {
output.WriteInt32(1, Channelid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSCommonProductList _inst = (CSCommonProductList) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Channelid = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class CSDeleteFriend : PacketDistributed
{

public const int friend_guidFieldNumber = 1;
 private bool hasFriend_guid;
 private Int64 friend_guid_ = 0;
 public bool HasFriend_guid {
 get { return hasFriend_guid; }
 }
 public Int64 Friend_guid {
 get { return friend_guid_; }
 set { SetFriend_guid(value); }
 }
 public void SetFriend_guid(Int64 value) { 
 hasFriend_guid = true;
 friend_guid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasFriend_guid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Friend_guid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasFriend_guid) {
output.WriteInt64(1, Friend_guid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSDeleteFriend _inst = (CSDeleteFriend) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Friend_guid = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasFriend_guid) return false;
 return true;
 }

	}


[Serializable]
public class CSFinishTask : PacketDistributed
{

public const int player_guidFieldNumber = 1;
 private bool hasPlayer_guid;
 private Int64 player_guid_ = 0;
 public bool HasPlayer_guid {
 get { return hasPlayer_guid; }
 }
 public Int64 Player_guid {
 get { return player_guid_; }
 set { SetPlayer_guid(value); }
 }
 public void SetPlayer_guid(Int64 value) { 
 hasPlayer_guid = true;
 player_guid_ = value;
 }

public const int templet_idFieldNumber = 2;
 private bool hasTemplet_id;
 private Int32 templet_id_ = 0;
 public bool HasTemplet_id {
 get { return hasTemplet_id; }
 }
 public Int32 Templet_id {
 get { return templet_id_; }
 set { SetTemplet_id(value); }
 }
 public void SetTemplet_id(Int32 value) { 
 hasTemplet_id = true;
 templet_id_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasPlayer_guid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Player_guid);
}
 if (HasTemplet_id) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Templet_id);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasPlayer_guid) {
output.WriteInt64(1, Player_guid);
}
 
if (HasTemplet_id) {
output.WriteInt32(2, Templet_id);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSFinishTask _inst = (CSFinishTask) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Player_guid = input.ReadInt64();
break;
}
   case  16: {
 _inst.Templet_id = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasPlayer_guid) return false;
 if (!hasTemplet_id) return false;
 return true;
 }

	}


[Serializable]
public class CSFriendMailDelete : PacketDistributed
{

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSFriendMailDelete _inst = (CSFriendMailDelete) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
 
 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class CSFriendsList : PacketDistributed
{

public const int user_guidFieldNumber = 1;
 private bool hasUser_guid;
 private Int64 user_guid_ = 0;
 public bool HasUser_guid {
 get { return hasUser_guid; }
 }
 public Int64 User_guid {
 get { return user_guid_; }
 set { SetUser_guid(value); }
 }
 public void SetUser_guid(Int64 value) { 
 hasUser_guid = true;
 user_guid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasUser_guid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, User_guid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasUser_guid) {
output.WriteInt64(1, User_guid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSFriendsList _inst = (CSFriendsList) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.User_guid = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasUser_guid) return false;
 return true;
 }

	}


[Serializable]
public class CSGGL : PacketDistributed
{

public const int player_guidFieldNumber = 1;
 private bool hasPlayer_guid;
 private Int32 player_guid_ = 0;
 public bool HasPlayer_guid {
 get { return hasPlayer_guid; }
 }
 public Int32 Player_guid {
 get { return player_guid_; }
 set { SetPlayer_guid(value); }
 }
 public void SetPlayer_guid(Int32 value) { 
 hasPlayer_guid = true;
 player_guid_ = value;
 }

public const int timesFieldNumber = 2;
 private bool hasTimes;
 private Int32 times_ = 0;
 public bool HasTimes {
 get { return hasTimes; }
 }
 public Int32 Times {
 get { return times_; }
 set { SetTimes(value); }
 }
 public void SetTimes(Int32 value) { 
 hasTimes = true;
 times_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasPlayer_guid) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Player_guid);
}
 if (HasTimes) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Times);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasPlayer_guid) {
output.WriteInt32(1, Player_guid);
}
 
if (HasTimes) {
output.WriteInt32(2, Times);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSGGL _inst = (CSGGL) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Player_guid = input.ReadInt32();
break;
}
   case  16: {
 _inst.Times = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasPlayer_guid) return false;
 if (!hasTimes) return false;
 return true;
 }

	}


[Serializable]
public class CSGMcmds : PacketDistributed
{

public const int cmdsFieldNumber = 1;
 private bool hasCmds;
 private string cmds_ = "";
 public bool HasCmds {
 get { return hasCmds; }
 }
 public string Cmds {
 get { return cmds_; }
 set { SetCmds(value); }
 }
 public void SetCmds(string value) { 
 hasCmds = true;
 cmds_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasCmds) {
size += pb::CodedOutputStream.ComputeStringSize(1, Cmds);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasCmds) {
output.WriteString(1, Cmds);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSGMcmds _inst = (CSGMcmds) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  10: {
 _inst.Cmds = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasCmds) return false;
 return true;
 }

	}


[Serializable]
public class CSGamble : PacketDistributed
{

public const int typeFieldNumber = 1;
 private bool hasType;
 private Int32 type_ = 0;
 public bool HasType {
 get { return hasType; }
 }
 public Int32 Type {
 get { return type_; }
 set { SetType(value); }
 }
 public void SetType(Int32 value) { 
 hasType = true;
 type_ = value;
 }

public const int timesFieldNumber = 2;
 private bool hasTimes;
 private Int32 times_ = 0;
 public bool HasTimes {
 get { return hasTimes; }
 }
 public Int32 Times {
 get { return times_; }
 set { SetTimes(value); }
 }
 public void SetTimes(Int32 value) { 
 hasTimes = true;
 times_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasType) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Type);
}
 if (HasTimes) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Times);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasType) {
output.WriteInt32(1, Type);
}
 
if (HasTimes) {
output.WriteInt32(2, Times);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSGamble _inst = (CSGamble) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Type = input.ReadInt32();
break;
}
   case  16: {
 _inst.Times = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasType) return false;
 if (!hasTimes) return false;
 return true;
 }

	}


[Serializable]
public class CSGetFriendPower : PacketDistributed
{

public const int friend_guidFieldNumber = 1;
 private bool hasFriend_guid;
 private Int64 friend_guid_ = 0;
 public bool HasFriend_guid {
 get { return hasFriend_guid; }
 }
 public Int64 Friend_guid {
 get { return friend_guid_; }
 set { SetFriend_guid(value); }
 }
 public void SetFriend_guid(Int64 value) { 
 hasFriend_guid = true;
 friend_guid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasFriend_guid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Friend_guid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasFriend_guid) {
output.WriteInt64(1, Friend_guid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSGetFriendPower _inst = (CSGetFriendPower) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Friend_guid = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasFriend_guid) return false;
 return true;
 }

	}


[Serializable]
public class CSGetRandomAssistanceList : PacketDistributed
{

public const int user_guidFieldNumber = 1;
 private bool hasUser_guid;
 private Int64 user_guid_ = 0;
 public bool HasUser_guid {
 get { return hasUser_guid; }
 }
 public Int64 User_guid {
 get { return user_guid_; }
 set { SetUser_guid(value); }
 }
 public void SetUser_guid(Int64 value) { 
 hasUser_guid = true;
 user_guid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasUser_guid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, User_guid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasUser_guid) {
output.WriteInt64(1, User_guid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSGetRandomAssistanceList _inst = (CSGetRandomAssistanceList) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.User_guid = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasUser_guid) return false;
 return true;
 }

	}


[Serializable]
public class CSGiveFriendPower : PacketDistributed
{

public const int friend_guidFieldNumber = 1;
 private bool hasFriend_guid;
 private Int64 friend_guid_ = 0;
 public bool HasFriend_guid {
 get { return hasFriend_guid; }
 }
 public Int64 Friend_guid {
 get { return friend_guid_; }
 set { SetFriend_guid(value); }
 }
 public void SetFriend_guid(Int64 value) { 
 hasFriend_guid = true;
 friend_guid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasFriend_guid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Friend_guid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasFriend_guid) {
output.WriteInt64(1, Friend_guid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSGiveFriendPower _inst = (CSGiveFriendPower) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Friend_guid = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasFriend_guid) return false;
 return true;
 }

	}


[Serializable]
public class CSGooglePayVerifyCharge : PacketDistributed
{

public const int orderIdFieldNumber = 1;
 private bool hasOrderId;
 private string orderId_ = "";
 public bool HasOrderId {
 get { return hasOrderId; }
 }
 public string OrderId {
 get { return orderId_; }
 set { SetOrderId(value); }
 }
 public void SetOrderId(string value) { 
 hasOrderId = true;
 orderId_ = value;
 }

public const int channelIdFieldNumber = 3;
 private bool hasChannelId;
 private Int32 channelId_ = 0;
 public bool HasChannelId {
 get { return hasChannelId; }
 }
 public Int32 ChannelId {
 get { return channelId_; }
 set { SetChannelId(value); }
 }
 public void SetChannelId(Int32 value) { 
 hasChannelId = true;
 channelId_ = value;
 }

public const int signatureFieldNumber = 4;
 private bool hasSignature;
 private string signature_ = "";
 public bool HasSignature {
 get { return hasSignature; }
 }
 public string Signature {
 get { return signature_; }
 set { SetSignature(value); }
 }
 public void SetSignature(string value) { 
 hasSignature = true;
 signature_ = value;
 }

public const int purchaseDataFieldNumber = 5;
 private bool hasPurchaseData;
 private string purchaseData_ = "";
 public bool HasPurchaseData {
 get { return hasPurchaseData; }
 }
 public string PurchaseData {
 get { return purchaseData_; }
 set { SetPurchaseData(value); }
 }
 public void SetPurchaseData(string value) { 
 hasPurchaseData = true;
 purchaseData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasOrderId) {
size += pb::CodedOutputStream.ComputeStringSize(1, OrderId);
}
 if (HasChannelId) {
size += pb::CodedOutputStream.ComputeInt32Size(3, ChannelId);
}
 if (HasSignature) {
size += pb::CodedOutputStream.ComputeStringSize(4, Signature);
}
 if (HasPurchaseData) {
size += pb::CodedOutputStream.ComputeStringSize(5, PurchaseData);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasOrderId) {
output.WriteString(1, OrderId);
}
 
if (HasChannelId) {
output.WriteInt32(3, ChannelId);
}
 
if (HasSignature) {
output.WriteString(4, Signature);
}
 
if (HasPurchaseData) {
output.WriteString(5, PurchaseData);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSGooglePayVerifyCharge _inst = (CSGooglePayVerifyCharge) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  10: {
 _inst.OrderId = input.ReadString();
break;
}
   case  24: {
 _inst.ChannelId = input.ReadInt32();
break;
}
   case  34: {
 _inst.Signature = input.ReadString();
break;
}
   case  42: {
 _inst.PurchaseData = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasOrderId) return false;
 if (!hasChannelId) return false;
 if (!hasSignature) return false;
 if (!hasPurchaseData) return false;
 return true;
 }

	}


[Serializable]
public class CSGuide : PacketDistributed
{

public const int player_guidFieldNumber = 1;
 private bool hasPlayer_guid;
 private Int32 player_guid_ = 0;
 public bool HasPlayer_guid {
 get { return hasPlayer_guid; }
 }
 public Int32 Player_guid {
 get { return player_guid_; }
 set { SetPlayer_guid(value); }
 }
 public void SetPlayer_guid(Int32 value) { 
 hasPlayer_guid = true;
 player_guid_ = value;
 }

public const int finish_stepFieldNumber = 2;
 private bool hasFinish_step;
 private Int32 finish_step_ = 0;
 public bool HasFinish_step {
 get { return hasFinish_step; }
 }
 public Int32 Finish_step {
 get { return finish_step_; }
 set { SetFinish_step(value); }
 }
 public void SetFinish_step(Int32 value) { 
 hasFinish_step = true;
 finish_step_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasPlayer_guid) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Player_guid);
}
 if (HasFinish_step) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Finish_step);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasPlayer_guid) {
output.WriteInt32(1, Player_guid);
}
 
if (HasFinish_step) {
output.WriteInt32(2, Finish_step);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSGuide _inst = (CSGuide) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Player_guid = input.ReadInt32();
break;
}
   case  16: {
 _inst.Finish_step = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasPlayer_guid) return false;
 if (!hasFinish_step) return false;
 return true;
 }

	}


[Serializable]
public class CSLogin : PacketDistributed
{

public const int midFieldNumber = 1;
 private bool hasMid;
 private string mid_ = "";
 public bool HasMid {
 get { return hasMid; }
 }
 public string Mid {
 get { return mid_; }
 set { SetMid(value); }
 }
 public void SetMid(string value) { 
 hasMid = true;
 mid_ = value;
 }

public const int versionFieldNumber = 2;
 private bool hasVersion;
 private string version_ = "";
 public bool HasVersion {
 get { return hasVersion; }
 }
 public string Version {
 get { return version_; }
 set { SetVersion(value); }
 }
 public void SetVersion(string value) { 
 hasVersion = true;
 version_ = value;
 }

public const int accountIdFieldNumber = 3;
 private bool hasAccountId;
 private string accountId_ = "";
 public bool HasAccountId {
 get { return hasAccountId; }
 }
 public string AccountId {
 get { return accountId_; }
 set { SetAccountId(value); }
 }
 public void SetAccountId(string value) { 
 hasAccountId = true;
 accountId_ = value;
 }

public const int areaFieldNumber = 4;
 private bool hasArea;
 private string area_ = "";
 public bool HasArea {
 get { return hasArea; }
 }
 public string Area {
 get { return area_; }
 set { SetArea(value); }
 }
 public void SetArea(string value) { 
 hasArea = true;
 area_ = value;
 }

public const int countryFieldNumber = 5;
 private bool hasCountry;
 private string country_ = "";
 public bool HasCountry {
 get { return hasCountry; }
 }
 public string Country {
 get { return country_; }
 set { SetCountry(value); }
 }
 public void SetCountry(string value) { 
 hasCountry = true;
 country_ = value;
 }

public const int deviceFieldNumber = 6;
 private bool hasDevice;
 private string device_ = "";
 public bool HasDevice {
 get { return hasDevice; }
 }
 public string Device {
 get { return device_; }
 set { SetDevice(value); }
 }
 public void SetDevice(string value) { 
 hasDevice = true;
 device_ = value;
 }

public const int deviceSystemFieldNumber = 7;
 private bool hasDeviceSystem;
 private string deviceSystem_ = "";
 public bool HasDeviceSystem {
 get { return hasDeviceSystem; }
 }
 public string DeviceSystem {
 get { return deviceSystem_; }
 set { SetDeviceSystem(value); }
 }
 public void SetDeviceSystem(string value) { 
 hasDeviceSystem = true;
 deviceSystem_ = value;
 }

public const int downloadTypeFieldNumber = 8;
 private bool hasDownloadType;
 private string downloadType_ = "";
 public bool HasDownloadType {
 get { return hasDownloadType; }
 }
 public string DownloadType {
 get { return downloadType_; }
 set { SetDownloadType(value); }
 }
 public void SetDownloadType(string value) { 
 hasDownloadType = true;
 downloadType_ = value;
 }

public const int networkTypeFieldNumber = 9;
 private bool hasNetworkType;
 private string networkType_ = "";
 public bool HasNetworkType {
 get { return hasNetworkType; }
 }
 public string NetworkType {
 get { return networkType_; }
 set { SetNetworkType(value); }
 }
 public void SetNetworkType(string value) { 
 hasNetworkType = true;
 networkType_ = value;
 }

public const int prisonBreakFieldNumber = 10;
 private bool hasPrisonBreak;
 private string prisonBreak_ = "";
 public bool HasPrisonBreak {
 get { return hasPrisonBreak; }
 }
 public string PrisonBreak {
 get { return prisonBreak_; }
 set { SetPrisonBreak(value); }
 }
 public void SetPrisonBreak(string value) { 
 hasPrisonBreak = true;
 prisonBreak_ = value;
 }

public const int operatorFieldNumber = 11;
 private bool hasOperator;
 private string operator_ = "";
 public bool HasOperator {
 get { return hasOperator; }
 }
 public string Operator {
 get { return operator_; }
 set { SetOperator(value); }
 }
 public void SetOperator(string value) { 
 hasOperator = true;
 operator_ = value;
 }

public const int serveridFieldNumber = 12;
 private bool hasServerid;
 private Int32 serverid_ = 0;
 public bool HasServerid {
 get { return hasServerid; }
 }
 public Int32 Serverid {
 get { return serverid_; }
 set { SetServerid(value); }
 }
 public void SetServerid(Int32 value) { 
 hasServerid = true;
 serverid_ = value;
 }

public const int typeFieldNumber = 13;
 private bool hasType;
 private Int32 type_ = 0;
 public bool HasType {
 get { return hasType; }
 }
 public Int32 Type {
 get { return type_; }
 set { SetType(value); }
 }
 public void SetType(Int32 value) { 
 hasType = true;
 type_ = value;
 }

public const int usernameFieldNumber = 14;
 private bool hasUsername;
 private string username_ = "";
 public bool HasUsername {
 get { return hasUsername; }
 }
 public string Username {
 get { return username_; }
 set { SetUsername(value); }
 }
 public void SetUsername(string value) { 
 hasUsername = true;
 username_ = value;
 }

public const int passwordFieldNumber = 15;
 private bool hasPassword;
 private string password_ = "";
 public bool HasPassword {
 get { return hasPassword; }
 }
 public string Password {
 get { return password_; }
 set { SetPassword(value); }
 }
 public void SetPassword(string value) { 
 hasPassword = true;
 password_ = value;
 }

public const int infoFieldNumber = 16;
 private pbc::PopsicleList<ButtonInfo> info_ = new pbc::PopsicleList<ButtonInfo>();
 public scg::IList<ButtonInfo> infoList {
 get { return pbc::Lists.AsReadOnly(info_); }
 }
 
 public int infoCount {
 get { return info_.Count; }
 }
 
public ButtonInfo GetInfo(int index) {
 return info_[index];
 }
 public void AddInfo(ButtonInfo value) {
 info_.Add(value);
 }

public const int lastAccountIdFieldNumber = 17;
 private bool hasLastAccountId;
 private string lastAccountId_ = "";
 public bool HasLastAccountId {
 get { return hasLastAccountId; }
 }
 public string LastAccountId {
 get { return lastAccountId_; }
 set { SetLastAccountId(value); }
 }
 public void SetLastAccountId(string value) { 
 hasLastAccountId = true;
 lastAccountId_ = value;
 }

public const int loginTypeFieldNumber = 18;
 private bool hasLoginType;
 private Int32 loginType_ = 0;
 public bool HasLoginType {
 get { return hasLoginType; }
 }
 public Int32 LoginType {
 get { return loginType_; }
 set { SetLoginType(value); }
 }
 public void SetLoginType(Int32 value) { 
 hasLoginType = true;
 loginType_ = value;
 }

public const int uidFieldNumber = 19;
 private bool hasUid;
 private string uid_ = "";
 public bool HasUid {
 get { return hasUid; }
 }
 public string Uid {
 get { return uid_; }
 set { SetUid(value); }
 }
 public void SetUid(string value) { 
 hasUid = true;
 uid_ = value;
 }

public const int tokenFieldNumber = 20;
 private bool hasToken;
 private string token_ = "";
 public bool HasToken {
 get { return hasToken; }
 }
 public string Token {
 get { return token_; }
 set { SetToken(value); }
 }
 public void SetToken(string value) { 
 hasToken = true;
 token_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasMid) {
size += pb::CodedOutputStream.ComputeStringSize(1, Mid);
}
 if (HasVersion) {
size += pb::CodedOutputStream.ComputeStringSize(2, Version);
}
 if (HasAccountId) {
size += pb::CodedOutputStream.ComputeStringSize(3, AccountId);
}
 if (HasArea) {
size += pb::CodedOutputStream.ComputeStringSize(4, Area);
}
 if (HasCountry) {
size += pb::CodedOutputStream.ComputeStringSize(5, Country);
}
 if (HasDevice) {
size += pb::CodedOutputStream.ComputeStringSize(6, Device);
}
 if (HasDeviceSystem) {
size += pb::CodedOutputStream.ComputeStringSize(7, DeviceSystem);
}
 if (HasDownloadType) {
size += pb::CodedOutputStream.ComputeStringSize(8, DownloadType);
}
 if (HasNetworkType) {
size += pb::CodedOutputStream.ComputeStringSize(9, NetworkType);
}
 if (HasPrisonBreak) {
size += pb::CodedOutputStream.ComputeStringSize(10, PrisonBreak);
}
 if (HasOperator) {
size += pb::CodedOutputStream.ComputeStringSize(11, Operator);
}
 if (HasServerid) {
size += pb::CodedOutputStream.ComputeInt32Size(12, Serverid);
}
 if (HasType) {
size += pb::CodedOutputStream.ComputeInt32Size(13, Type);
}
 if (HasUsername) {
size += pb::CodedOutputStream.ComputeStringSize(14, Username);
}
 if (HasPassword) {
size += pb::CodedOutputStream.ComputeStringSize(15, Password);
}
{
foreach (ButtonInfo element in infoList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)16) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 if (HasLastAccountId) {
size += pb::CodedOutputStream.ComputeStringSize(17, LastAccountId);
}
 if (HasLoginType) {
size += pb::CodedOutputStream.ComputeInt32Size(18, LoginType);
}
 if (HasUid) {
size += pb::CodedOutputStream.ComputeStringSize(19, Uid);
}
 if (HasToken) {
size += pb::CodedOutputStream.ComputeStringSize(20, Token);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasMid) {
output.WriteString(1, Mid);
}
 
if (HasVersion) {
output.WriteString(2, Version);
}
 
if (HasAccountId) {
output.WriteString(3, AccountId);
}
 
if (HasArea) {
output.WriteString(4, Area);
}
 
if (HasCountry) {
output.WriteString(5, Country);
}
 
if (HasDevice) {
output.WriteString(6, Device);
}
 
if (HasDeviceSystem) {
output.WriteString(7, DeviceSystem);
}
 
if (HasDownloadType) {
output.WriteString(8, DownloadType);
}
 
if (HasNetworkType) {
output.WriteString(9, NetworkType);
}
 
if (HasPrisonBreak) {
output.WriteString(10, PrisonBreak);
}
 
if (HasOperator) {
output.WriteString(11, Operator);
}
 
if (HasServerid) {
output.WriteInt32(12, Serverid);
}
 
if (HasType) {
output.WriteInt32(13, Type);
}
 
if (HasUsername) {
output.WriteString(14, Username);
}
 
if (HasPassword) {
output.WriteString(15, Password);
}

do{
foreach (ButtonInfo element in infoList) {
output.WriteTag((int)16, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 
if (HasLastAccountId) {
output.WriteString(17, LastAccountId);
}
 
if (HasLoginType) {
output.WriteInt32(18, LoginType);
}
 
if (HasUid) {
output.WriteString(19, Uid);
}
 
if (HasToken) {
output.WriteString(20, Token);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSLogin _inst = (CSLogin) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  10: {
 _inst.Mid = input.ReadString();
break;
}
   case  18: {
 _inst.Version = input.ReadString();
break;
}
   case  26: {
 _inst.AccountId = input.ReadString();
break;
}
   case  34: {
 _inst.Area = input.ReadString();
break;
}
   case  42: {
 _inst.Country = input.ReadString();
break;
}
   case  50: {
 _inst.Device = input.ReadString();
break;
}
   case  58: {
 _inst.DeviceSystem = input.ReadString();
break;
}
   case  66: {
 _inst.DownloadType = input.ReadString();
break;
}
   case  74: {
 _inst.NetworkType = input.ReadString();
break;
}
   case  82: {
 _inst.PrisonBreak = input.ReadString();
break;
}
   case  90: {
 _inst.Operator = input.ReadString();
break;
}
   case  96: {
 _inst.Serverid = input.ReadInt32();
break;
}
   case  104: {
 _inst.Type = input.ReadInt32();
break;
}
   case  114: {
 _inst.Username = input.ReadString();
break;
}
   case  122: {
 _inst.Password = input.ReadString();
break;
}
    case  130: {
ButtonInfo subBuilder =  new ButtonInfo();
input.ReadMessage(subBuilder);
_inst.AddInfo(subBuilder);
break;
}
   case  138: {
 _inst.LastAccountId = input.ReadString();
break;
}
   case  144: {
 _inst.LoginType = input.ReadInt32();
break;
}
   case  154: {
 _inst.Uid = input.ReadString();
break;
}
   case  162: {
 _inst.Token = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasMid) return false;
 if (!hasVersion) return false;
foreach (ButtonInfo element in infoList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class CSLoginThirdPlatform : PacketDistributed
{

public const int midFieldNumber = 1;
 private bool hasMid;
 private string mid_ = "";
 public bool HasMid {
 get { return hasMid; }
 }
 public string Mid {
 get { return mid_; }
 set { SetMid(value); }
 }
 public void SetMid(string value) { 
 hasMid = true;
 mid_ = value;
 }

public const int versionFieldNumber = 2;
 private bool hasVersion;
 private string version_ = "";
 public bool HasVersion {
 get { return hasVersion; }
 }
 public string Version {
 get { return version_; }
 set { SetVersion(value); }
 }
 public void SetVersion(string value) { 
 hasVersion = true;
 version_ = value;
 }

public const int platformFieldNumber = 3;
 private bool hasPlatform;
 private Int32 platform_ = 0;
 public bool HasPlatform {
 get { return hasPlatform; }
 }
 public Int32 Platform {
 get { return platform_; }
 set { SetPlatform(value); }
 }
 public void SetPlatform(Int32 value) { 
 hasPlatform = true;
 platform_ = value;
 }

public const int sidFieldNumber = 4;
 private bool hasSid;
 private string sid_ = "";
 public bool HasSid {
 get { return hasSid; }
 }
 public string Sid {
 get { return sid_; }
 set { SetSid(value); }
 }
 public void SetSid(string value) { 
 hasSid = true;
 sid_ = value;
 }

public const int areaFieldNumber = 5;
 private bool hasArea;
 private string area_ = "";
 public bool HasArea {
 get { return hasArea; }
 }
 public string Area {
 get { return area_; }
 set { SetArea(value); }
 }
 public void SetArea(string value) { 
 hasArea = true;
 area_ = value;
 }

public const int countryFieldNumber = 6;
 private bool hasCountry;
 private string country_ = "";
 public bool HasCountry {
 get { return hasCountry; }
 }
 public string Country {
 get { return country_; }
 set { SetCountry(value); }
 }
 public void SetCountry(string value) { 
 hasCountry = true;
 country_ = value;
 }

public const int deviceFieldNumber = 7;
 private bool hasDevice;
 private string device_ = "";
 public bool HasDevice {
 get { return hasDevice; }
 }
 public string Device {
 get { return device_; }
 set { SetDevice(value); }
 }
 public void SetDevice(string value) { 
 hasDevice = true;
 device_ = value;
 }

public const int deviceSystemFieldNumber = 8;
 private bool hasDeviceSystem;
 private string deviceSystem_ = "";
 public bool HasDeviceSystem {
 get { return hasDeviceSystem; }
 }
 public string DeviceSystem {
 get { return deviceSystem_; }
 set { SetDeviceSystem(value); }
 }
 public void SetDeviceSystem(string value) { 
 hasDeviceSystem = true;
 deviceSystem_ = value;
 }

public const int networkTypeFieldNumber = 9;
 private bool hasNetworkType;
 private string networkType_ = "";
 public bool HasNetworkType {
 get { return hasNetworkType; }
 }
 public string NetworkType {
 get { return networkType_; }
 set { SetNetworkType(value); }
 }
 public void SetNetworkType(string value) { 
 hasNetworkType = true;
 networkType_ = value;
 }

public const int operatorFieldNumber = 11;
 private bool hasOperator;
 private string operator_ = "";
 public bool HasOperator {
 get { return hasOperator; }
 }
 public string Operator {
 get { return operator_; }
 set { SetOperator(value); }
 }
 public void SetOperator(string value) { 
 hasOperator = true;
 operator_ = value;
 }

public const int serveridFieldNumber = 12;
 private bool hasServerid;
 private Int32 serverid_ = 0;
 public bool HasServerid {
 get { return hasServerid; }
 }
 public Int32 Serverid {
 get { return serverid_; }
 set { SetServerid(value); }
 }
 public void SetServerid(Int32 value) { 
 hasServerid = true;
 serverid_ = value;
 }

public const int infoFieldNumber = 16;
 private pbc::PopsicleList<ButtonInfo> info_ = new pbc::PopsicleList<ButtonInfo>();
 public scg::IList<ButtonInfo> infoList {
 get { return pbc::Lists.AsReadOnly(info_); }
 }
 
 public int infoCount {
 get { return info_.Count; }
 }
 
public ButtonInfo GetInfo(int index) {
 return info_[index];
 }
 public void AddInfo(ButtonInfo value) {
 info_.Add(value);
 }

public const int lastAccountIdFieldNumber = 17;
 private bool hasLastAccountId;
 private string lastAccountId_ = "";
 public bool HasLastAccountId {
 get { return hasLastAccountId; }
 }
 public string LastAccountId {
 get { return lastAccountId_; }
 set { SetLastAccountId(value); }
 }
 public void SetLastAccountId(string value) { 
 hasLastAccountId = true;
 lastAccountId_ = value;
 }

public const int loginTypeFieldNumber = 18;
 private bool hasLoginType;
 private Int32 loginType_ = 0;
 public bool HasLoginType {
 get { return hasLoginType; }
 }
 public Int32 LoginType {
 get { return loginType_; }
 set { SetLoginType(value); }
 }
 public void SetLoginType(Int32 value) { 
 hasLoginType = true;
 loginType_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasMid) {
size += pb::CodedOutputStream.ComputeStringSize(1, Mid);
}
 if (HasVersion) {
size += pb::CodedOutputStream.ComputeStringSize(2, Version);
}
 if (HasPlatform) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Platform);
}
 if (HasSid) {
size += pb::CodedOutputStream.ComputeStringSize(4, Sid);
}
 if (HasArea) {
size += pb::CodedOutputStream.ComputeStringSize(5, Area);
}
 if (HasCountry) {
size += pb::CodedOutputStream.ComputeStringSize(6, Country);
}
 if (HasDevice) {
size += pb::CodedOutputStream.ComputeStringSize(7, Device);
}
 if (HasDeviceSystem) {
size += pb::CodedOutputStream.ComputeStringSize(8, DeviceSystem);
}
 if (HasNetworkType) {
size += pb::CodedOutputStream.ComputeStringSize(9, NetworkType);
}
 if (HasOperator) {
size += pb::CodedOutputStream.ComputeStringSize(11, Operator);
}
 if (HasServerid) {
size += pb::CodedOutputStream.ComputeInt32Size(12, Serverid);
}
{
foreach (ButtonInfo element in infoList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)16) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 if (HasLastAccountId) {
size += pb::CodedOutputStream.ComputeStringSize(17, LastAccountId);
}
 if (HasLoginType) {
size += pb::CodedOutputStream.ComputeInt32Size(18, LoginType);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasMid) {
output.WriteString(1, Mid);
}
 
if (HasVersion) {
output.WriteString(2, Version);
}
 
if (HasPlatform) {
output.WriteInt32(3, Platform);
}
 
if (HasSid) {
output.WriteString(4, Sid);
}
 
if (HasArea) {
output.WriteString(5, Area);
}
 
if (HasCountry) {
output.WriteString(6, Country);
}
 
if (HasDevice) {
output.WriteString(7, Device);
}
 
if (HasDeviceSystem) {
output.WriteString(8, DeviceSystem);
}
 
if (HasNetworkType) {
output.WriteString(9, NetworkType);
}
 
if (HasOperator) {
output.WriteString(11, Operator);
}
 
if (HasServerid) {
output.WriteInt32(12, Serverid);
}

do{
foreach (ButtonInfo element in infoList) {
output.WriteTag((int)16, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 
if (HasLastAccountId) {
output.WriteString(17, LastAccountId);
}
 
if (HasLoginType) {
output.WriteInt32(18, LoginType);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSLoginThirdPlatform _inst = (CSLoginThirdPlatform) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  10: {
 _inst.Mid = input.ReadString();
break;
}
   case  18: {
 _inst.Version = input.ReadString();
break;
}
   case  24: {
 _inst.Platform = input.ReadInt32();
break;
}
   case  34: {
 _inst.Sid = input.ReadString();
break;
}
   case  42: {
 _inst.Area = input.ReadString();
break;
}
   case  50: {
 _inst.Country = input.ReadString();
break;
}
   case  58: {
 _inst.Device = input.ReadString();
break;
}
   case  66: {
 _inst.DeviceSystem = input.ReadString();
break;
}
   case  74: {
 _inst.NetworkType = input.ReadString();
break;
}
   case  90: {
 _inst.Operator = input.ReadString();
break;
}
   case  96: {
 _inst.Serverid = input.ReadInt32();
break;
}
    case  130: {
ButtonInfo subBuilder =  new ButtonInfo();
input.ReadMessage(subBuilder);
_inst.AddInfo(subBuilder);
break;
}
   case  138: {
 _inst.LastAccountId = input.ReadString();
break;
}
   case  144: {
 _inst.LoginType = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasMid) return false;
 if (!hasVersion) return false;
 if (!hasPlatform) return false;
 if (!hasSid) return false;
foreach (ButtonInfo element in infoList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class CSMailDelete : PacketDistributed
{

public const int mail_idFieldNumber = 1;
 private bool hasMail_id;
 private Int64 mail_id_ = 0;
 public bool HasMail_id {
 get { return hasMail_id; }
 }
 public Int64 Mail_id {
 get { return mail_id_; }
 set { SetMail_id(value); }
 }
 public void SetMail_id(Int64 value) { 
 hasMail_id = true;
 mail_id_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasMail_id) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Mail_id);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasMail_id) {
output.WriteInt64(1, Mail_id);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSMailDelete _inst = (CSMailDelete) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Mail_id = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasMail_id) return false;
 return true;
 }

	}


[Serializable]
public class CSMailFriend : PacketDistributed
{

public const int mail_idFieldNumber = 1;
 private bool hasMail_id;
 private Int64 mail_id_ = 0;
 public bool HasMail_id {
 get { return hasMail_id; }
 }
 public Int64 Mail_id {
 get { return mail_id_; }
 set { SetMail_id(value); }
 }
 public void SetMail_id(Int64 value) { 
 hasMail_id = true;
 mail_id_ = value;
 }

public const int stateFieldNumber = 2;
 private bool hasState;
 private Int32 state_ = 0;
 public bool HasState {
 get { return hasState; }
 }
 public Int32 State {
 get { return state_; }
 set { SetState(value); }
 }
 public void SetState(Int32 value) { 
 hasState = true;
 state_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasMail_id) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Mail_id);
}
 if (HasState) {
size += pb::CodedOutputStream.ComputeInt32Size(2, State);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasMail_id) {
output.WriteInt64(1, Mail_id);
}
 
if (HasState) {
output.WriteInt32(2, State);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSMailFriend _inst = (CSMailFriend) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Mail_id = input.ReadInt64();
break;
}
   case  16: {
 _inst.State = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasMail_id) return false;
 if (!hasState) return false;
 return true;
 }

	}


[Serializable]
public class CSMailList : PacketDistributed
{

public const int player_guidFieldNumber = 1;
 private bool hasPlayer_guid;
 private Int64 player_guid_ = 0;
 public bool HasPlayer_guid {
 get { return hasPlayer_guid; }
 }
 public Int64 Player_guid {
 get { return player_guid_; }
 set { SetPlayer_guid(value); }
 }
 public void SetPlayer_guid(Int64 value) { 
 hasPlayer_guid = true;
 player_guid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasPlayer_guid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Player_guid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasPlayer_guid) {
output.WriteInt64(1, Player_guid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSMailList _inst = (CSMailList) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Player_guid = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasPlayer_guid) return false;
 return true;
 }

	}


[Serializable]
public class CSMailRead : PacketDistributed
{

public const int mail_idFieldNumber = 1;
 private bool hasMail_id;
 private Int64 mail_id_ = 0;
 public bool HasMail_id {
 get { return hasMail_id; }
 }
 public Int64 Mail_id {
 get { return mail_id_; }
 set { SetMail_id(value); }
 }
 public void SetMail_id(Int64 value) { 
 hasMail_id = true;
 mail_id_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasMail_id) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Mail_id);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasMail_id) {
output.WriteInt64(1, Mail_id);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSMailRead _inst = (CSMailRead) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Mail_id = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasMail_id) return false;
 return true;
 }

	}


[Serializable]
public class CSMailSend : PacketDistributed
{

public const int friend_idFieldNumber = 1;
 private bool hasFriend_id;
 private Int64 friend_id_ = 0;
 public bool HasFriend_id {
 get { return hasFriend_id; }
 }
 public Int64 Friend_id {
 get { return friend_id_; }
 set { SetFriend_id(value); }
 }
 public void SetFriend_id(Int64 value) { 
 hasFriend_id = true;
 friend_id_ = value;
 }

public const int contentFieldNumber = 2;
 private bool hasContent;
 private string content_ = "";
 public bool HasContent {
 get { return hasContent; }
 }
 public string Content {
 get { return content_; }
 set { SetContent(value); }
 }
 public void SetContent(string value) { 
 hasContent = true;
 content_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasFriend_id) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Friend_id);
}
 if (HasContent) {
size += pb::CodedOutputStream.ComputeStringSize(2, Content);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasFriend_id) {
output.WriteInt64(1, Friend_id);
}
 
if (HasContent) {
output.WriteString(2, Content);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSMailSend _inst = (CSMailSend) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Friend_id = input.ReadInt64();
break;
}
   case  18: {
 _inst.Content = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasFriend_id) return false;
 if (!hasContent) return false;
 return true;
 }

	}


[Serializable]
public class CSMailSystem : PacketDistributed
{

public const int mail_idFieldNumber = 1;
 private bool hasMail_id;
 private Int64 mail_id_ = 0;
 public bool HasMail_id {
 get { return hasMail_id; }
 }
 public Int64 Mail_id {
 get { return mail_id_; }
 set { SetMail_id(value); }
 }
 public void SetMail_id(Int64 value) { 
 hasMail_id = true;
 mail_id_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasMail_id) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Mail_id);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasMail_id) {
output.WriteInt64(1, Mail_id);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSMailSystem _inst = (CSMailSystem) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Mail_id = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasMail_id) return false;
 return true;
 }

	}


[Serializable]
public class CSMonthCardGetDollar : PacketDistributed
{

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSMonthCardGetDollar _inst = (CSMonthCardGetDollar) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
 
 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class CSMonthCardInfo : PacketDistributed
{

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSMonthCardInfo _inst = (CSMonthCardInfo) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
 
 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class CSPPProductList : PacketDistributed
{

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSPPProductList _inst = (CSPPProductList) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
 
 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class CSPPVerifyCharge : PacketDistributed
{

public const int goodsIdFieldNumber = 1;
 private bool hasGoodsId;
 private Int32 goodsId_ = 0;
 public bool HasGoodsId {
 get { return hasGoodsId; }
 }
 public Int32 GoodsId {
 get { return goodsId_; }
 set { SetGoodsId(value); }
 }
 public void SetGoodsId(Int32 value) { 
 hasGoodsId = true;
 goodsId_ = value;
 }

public const int productIdFieldNumber = 2;
 private bool hasProductId;
 private string productId_ = "";
 public bool HasProductId {
 get { return hasProductId; }
 }
 public string ProductId {
 get { return productId_; }
 set { SetProductId(value); }
 }
 public void SetProductId(string value) { 
 hasProductId = true;
 productId_ = value;
 }

public const int priceFieldNumber = 3;
 private bool hasPrice;
 private Int32 price_ = 0;
 public bool HasPrice {
 get { return hasPrice; }
 }
 public Int32 Price {
 get { return price_; }
 set { SetPrice(value); }
 }
 public void SetPrice(Int32 value) { 
 hasPrice = true;
 price_ = value;
 }

public const int orderIDFieldNumber = 4;
 private bool hasOrderID;
 private string orderID_ = "";
 public bool HasOrderID {
 get { return hasOrderID; }
 }
 public string OrderID {
 get { return orderID_; }
 set { SetOrderID(value); }
 }
 public void SetOrderID(string value) { 
 hasOrderID = true;
 orderID_ = value;
 }

public const int userIDFieldNumber = 5;
 private bool hasUserID;
 private string userID_ = "";
 public bool HasUserID {
 get { return hasUserID; }
 }
 public string UserID {
 get { return userID_; }
 set { SetUserID(value); }
 }
 public void SetUserID(string value) { 
 hasUserID = true;
 userID_ = value;
 }

public const int zoneIDFieldNumber = 6;
 private bool hasZoneID;
 private Int32 zoneID_ = 0;
 public bool HasZoneID {
 get { return hasZoneID; }
 }
 public Int32 ZoneID {
 get { return zoneID_; }
 set { SetZoneID(value); }
 }
 public void SetZoneID(Int32 value) { 
 hasZoneID = true;
 zoneID_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasGoodsId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, GoodsId);
}
 if (HasProductId) {
size += pb::CodedOutputStream.ComputeStringSize(2, ProductId);
}
 if (HasPrice) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Price);
}
 if (HasOrderID) {
size += pb::CodedOutputStream.ComputeStringSize(4, OrderID);
}
 if (HasUserID) {
size += pb::CodedOutputStream.ComputeStringSize(5, UserID);
}
 if (HasZoneID) {
size += pb::CodedOutputStream.ComputeInt32Size(6, ZoneID);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasGoodsId) {
output.WriteInt32(1, GoodsId);
}
 
if (HasProductId) {
output.WriteString(2, ProductId);
}
 
if (HasPrice) {
output.WriteInt32(3, Price);
}
 
if (HasOrderID) {
output.WriteString(4, OrderID);
}
 
if (HasUserID) {
output.WriteString(5, UserID);
}
 
if (HasZoneID) {
output.WriteInt32(6, ZoneID);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSPPVerifyCharge _inst = (CSPPVerifyCharge) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.GoodsId = input.ReadInt32();
break;
}
   case  18: {
 _inst.ProductId = input.ReadString();
break;
}
   case  24: {
 _inst.Price = input.ReadInt32();
break;
}
   case  34: {
 _inst.OrderID = input.ReadString();
break;
}
   case  42: {
 _inst.UserID = input.ReadString();
break;
}
   case  48: {
 _inst.ZoneID = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasGoodsId) return false;
 if (!hasProductId) return false;
 if (!hasPrice) return false;
 if (!hasOrderID) return false;
 if (!hasUserID) return false;
 if (!hasZoneID) return false;
 return true;
 }

	}


[Serializable]
public class CSPVPBattleData : PacketDistributed
{

public const int guidFieldNumber = 1;
 private bool hasGuid;
 private Int64 guid_ = 0;
 public bool HasGuid {
 get { return hasGuid; }
 }
 public Int64 Guid {
 get { return guid_; }
 set { SetGuid(value); }
 }
 public void SetGuid(Int64 value) { 
 hasGuid = true;
 guid_ = value;
 }

public const int roundFieldNumber = 2;
 private bool hasRound;
 private Int32 round_ = 0;
 public bool HasRound {
 get { return hasRound; }
 }
 public Int32 Round {
 get { return round_; }
 set { SetRound(value); }
 }
 public void SetRound(Int32 value) { 
 hasRound = true;
 round_ = value;
 }

public const int infoFieldNumber = 3;
 private pbc::PopsicleList<BattleCard> info_ = new pbc::PopsicleList<BattleCard>();
 public scg::IList<BattleCard> infoList {
 get { return pbc::Lists.AsReadOnly(info_); }
 }
 
 public int infoCount {
 get { return info_.Count; }
 }
 
public BattleCard GetInfo(int index) {
 return info_[index];
 }
 public void AddInfo(BattleCard value) {
 info_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasGuid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Guid);
}
 if (HasRound) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Round);
}
{
foreach (BattleCard element in infoList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasGuid) {
output.WriteInt64(1, Guid);
}
 
if (HasRound) {
output.WriteInt32(2, Round);
}

do{
foreach (BattleCard element in infoList) {
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSPVPBattleData _inst = (CSPVPBattleData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Guid = input.ReadInt64();
break;
}
   case  16: {
 _inst.Round = input.ReadInt32();
break;
}
    case  26: {
BattleCard subBuilder =  new BattleCard();
input.ReadMessage(subBuilder);
_inst.AddInfo(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasGuid) return false;
 if (!hasRound) return false;
foreach (BattleCard element in infoList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class CSPVPShop : PacketDistributed
{

public const int player_guidFieldNumber = 1;
 private bool hasPlayer_guid;
 private Int32 player_guid_ = 0;
 public bool HasPlayer_guid {
 get { return hasPlayer_guid; }
 }
 public Int32 Player_guid {
 get { return player_guid_; }
 set { SetPlayer_guid(value); }
 }
 public void SetPlayer_guid(Int32 value) { 
 hasPlayer_guid = true;
 player_guid_ = value;
 }

public const int typeFieldNumber = 2;
 private bool hasType;
 private Int32 type_ = 0;
 public bool HasType {
 get { return hasType; }
 }
 public Int32 Type {
 get { return type_; }
 set { SetType(value); }
 }
 public void SetType(Int32 value) { 
 hasType = true;
 type_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasPlayer_guid) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Player_guid);
}
 if (HasType) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Type);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasPlayer_guid) {
output.WriteInt32(1, Player_guid);
}
 
if (HasType) {
output.WriteInt32(2, Type);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSPVPShop _inst = (CSPVPShop) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Player_guid = input.ReadInt32();
break;
}
   case  16: {
 _inst.Type = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasPlayer_guid) return false;
 if (!hasType) return false;
 return true;
 }

	}


[Serializable]
public class CSPaiTaBattleData : PacketDistributed
{

public const int guidFieldNumber = 1;
 private bool hasGuid;
 private Int64 guid_ = 0;
 public bool HasGuid {
 get { return hasGuid; }
 }
 public Int64 Guid {
 get { return guid_; }
 set { SetGuid(value); }
 }
 public void SetGuid(Int64 value) { 
 hasGuid = true;
 guid_ = value;
 }

public const int roundFieldNumber = 2;
 private bool hasRound;
 private Int32 round_ = 0;
 public bool HasRound {
 get { return hasRound; }
 }
 public Int32 Round {
 get { return round_; }
 set { SetRound(value); }
 }
 public void SetRound(Int32 value) { 
 hasRound = true;
 round_ = value;
 }

public const int infoFieldNumber = 3;
 private pbc::PopsicleList<BattleCard> info_ = new pbc::PopsicleList<BattleCard>();
 public scg::IList<BattleCard> infoList {
 get { return pbc::Lists.AsReadOnly(info_); }
 }
 
 public int infoCount {
 get { return info_.Count; }
 }
 
public BattleCard GetInfo(int index) {
 return info_[index];
 }
 public void AddInfo(BattleCard value) {
 info_.Add(value);
 }

public const int numFieldNumber = 4;
 private bool hasNum;
 private Int32 num_ = 0;
 public bool HasNum {
 get { return hasNum; }
 }
 public Int32 Num {
 get { return num_; }
 set { SetNum(value); }
 }
 public void SetNum(Int32 value) { 
 hasNum = true;
 num_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasGuid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Guid);
}
 if (HasRound) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Round);
}
{
foreach (BattleCard element in infoList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 if (HasNum) {
size += pb::CodedOutputStream.ComputeInt32Size(4, Num);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasGuid) {
output.WriteInt64(1, Guid);
}
 
if (HasRound) {
output.WriteInt32(2, Round);
}

do{
foreach (BattleCard element in infoList) {
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 
if (HasNum) {
output.WriteInt32(4, Num);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSPaiTaBattleData _inst = (CSPaiTaBattleData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Guid = input.ReadInt64();
break;
}
   case  16: {
 _inst.Round = input.ReadInt32();
break;
}
    case  26: {
BattleCard subBuilder =  new BattleCard();
input.ReadMessage(subBuilder);
_inst.AddInfo(subBuilder);
break;
}
   case  32: {
 _inst.Num = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasGuid) return false;
 if (!hasRound) return false;
foreach (BattleCard element in infoList) {
if (!element.IsInitialized()) return false;
}
 if (!hasNum) return false;
 return true;
 }

	}


[Serializable]
public class CSProductList : PacketDistributed
{

public const int guidFieldNumber = 1;
 private bool hasGuid;
 private Int64 guid_ = 0;
 public bool HasGuid {
 get { return hasGuid; }
 }
 public Int64 Guid {
 get { return guid_; }
 set { SetGuid(value); }
 }
 public void SetGuid(Int64 value) { 
 hasGuid = true;
 guid_ = value;
 }

public const int platformFieldNumber = 3;
 private bool hasPlatform;
 private Int32 platform_ = 0;
 public bool HasPlatform {
 get { return hasPlatform; }
 }
 public Int32 Platform {
 get { return platform_; }
 set { SetPlatform(value); }
 }
 public void SetPlatform(Int32 value) { 
 hasPlatform = true;
 platform_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasGuid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Guid);
}
 if (HasPlatform) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Platform);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasGuid) {
output.WriteInt64(1, Guid);
}
 
if (HasPlatform) {
output.WriteInt32(3, Platform);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSProductList _inst = (CSProductList) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Guid = input.ReadInt64();
break;
}
   case  24: {
 _inst.Platform = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class CSQxzbBattle : PacketDistributed
{

public const int playerIdFieldNumber = 1;
 private bool hasPlayerId;
 private Int32 playerId_ = 0;
 public bool HasPlayerId {
 get { return hasPlayerId; }
 }
 public Int32 PlayerId {
 get { return playerId_; }
 set { SetPlayerId(value); }
 }
 public void SetPlayerId(Int32 value) { 
 hasPlayerId = true;
 playerId_ = value;
 }

public const int defierIdFieldNumber = 2;
 private bool hasDefierId;
 private Int32 defierId_ = 0;
 public bool HasDefierId {
 get { return hasDefierId; }
 }
 public Int32 DefierId {
 get { return defierId_; }
 set { SetDefierId(value); }
 }
 public void SetDefierId(Int32 value) { 
 hasDefierId = true;
 defierId_ = value;
 }

public const int starFieldNumber = 3;
 private bool hasStar;
 private Int32 star_ = 0;
 public bool HasStar {
 get { return hasStar; }
 }
 public Int32 Star {
 get { return star_; }
 set { SetStar(value); }
 }
 public void SetStar(Int32 value) { 
 hasStar = true;
 star_ = value;
 }

public const int infoFieldNumber = 4;
 private pbc::PopsicleList<BattleCard> info_ = new pbc::PopsicleList<BattleCard>();
 public scg::IList<BattleCard> infoList {
 get { return pbc::Lists.AsReadOnly(info_); }
 }
 
 public int infoCount {
 get { return info_.Count; }
 }
 
public BattleCard GetInfo(int index) {
 return info_[index];
 }
 public void AddInfo(BattleCard value) {
 info_.Add(value);
 }

public const int leaderIDFieldNumber = 5;
 private bool hasLeaderID;
 private Int64 leaderID_ = 0;
 public bool HasLeaderID {
 get { return hasLeaderID; }
 }
 public Int64 LeaderID {
 get { return leaderID_; }
 set { SetLeaderID(value); }
 }
 public void SetLeaderID(Int64 value) { 
 hasLeaderID = true;
 leaderID_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasPlayerId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, PlayerId);
}
 if (HasDefierId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, DefierId);
}
 if (HasStar) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Star);
}
{
foreach (BattleCard element in infoList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)4) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 if (HasLeaderID) {
size += pb::CodedOutputStream.ComputeInt64Size(5, LeaderID);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasPlayerId) {
output.WriteInt32(1, PlayerId);
}
 
if (HasDefierId) {
output.WriteInt32(2, DefierId);
}
 
if (HasStar) {
output.WriteInt32(3, Star);
}

do{
foreach (BattleCard element in infoList) {
output.WriteTag((int)4, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 
if (HasLeaderID) {
output.WriteInt64(5, LeaderID);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSQxzbBattle _inst = (CSQxzbBattle) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.PlayerId = input.ReadInt32();
break;
}
   case  16: {
 _inst.DefierId = input.ReadInt32();
break;
}
   case  24: {
 _inst.Star = input.ReadInt32();
break;
}
    case  34: {
BattleCard subBuilder =  new BattleCard();
input.ReadMessage(subBuilder);
_inst.AddInfo(subBuilder);
break;
}
   case  40: {
 _inst.LeaderID = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasPlayerId) return false;
 if (!hasDefierId) return false;
 if (!hasStar) return false;
foreach (BattleCard element in infoList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class CSQxzbGetReward : PacketDistributed
{

public const int playerIdFieldNumber = 1;
 private bool hasPlayerId;
 private Int32 playerId_ = 0;
 public bool HasPlayerId {
 get { return hasPlayerId; }
 }
 public Int32 PlayerId {
 get { return playerId_; }
 set { SetPlayerId(value); }
 }
 public void SetPlayerId(Int32 value) { 
 hasPlayerId = true;
 playerId_ = value;
 }

public const int starFieldNumber = 2;
 private bool hasStar;
 private Int32 star_ = 0;
 public bool HasStar {
 get { return hasStar; }
 }
 public Int32 Star {
 get { return star_; }
 set { SetStar(value); }
 }
 public void SetStar(Int32 value) { 
 hasStar = true;
 star_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasPlayerId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, PlayerId);
}
 if (HasStar) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Star);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasPlayerId) {
output.WriteInt32(1, PlayerId);
}
 
if (HasStar) {
output.WriteInt32(2, Star);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSQxzbGetReward _inst = (CSQxzbGetReward) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.PlayerId = input.ReadInt32();
break;
}
   case  16: {
 _inst.Star = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasPlayerId) return false;
 if (!hasStar) return false;
 return true;
 }

	}


[Serializable]
public class CSQxzbPVPClearCD : PacketDistributed
{

public const int playerIdFieldNumber = 1;
 private bool hasPlayerId;
 private Int64 playerId_ = 0;
 public bool HasPlayerId {
 get { return hasPlayerId; }
 }
 public Int64 PlayerId {
 get { return playerId_; }
 set { SetPlayerId(value); }
 }
 public void SetPlayerId(Int64 value) { 
 hasPlayerId = true;
 playerId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasPlayerId) {
size += pb::CodedOutputStream.ComputeInt64Size(1, PlayerId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasPlayerId) {
output.WriteInt64(1, PlayerId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSQxzbPVPClearCD _inst = (CSQxzbPVPClearCD) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.PlayerId = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasPlayerId) return false;
 return true;
 }

	}


[Serializable]
public class CSQxzbPVPDataAsk : PacketDistributed
{

public const int playerIdFieldNumber = 1;
 private bool hasPlayerId;
 private Int64 playerId_ = 0;
 public bool HasPlayerId {
 get { return hasPlayerId; }
 }
 public Int64 PlayerId {
 get { return playerId_; }
 set { SetPlayerId(value); }
 }
 public void SetPlayerId(Int64 value) { 
 hasPlayerId = true;
 playerId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasPlayerId) {
size += pb::CodedOutputStream.ComputeInt64Size(1, PlayerId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasPlayerId) {
output.WriteInt64(1, PlayerId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSQxzbPVPDataAsk _inst = (CSQxzbPVPDataAsk) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.PlayerId = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasPlayerId) return false;
 return true;
 }

	}


[Serializable]
public class CSRandomCardFree : PacketDistributed
{

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSRandomCardFree _inst = (CSRandomCardFree) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
 
 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class CSSDKLoginThirdPlatform : PacketDistributed
{

public const int midFieldNumber = 1;
 private bool hasMid;
 private string mid_ = "";
 public bool HasMid {
 get { return hasMid; }
 }
 public string Mid {
 get { return mid_; }
 set { SetMid(value); }
 }
 public void SetMid(string value) { 
 hasMid = true;
 mid_ = value;
 }

public const int versionFieldNumber = 2;
 private bool hasVersion;
 private string version_ = "";
 public bool HasVersion {
 get { return hasVersion; }
 }
 public string Version {
 get { return version_; }
 set { SetVersion(value); }
 }
 public void SetVersion(string value) { 
 hasVersion = true;
 version_ = value;
 }

public const int platformFieldNumber = 3;
 private bool hasPlatform;
 private Int32 platform_ = 0;
 public bool HasPlatform {
 get { return hasPlatform; }
 }
 public Int32 Platform {
 get { return platform_; }
 set { SetPlatform(value); }
 }
 public void SetPlatform(Int32 value) { 
 hasPlatform = true;
 platform_ = value;
 }

public const int sidFieldNumber = 4;
 private bool hasSid;
 private string sid_ = "";
 public bool HasSid {
 get { return hasSid; }
 }
 public string Sid {
 get { return sid_; }
 set { SetSid(value); }
 }
 public void SetSid(string value) { 
 hasSid = true;
 sid_ = value;
 }

public const int areaFieldNumber = 5;
 private bool hasArea;
 private string area_ = "";
 public bool HasArea {
 get { return hasArea; }
 }
 public string Area {
 get { return area_; }
 set { SetArea(value); }
 }
 public void SetArea(string value) { 
 hasArea = true;
 area_ = value;
 }

public const int countryFieldNumber = 6;
 private bool hasCountry;
 private string country_ = "";
 public bool HasCountry {
 get { return hasCountry; }
 }
 public string Country {
 get { return country_; }
 set { SetCountry(value); }
 }
 public void SetCountry(string value) { 
 hasCountry = true;
 country_ = value;
 }

public const int deviceFieldNumber = 7;
 private bool hasDevice;
 private string device_ = "";
 public bool HasDevice {
 get { return hasDevice; }
 }
 public string Device {
 get { return device_; }
 set { SetDevice(value); }
 }
 public void SetDevice(string value) { 
 hasDevice = true;
 device_ = value;
 }

public const int deviceSystemFieldNumber = 8;
 private bool hasDeviceSystem;
 private string deviceSystem_ = "";
 public bool HasDeviceSystem {
 get { return hasDeviceSystem; }
 }
 public string DeviceSystem {
 get { return deviceSystem_; }
 set { SetDeviceSystem(value); }
 }
 public void SetDeviceSystem(string value) { 
 hasDeviceSystem = true;
 deviceSystem_ = value;
 }

public const int networkTypeFieldNumber = 9;
 private bool hasNetworkType;
 private string networkType_ = "";
 public bool HasNetworkType {
 get { return hasNetworkType; }
 }
 public string NetworkType {
 get { return networkType_; }
 set { SetNetworkType(value); }
 }
 public void SetNetworkType(string value) { 
 hasNetworkType = true;
 networkType_ = value;
 }

public const int operatorFieldNumber = 11;
 private bool hasOperator;
 private string operator_ = "";
 public bool HasOperator {
 get { return hasOperator; }
 }
 public string Operator {
 get { return operator_; }
 set { SetOperator(value); }
 }
 public void SetOperator(string value) { 
 hasOperator = true;
 operator_ = value;
 }

public const int serveridFieldNumber = 12;
 private bool hasServerid;
 private Int32 serverid_ = 0;
 public bool HasServerid {
 get { return hasServerid; }
 }
 public Int32 Serverid {
 get { return serverid_; }
 set { SetServerid(value); }
 }
 public void SetServerid(Int32 value) { 
 hasServerid = true;
 serverid_ = value;
 }

public const int infoFieldNumber = 16;
 private pbc::PopsicleList<ButtonInfo> info_ = new pbc::PopsicleList<ButtonInfo>();
 public scg::IList<ButtonInfo> infoList {
 get { return pbc::Lists.AsReadOnly(info_); }
 }
 
 public int infoCount {
 get { return info_.Count; }
 }
 
public ButtonInfo GetInfo(int index) {
 return info_[index];
 }
 public void AddInfo(ButtonInfo value) {
 info_.Add(value);
 }

public const int lastAccountIdFieldNumber = 17;
 private bool hasLastAccountId;
 private string lastAccountId_ = "";
 public bool HasLastAccountId {
 get { return hasLastAccountId; }
 }
 public string LastAccountId {
 get { return lastAccountId_; }
 set { SetLastAccountId(value); }
 }
 public void SetLastAccountId(string value) { 
 hasLastAccountId = true;
 lastAccountId_ = value;
 }

public const int loginTypeFieldNumber = 18;
 private bool hasLoginType;
 private Int32 loginType_ = 0;
 public bool HasLoginType {
 get { return hasLoginType; }
 }
 public Int32 LoginType {
 get { return loginType_; }
 set { SetLoginType(value); }
 }
 public void SetLoginType(Int32 value) { 
 hasLoginType = true;
 loginType_ = value;
 }

public const int downloadTypeFieldNumber = 19;
 private bool hasDownloadType;
 private string downloadType_ = "";
 public bool HasDownloadType {
 get { return hasDownloadType; }
 }
 public string DownloadType {
 get { return downloadType_; }
 set { SetDownloadType(value); }
 }
 public void SetDownloadType(string value) { 
 hasDownloadType = true;
 downloadType_ = value;
 }

public const int jsonDataFieldNumber = 20;
 private bool hasJsonData;
 private string jsonData_ = "";
 public bool HasJsonData {
 get { return hasJsonData; }
 }
 public string JsonData {
 get { return jsonData_; }
 set { SetJsonData(value); }
 }
 public void SetJsonData(string value) { 
 hasJsonData = true;
 jsonData_ = value;
 }

public const int prisonBreakFieldNumber = 21;
 private bool hasPrisonBreak;
 private string prisonBreak_ = "";
 public bool HasPrisonBreak {
 get { return hasPrisonBreak; }
 }
 public string PrisonBreak {
 get { return prisonBreak_; }
 set { SetPrisonBreak(value); }
 }
 public void SetPrisonBreak(string value) { 
 hasPrisonBreak = true;
 prisonBreak_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasMid) {
size += pb::CodedOutputStream.ComputeStringSize(1, Mid);
}
 if (HasVersion) {
size += pb::CodedOutputStream.ComputeStringSize(2, Version);
}
 if (HasPlatform) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Platform);
}
 if (HasSid) {
size += pb::CodedOutputStream.ComputeStringSize(4, Sid);
}
 if (HasArea) {
size += pb::CodedOutputStream.ComputeStringSize(5, Area);
}
 if (HasCountry) {
size += pb::CodedOutputStream.ComputeStringSize(6, Country);
}
 if (HasDevice) {
size += pb::CodedOutputStream.ComputeStringSize(7, Device);
}
 if (HasDeviceSystem) {
size += pb::CodedOutputStream.ComputeStringSize(8, DeviceSystem);
}
 if (HasNetworkType) {
size += pb::CodedOutputStream.ComputeStringSize(9, NetworkType);
}
 if (HasOperator) {
size += pb::CodedOutputStream.ComputeStringSize(11, Operator);
}
 if (HasServerid) {
size += pb::CodedOutputStream.ComputeInt32Size(12, Serverid);
}
{
foreach (ButtonInfo element in infoList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)16) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 if (HasLastAccountId) {
size += pb::CodedOutputStream.ComputeStringSize(17, LastAccountId);
}
 if (HasLoginType) {
size += pb::CodedOutputStream.ComputeInt32Size(18, LoginType);
}
 if (HasDownloadType) {
size += pb::CodedOutputStream.ComputeStringSize(19, DownloadType);
}
 if (HasJsonData) {
size += pb::CodedOutputStream.ComputeStringSize(20, JsonData);
}
 if (HasPrisonBreak) {
size += pb::CodedOutputStream.ComputeStringSize(21, PrisonBreak);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasMid) {
output.WriteString(1, Mid);
}
 
if (HasVersion) {
output.WriteString(2, Version);
}
 
if (HasPlatform) {
output.WriteInt32(3, Platform);
}
 
if (HasSid) {
output.WriteString(4, Sid);
}
 
if (HasArea) {
output.WriteString(5, Area);
}
 
if (HasCountry) {
output.WriteString(6, Country);
}
 
if (HasDevice) {
output.WriteString(7, Device);
}
 
if (HasDeviceSystem) {
output.WriteString(8, DeviceSystem);
}
 
if (HasNetworkType) {
output.WriteString(9, NetworkType);
}
 
if (HasOperator) {
output.WriteString(11, Operator);
}
 
if (HasServerid) {
output.WriteInt32(12, Serverid);
}

do{
foreach (ButtonInfo element in infoList) {
output.WriteTag((int)16, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 
if (HasLastAccountId) {
output.WriteString(17, LastAccountId);
}
 
if (HasLoginType) {
output.WriteInt32(18, LoginType);
}
 
if (HasDownloadType) {
output.WriteString(19, DownloadType);
}
 
if (HasJsonData) {
output.WriteString(20, JsonData);
}
 
if (HasPrisonBreak) {
output.WriteString(21, PrisonBreak);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSSDKLoginThirdPlatform _inst = (CSSDKLoginThirdPlatform) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  10: {
 _inst.Mid = input.ReadString();
break;
}
   case  18: {
 _inst.Version = input.ReadString();
break;
}
   case  24: {
 _inst.Platform = input.ReadInt32();
break;
}
   case  34: {
 _inst.Sid = input.ReadString();
break;
}
   case  42: {
 _inst.Area = input.ReadString();
break;
}
   case  50: {
 _inst.Country = input.ReadString();
break;
}
   case  58: {
 _inst.Device = input.ReadString();
break;
}
   case  66: {
 _inst.DeviceSystem = input.ReadString();
break;
}
   case  74: {
 _inst.NetworkType = input.ReadString();
break;
}
   case  90: {
 _inst.Operator = input.ReadString();
break;
}
   case  96: {
 _inst.Serverid = input.ReadInt32();
break;
}
    case  130: {
ButtonInfo subBuilder =  new ButtonInfo();
input.ReadMessage(subBuilder);
_inst.AddInfo(subBuilder);
break;
}
   case  138: {
 _inst.LastAccountId = input.ReadString();
break;
}
   case  144: {
 _inst.LoginType = input.ReadInt32();
break;
}
   case  154: {
 _inst.DownloadType = input.ReadString();
break;
}
   case  162: {
 _inst.JsonData = input.ReadString();
break;
}
   case  170: {
 _inst.PrisonBreak = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasMid) return false;
 if (!hasVersion) return false;
 if (!hasPlatform) return false;
 if (!hasSid) return false;
foreach (ButtonInfo element in infoList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class CSSDKRefresh : PacketDistributed
{

public const int platformFieldNumber = 1;
 private bool hasPlatform;
 private Int32 platform_ = 0;
 public bool HasPlatform {
 get { return hasPlatform; }
 }
 public Int32 Platform {
 get { return platform_; }
 set { SetPlatform(value); }
 }
 public void SetPlatform(Int32 value) { 
 hasPlatform = true;
 platform_ = value;
 }

public const int jsonDataFieldNumber = 2;
 private bool hasJsonData;
 private string jsonData_ = "";
 public bool HasJsonData {
 get { return hasJsonData; }
 }
 public string JsonData {
 get { return jsonData_; }
 set { SetJsonData(value); }
 }
 public void SetJsonData(string value) { 
 hasJsonData = true;
 jsonData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasPlatform) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Platform);
}
 if (HasJsonData) {
size += pb::CodedOutputStream.ComputeStringSize(2, JsonData);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasPlatform) {
output.WriteInt32(1, Platform);
}
 
if (HasJsonData) {
output.WriteString(2, JsonData);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSSDKRefresh _inst = (CSSDKRefresh) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Platform = input.ReadInt32();
break;
}
   case  18: {
 _inst.JsonData = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasPlatform) return false;
 return true;
 }

	}


[Serializable]
public class CSSearchFriend : PacketDistributed
{

public const int friend_guidFieldNumber = 1;
 private bool hasFriend_guid;
 private Int64 friend_guid_ = 0;
 public bool HasFriend_guid {
 get { return hasFriend_guid; }
 }
 public Int64 Friend_guid {
 get { return friend_guid_; }
 set { SetFriend_guid(value); }
 }
 public void SetFriend_guid(Int64 value) { 
 hasFriend_guid = true;
 friend_guid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasFriend_guid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Friend_guid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasFriend_guid) {
output.WriteInt64(1, Friend_guid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSSearchFriend _inst = (CSSearchFriend) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Friend_guid = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasFriend_guid) return false;
 return true;
 }

	}


[Serializable]
public class CSSellCard : PacketDistributed
{

public const int card_guidFieldNumber = 1;
 private pbc::PopsicleList<Int64> card_guid_ = new pbc::PopsicleList<Int64>();
 public scg::IList<Int64> card_guidList {
 get { return pbc::Lists.AsReadOnly(card_guid_); }
 }
 
 public int card_guidCount {
 get { return card_guid_.Count; }
 }
 
public Int64 GetCard_guid(int index) {
 return card_guid_[index];
 }
 public void AddCard_guid(Int64 value) {
 card_guid_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int dataSize = 0;
foreach (Int64 element in card_guidList) {
dataSize += pb::CodedOutputStream.ComputeInt64SizeNoTag(element);
}
size += dataSize;
size += 1 * card_guid_.Count;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
if (card_guid_.Count > 0) {
foreach (Int64 element in card_guidList) {
output.WriteInt64(1,element);
}
}

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSSellCard _inst = (CSSellCard) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.AddCard_guid(input.ReadInt64());
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class CSShop : PacketDistributed
{

public const int player_guidFieldNumber = 1;
 private bool hasPlayer_guid;
 private Int32 player_guid_ = 0;
 public bool HasPlayer_guid {
 get { return hasPlayer_guid; }
 }
 public Int32 Player_guid {
 get { return player_guid_; }
 set { SetPlayer_guid(value); }
 }
 public void SetPlayer_guid(Int32 value) { 
 hasPlayer_guid = true;
 player_guid_ = value;
 }

public const int typeFieldNumber = 2;
 private bool hasType;
 private Int32 type_ = 0;
 public bool HasType {
 get { return hasType; }
 }
 public Int32 Type {
 get { return type_; }
 set { SetType(value); }
 }
 public void SetType(Int32 value) { 
 hasType = true;
 type_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasPlayer_guid) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Player_guid);
}
 if (HasType) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Type);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasPlayer_guid) {
output.WriteInt32(1, Player_guid);
}
 
if (HasType) {
output.WriteInt32(2, Type);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSShop _inst = (CSShop) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Player_guid = input.ReadInt32();
break;
}
   case  16: {
 _inst.Type = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasPlayer_guid) return false;
 if (!hasType) return false;
 return true;
 }

	}


[Serializable]
public class CSStudySkill : PacketDistributed
{

public const int hero_cardguidFieldNumber = 1;
 private bool hasHero_cardguid;
 private Int64 hero_cardguid_ = 0;
 public bool HasHero_cardguid {
 get { return hasHero_cardguid; }
 }
 public Int64 Hero_cardguid {
 get { return hero_cardguid_; }
 set { SetHero_cardguid(value); }
 }
 public void SetHero_cardguid(Int64 value) { 
 hasHero_cardguid = true;
 hero_cardguid_ = value;
 }

public const int swallowed_cardguidFieldNumber = 2;
 private bool hasSwallowed_cardguid;
 private Int64 swallowed_cardguid_ = 0;
 public bool HasSwallowed_cardguid {
 get { return hasSwallowed_cardguid; }
 }
 public Int64 Swallowed_cardguid {
 get { return swallowed_cardguid_; }
 set { SetSwallowed_cardguid(value); }
 }
 public void SetSwallowed_cardguid(Int64 value) { 
 hasSwallowed_cardguid = true;
 swallowed_cardguid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasHero_cardguid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Hero_cardguid);
}
 if (HasSwallowed_cardguid) {
size += pb::CodedOutputStream.ComputeInt64Size(2, Swallowed_cardguid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasHero_cardguid) {
output.WriteInt64(1, Hero_cardguid);
}
 
if (HasSwallowed_cardguid) {
output.WriteInt64(2, Swallowed_cardguid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSStudySkill _inst = (CSStudySkill) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Hero_cardguid = input.ReadInt64();
break;
}
   case  16: {
 _inst.Swallowed_cardguid = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasHero_cardguid) return false;
 if (!hasSwallowed_cardguid) return false;
 return true;
 }

	}


[Serializable]
public class CSStudySkillUpdate : PacketDistributed
{

public const int hero_cardguidFieldNumber = 1;
 private bool hasHero_cardguid;
 private Int64 hero_cardguid_ = 0;
 public bool HasHero_cardguid {
 get { return hasHero_cardguid; }
 }
 public Int64 Hero_cardguid {
 get { return hero_cardguid_; }
 set { SetHero_cardguid(value); }
 }
 public void SetHero_cardguid(Int64 value) { 
 hasHero_cardguid = true;
 hero_cardguid_ = value;
 }

public const int swallowed_cardguidFieldNumber = 2;
 private pbc::PopsicleList<Int64> swallowed_cardguid_ = new pbc::PopsicleList<Int64>();
 public scg::IList<Int64> swallowed_cardguidList {
 get { return pbc::Lists.AsReadOnly(swallowed_cardguid_); }
 }
 
 public int swallowed_cardguidCount {
 get { return swallowed_cardguid_.Count; }
 }
 
public Int64 GetSwallowed_cardguid(int index) {
 return swallowed_cardguid_[index];
 }
 public void AddSwallowed_cardguid(Int64 value) {
 swallowed_cardguid_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasHero_cardguid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Hero_cardguid);
}
{
int dataSize = 0;
foreach (Int64 element in swallowed_cardguidList) {
dataSize += pb::CodedOutputStream.ComputeInt64SizeNoTag(element);
}
size += dataSize;
size += 1 * swallowed_cardguid_.Count;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasHero_cardguid) {
output.WriteInt64(1, Hero_cardguid);
}
{
if (swallowed_cardguid_.Count > 0) {
foreach (Int64 element in swallowed_cardguidList) {
output.WriteInt64(2,element);
}
}

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSStudySkillUpdate _inst = (CSStudySkillUpdate) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Hero_cardguid = input.ReadInt64();
break;
}
   case  16: {
 _inst.AddSwallowed_cardguid(input.ReadInt64());
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasHero_cardguid) return false;
 return true;
 }

	}


[Serializable]
public class CSTaskList : PacketDistributed
{

public const int guidFieldNumber = 1;
 private bool hasGuid;
 private Int64 guid_ = 0;
 public bool HasGuid {
 get { return hasGuid; }
 }
 public Int64 Guid {
 get { return guid_; }
 set { SetGuid(value); }
 }
 public void SetGuid(Int64 value) { 
 hasGuid = true;
 guid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasGuid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Guid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasGuid) {
output.WriteInt64(1, Guid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSTaskList _inst = (CSTaskList) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Guid = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasGuid) return false;
 return true;
 }

	}


[Serializable]
public class CSTaskOver : PacketDistributed
{

public const int TaskIdFieldNumber = 1;
 private bool hasTaskId;
 private Int32 TaskId_ = 0;
 public bool HasTaskId {
 get { return hasTaskId; }
 }
 public Int32 TaskId {
 get { return TaskId_; }
 set { SetTaskId(value); }
 }
 public void SetTaskId(Int32 value) { 
 hasTaskId = true;
 TaskId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasTaskId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, TaskId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasTaskId) {
output.WriteInt32(1, TaskId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSTaskOver _inst = (CSTaskOver) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.TaskId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasTaskId) return false;
 return true;
 }

	}


[Serializable]
public class CSThirdPlatformVerifyCharge : PacketDistributed
{

public const int goodsIdFieldNumber = 1;
 private bool hasGoodsId;
 private Int32 goodsId_ = 0;
 public bool HasGoodsId {
 get { return hasGoodsId; }
 }
 public Int32 GoodsId {
 get { return goodsId_; }
 set { SetGoodsId(value); }
 }
 public void SetGoodsId(Int32 value) { 
 hasGoodsId = true;
 goodsId_ = value;
 }

public const int productIdFieldNumber = 2;
 private bool hasProductId;
 private string productId_ = "";
 public bool HasProductId {
 get { return hasProductId; }
 }
 public string ProductId {
 get { return productId_; }
 set { SetProductId(value); }
 }
 public void SetProductId(string value) { 
 hasProductId = true;
 productId_ = value;
 }

public const int orderIdFieldNumber = 3;
 private bool hasOrderId;
 private string orderId_ = "";
 public bool HasOrderId {
 get { return hasOrderId; }
 }
 public string OrderId {
 get { return orderId_; }
 set { SetOrderId(value); }
 }
 public void SetOrderId(string value) { 
 hasOrderId = true;
 orderId_ = value;
 }

public const int platformFieldNumber = 4;
 private bool hasPlatform;
 private Int32 platform_ = 0;
 public bool HasPlatform {
 get { return hasPlatform; }
 }
 public Int32 Platform {
 get { return platform_; }
 set { SetPlatform(value); }
 }
 public void SetPlatform(Int32 value) { 
 hasPlatform = true;
 platform_ = value;
 }

public const int goodpriceFieldNumber = 5;
 private bool hasGoodprice;
 private Int32 goodprice_ = 0;
 public bool HasGoodprice {
 get { return hasGoodprice; }
 }
 public Int32 Goodprice {
 get { return goodprice_; }
 set { SetGoodprice(value); }
 }
 public void SetGoodprice(Int32 value) { 
 hasGoodprice = true;
 goodprice_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasGoodsId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, GoodsId);
}
 if (HasProductId) {
size += pb::CodedOutputStream.ComputeStringSize(2, ProductId);
}
 if (HasOrderId) {
size += pb::CodedOutputStream.ComputeStringSize(3, OrderId);
}
 if (HasPlatform) {
size += pb::CodedOutputStream.ComputeInt32Size(4, Platform);
}
 if (HasGoodprice) {
size += pb::CodedOutputStream.ComputeInt32Size(5, Goodprice);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasGoodsId) {
output.WriteInt32(1, GoodsId);
}
 
if (HasProductId) {
output.WriteString(2, ProductId);
}
 
if (HasOrderId) {
output.WriteString(3, OrderId);
}
 
if (HasPlatform) {
output.WriteInt32(4, Platform);
}
 
if (HasGoodprice) {
output.WriteInt32(5, Goodprice);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSThirdPlatformVerifyCharge _inst = (CSThirdPlatformVerifyCharge) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.GoodsId = input.ReadInt32();
break;
}
   case  18: {
 _inst.ProductId = input.ReadString();
break;
}
   case  26: {
 _inst.OrderId = input.ReadString();
break;
}
   case  32: {
 _inst.Platform = input.ReadInt32();
break;
}
   case  40: {
 _inst.Goodprice = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasGoodsId) return false;
 if (!hasProductId) return false;
 if (!hasOrderId) return false;
 if (!hasPlatform) return false;
 if (!hasGoodprice) return false;
 return true;
 }

	}


[Serializable]
public class CSWorldBossAddZhufu : PacketDistributed
{

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSWorldBossAddZhufu _inst = (CSWorldBossAddZhufu) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
 
 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class CSWorldBossResurgence : PacketDistributed
{

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSWorldBossResurgence _inst = (CSWorldBossResurgence) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
 
 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class CSWorldBossWeekRank : PacketDistributed
{

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSWorldBossWeekRank _inst = (CSWorldBossWeekRank) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
 
 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class CSWorldBossWeekReward : PacketDistributed
{

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSWorldBossWeekReward _inst = (CSWorldBossWeekReward) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
 
 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class CSWuxingActivation : PacketDistributed
{

public const int idFieldNumber = 1;
 private bool hasId;
 private Int32 id_ = 0;
 public bool HasId {
 get { return hasId; }
 }
 public Int32 Id {
 get { return id_; }
 set { SetId(value); }
 }
 public void SetId(Int32 value) { 
 hasId = true;
 id_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Id);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasId) {
output.WriteInt32(1, Id);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSWuxingActivation _inst = (CSWuxingActivation) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Id = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasId) return false;
 return true;
 }

	}


[Serializable]
public class CSWuxingLevelup : PacketDistributed
{

public const int idFieldNumber = 1;
 private bool hasId;
 private Int32 id_ = 0;
 public bool HasId {
 get { return hasId; }
 }
 public Int32 Id {
 get { return id_; }
 set { SetId(value); }
 }
 public void SetId(Int32 value) { 
 hasId = true;
 id_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Id);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasId) {
output.WriteInt32(1, Id);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSWuxingLevelup _inst = (CSWuxingLevelup) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Id = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasId) return false;
 return true;
 }

	}


[Serializable]
public class CSWuxingReset : PacketDistributed
{

public const int idFieldNumber = 1;
 private bool hasId;
 private Int32 id_ = 0;
 public bool HasId {
 get { return hasId; }
 }
 public Int32 Id {
 get { return id_; }
 set { SetId(value); }
 }
 public void SetId(Int32 value) { 
 hasId = true;
 id_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Id);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasId) {
output.WriteInt32(1, Id);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSWuxingReset _inst = (CSWuxingReset) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Id = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasId) return false;
 return true;
 }

	}


[Serializable]
public class CSYunyingHuodong : PacketDistributed
{

public const int accountIdFieldNumber = 1;
 private bool hasAccountId;
 private Int64 accountId_ = 0;
 public bool HasAccountId {
 get { return hasAccountId; }
 }
 public Int64 AccountId {
 get { return accountId_; }
 set { SetAccountId(value); }
 }
 public void SetAccountId(Int64 value) { 
 hasAccountId = true;
 accountId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasAccountId) {
size += pb::CodedOutputStream.ComputeInt64Size(1, AccountId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasAccountId) {
output.WriteInt64(1, AccountId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSYunyingHuodong _inst = (CSYunyingHuodong) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.AccountId = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasAccountId) return false;
 return true;
 }

	}


[Serializable]
public class CSscode : PacketDistributed
{

public const int scodeFieldNumber = 1;
 private bool hasScode;
 private string scode_ = "";
 public bool HasScode {
 get { return hasScode; }
 }
 public string Scode {
 get { return scode_; }
 set { SetScode(value); }
 }
 public void SetScode(string value) { 
 hasScode = true;
 scode_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasScode) {
size += pb::CodedOutputStream.ComputeStringSize(1, Scode);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasScode) {
output.WriteString(1, Scode);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CSscode _inst = (CSscode) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  10: {
 _inst.Scode = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasScode) return false;
 return true;
 }

	}


[Serializable]
public class CardInfo : PacketDistributed
{

public const int cardIdFieldNumber = 1;
 private bool hasCardId;
 private Int64 cardId_ = 0;
 public bool HasCardId {
 get { return hasCardId; }
 }
 public Int64 CardId {
 get { return cardId_; }
 set { SetCardId(value); }
 }
 public void SetCardId(Int64 value) { 
 hasCardId = true;
 cardId_ = value;
 }

public const int templateIdFieldNumber = 2;
 private bool hasTemplateId;
 private Int32 templateId_ = 0;
 public bool HasTemplateId {
 get { return hasTemplateId; }
 }
 public Int32 TemplateId {
 get { return templateId_; }
 set { SetTemplateId(value); }
 }
 public void SetTemplateId(Int32 value) { 
 hasTemplateId = true;
 templateId_ = value;
 }

public const int levelFieldNumber = 3;
 private bool hasLevel;
 private Int32 level_ = 0;
 public bool HasLevel {
 get { return hasLevel; }
 }
 public Int32 Level {
 get { return level_; }
 set { SetLevel(value); }
 }
 public void SetLevel(Int32 value) { 
 hasLevel = true;
 level_ = value;
 }

public const int skill_levelFieldNumber = 4;
 private bool hasSkill_level;
 private Int32 skill_level_ = 0;
 public bool HasSkill_level {
 get { return hasSkill_level; }
 }
 public Int32 Skill_level {
 get { return skill_level_; }
 set { SetSkill_level(value); }
 }
 public void SetSkill_level(Int32 value) { 
 hasSkill_level = true;
 skill_level_ = value;
 }

public const int add_quality_attFieldNumber = 5;
 private bool hasAdd_quality_att;
 private Int32 add_quality_att_ = 0;
 public bool HasAdd_quality_att {
 get { return hasAdd_quality_att; }
 }
 public Int32 Add_quality_att {
 get { return add_quality_att_; }
 set { SetAdd_quality_att(value); }
 }
 public void SetAdd_quality_att(Int32 value) { 
 hasAdd_quality_att = true;
 add_quality_att_ = value;
 }

public const int add_quality_hpFieldNumber = 6;
 private bool hasAdd_quality_hp;
 private Int32 add_quality_hp_ = 0;
 public bool HasAdd_quality_hp {
 get { return hasAdd_quality_hp; }
 }
 public Int32 Add_quality_hp {
 get { return add_quality_hp_; }
 set { SetAdd_quality_hp(value); }
 }
 public void SetAdd_quality_hp(Int32 value) { 
 hasAdd_quality_hp = true;
 add_quality_hp_ = value;
 }

public const int fightIndexFieldNumber = 7;
 private bool hasFightIndex;
 private Int32 fightIndex_ = 0;
 public bool HasFightIndex {
 get { return hasFightIndex; }
 }
 public Int32 FightIndex {
 get { return fightIndex_; }
 set { SetFightIndex(value); }
 }
 public void SetFightIndex(Int32 value) { 
 hasFightIndex = true;
 fightIndex_ = value;
 }

public const int curLevExpFieldNumber = 8;
 private bool hasCurLevExp;
 private Int32 curLevExp_ = 0;
 public bool HasCurLevExp {
 get { return hasCurLevExp; }
 }
 public Int32 CurLevExp {
 get { return curLevExp_; }
 set { SetCurLevExp(value); }
 }
 public void SetCurLevExp(Int32 value) { 
 hasCurLevExp = true;
 curLevExp_ = value;
 }

public const int memberIDFieldNumber = 9;
 private bool hasMemberID;
 private Int32 memberID_ = 0;
 public bool HasMemberID {
 get { return hasMemberID; }
 }
 public Int32 MemberID {
 get { return memberID_; }
 set { SetMemberID(value); }
 }
 public void SetMemberID(Int32 value) { 
 hasMemberID = true;
 memberID_ = value;
 }

public const int studySkillIDFieldNumber = 10;
 private bool hasStudySkillID;
 private Int32 studySkillID_ = 0;
 public bool HasStudySkillID {
 get { return hasStudySkillID; }
 }
 public Int32 StudySkillID {
 get { return studySkillID_; }
 set { SetStudySkillID(value); }
 }
 public void SetStudySkillID(Int32 value) { 
 hasStudySkillID = true;
 studySkillID_ = value;
 }

public const int studySkillLevFieldNumber = 11;
 private bool hasStudySkillLev;
 private Int32 studySkillLev_ = 0;
 public bool HasStudySkillLev {
 get { return hasStudySkillLev; }
 }
 public Int32 StudySkillLev {
 get { return studySkillLev_; }
 set { SetStudySkillLev(value); }
 }
 public void SetStudySkillLev(Int32 value) { 
 hasStudySkillLev = true;
 studySkillLev_ = value;
 }

public const int studySkillcurLevExpFieldNumber = 12;
 private bool hasStudySkillcurLevExp;
 private Int32 studySkillcurLevExp_ = 0;
 public bool HasStudySkillcurLevExp {
 get { return hasStudySkillcurLevExp; }
 }
 public Int32 StudySkillcurLevExp {
 get { return studySkillcurLevExp_; }
 set { SetStudySkillcurLevExp(value); }
 }
 public void SetStudySkillcurLevExp(Int32 value) { 
 hasStudySkillcurLevExp = true;
 studySkillcurLevExp_ = value;
 }

public const int QxzbFightIndexFieldNumber = 13;
 private bool hasQxzbFightIndex;
 private Int32 QxzbFightIndex_ = 0;
 public bool HasQxzbFightIndex {
 get { return hasQxzbFightIndex; }
 }
 public Int32 QxzbFightIndex {
 get { return QxzbFightIndex_; }
 set { SetQxzbFightIndex(value); }
 }
 public void SetQxzbFightIndex(Int32 value) { 
 hasQxzbFightIndex = true;
 QxzbFightIndex_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasCardId) {
size += pb::CodedOutputStream.ComputeInt64Size(1, CardId);
}
 if (HasTemplateId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, TemplateId);
}
 if (HasLevel) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Level);
}
 if (HasSkill_level) {
size += pb::CodedOutputStream.ComputeInt32Size(4, Skill_level);
}
 if (HasAdd_quality_att) {
size += pb::CodedOutputStream.ComputeInt32Size(5, Add_quality_att);
}
 if (HasAdd_quality_hp) {
size += pb::CodedOutputStream.ComputeInt32Size(6, Add_quality_hp);
}
 if (HasFightIndex) {
size += pb::CodedOutputStream.ComputeInt32Size(7, FightIndex);
}
 if (HasCurLevExp) {
size += pb::CodedOutputStream.ComputeInt32Size(8, CurLevExp);
}
 if (HasMemberID) {
size += pb::CodedOutputStream.ComputeInt32Size(9, MemberID);
}
 if (HasStudySkillID) {
size += pb::CodedOutputStream.ComputeInt32Size(10, StudySkillID);
}
 if (HasStudySkillLev) {
size += pb::CodedOutputStream.ComputeInt32Size(11, StudySkillLev);
}
 if (HasStudySkillcurLevExp) {
size += pb::CodedOutputStream.ComputeInt32Size(12, StudySkillcurLevExp);
}
 if (HasQxzbFightIndex) {
size += pb::CodedOutputStream.ComputeInt32Size(13, QxzbFightIndex);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasCardId) {
output.WriteInt64(1, CardId);
}
 
if (HasTemplateId) {
output.WriteInt32(2, TemplateId);
}
 
if (HasLevel) {
output.WriteInt32(3, Level);
}
 
if (HasSkill_level) {
output.WriteInt32(4, Skill_level);
}
 
if (HasAdd_quality_att) {
output.WriteInt32(5, Add_quality_att);
}
 
if (HasAdd_quality_hp) {
output.WriteInt32(6, Add_quality_hp);
}
 
if (HasFightIndex) {
output.WriteInt32(7, FightIndex);
}
 
if (HasCurLevExp) {
output.WriteInt32(8, CurLevExp);
}
 
if (HasMemberID) {
output.WriteInt32(9, MemberID);
}
 
if (HasStudySkillID) {
output.WriteInt32(10, StudySkillID);
}
 
if (HasStudySkillLev) {
output.WriteInt32(11, StudySkillLev);
}
 
if (HasStudySkillcurLevExp) {
output.WriteInt32(12, StudySkillcurLevExp);
}
 
if (HasQxzbFightIndex) {
output.WriteInt32(13, QxzbFightIndex);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CardInfo _inst = (CardInfo) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.CardId = input.ReadInt64();
break;
}
   case  16: {
 _inst.TemplateId = input.ReadInt32();
break;
}
   case  24: {
 _inst.Level = input.ReadInt32();
break;
}
   case  32: {
 _inst.Skill_level = input.ReadInt32();
break;
}
   case  40: {
 _inst.Add_quality_att = input.ReadInt32();
break;
}
   case  48: {
 _inst.Add_quality_hp = input.ReadInt32();
break;
}
   case  56: {
 _inst.FightIndex = input.ReadInt32();
break;
}
   case  64: {
 _inst.CurLevExp = input.ReadInt32();
break;
}
   case  72: {
 _inst.MemberID = input.ReadInt32();
break;
}
   case  80: {
 _inst.StudySkillID = input.ReadInt32();
break;
}
   case  88: {
 _inst.StudySkillLev = input.ReadInt32();
break;
}
   case  96: {
 _inst.StudySkillcurLevExp = input.ReadInt32();
break;
}
   case  104: {
 _inst.QxzbFightIndex = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasCardId) return false;
 if (!hasTemplateId) return false;
 if (!hasLevel) return false;
 if (!hasSkill_level) return false;
 if (!hasCurLevExp) return false;
 if (!hasMemberID) return false;
 if (!hasStudySkillID) return false;
 if (!hasStudySkillLev) return false;
 if (!hasStudySkillcurLevExp) return false;
 return true;
 }

	}


[Serializable]
public class ChangeCardInfo : PacketDistributed
{

public const int changeType_1FieldNumber = 1;
 private bool hasChangeType_1;
 private Int32 changeType_1_ = 0;
 public bool HasChangeType_1 {
 get { return hasChangeType_1; }
 }
 public Int32 ChangeType_1 {
 get { return changeType_1_; }
 set { SetChangeType_1(value); }
 }
 public void SetChangeType_1(Int32 value) { 
 hasChangeType_1 = true;
 changeType_1_ = value;
 }

public const int changeNum_1FieldNumber = 2;
 private bool hasChangeNum_1;
 private Int32 changeNum_1_ = 0;
 public bool HasChangeNum_1 {
 get { return hasChangeNum_1; }
 }
 public Int32 ChangeNum_1 {
 get { return changeNum_1_; }
 set { SetChangeNum_1(value); }
 }
 public void SetChangeNum_1(Int32 value) { 
 hasChangeNum_1 = true;
 changeNum_1_ = value;
 }

public const int changeType_2FieldNumber = 3;
 private bool hasChangeType_2;
 private Int32 changeType_2_ = 0;
 public bool HasChangeType_2 {
 get { return hasChangeType_2; }
 }
 public Int32 ChangeType_2 {
 get { return changeType_2_; }
 set { SetChangeType_2(value); }
 }
 public void SetChangeType_2(Int32 value) { 
 hasChangeType_2 = true;
 changeType_2_ = value;
 }

public const int changeNum_2FieldNumber = 4;
 private bool hasChangeNum_2;
 private Int32 changeNum_2_ = 0;
 public bool HasChangeNum_2 {
 get { return hasChangeNum_2; }
 }
 public Int32 ChangeNum_2 {
 get { return changeNum_2_; }
 set { SetChangeNum_2(value); }
 }
 public void SetChangeNum_2(Int32 value) { 
 hasChangeNum_2 = true;
 changeNum_2_ = value;
 }

public const int changeType_3FieldNumber = 5;
 private bool hasChangeType_3;
 private Int32 changeType_3_ = 0;
 public bool HasChangeType_3 {
 get { return hasChangeType_3; }
 }
 public Int32 ChangeType_3 {
 get { return changeType_3_; }
 set { SetChangeType_3(value); }
 }
 public void SetChangeType_3(Int32 value) { 
 hasChangeType_3 = true;
 changeType_3_ = value;
 }

public const int changeNum_3FieldNumber = 6;
 private bool hasChangeNum_3;
 private Int32 changeNum_3_ = 0;
 public bool HasChangeNum_3 {
 get { return hasChangeNum_3; }
 }
 public Int32 ChangeNum_3 {
 get { return changeNum_3_; }
 set { SetChangeNum_3(value); }
 }
 public void SetChangeNum_3(Int32 value) { 
 hasChangeNum_3 = true;
 changeNum_3_ = value;
 }

public const int resultIDFieldNumber = 7;
 private bool hasResultID;
 private Int32 resultID_ = 0;
 public bool HasResultID {
 get { return hasResultID; }
 }
 public Int32 ResultID {
 get { return resultID_; }
 set { SetResultID(value); }
 }
 public void SetResultID(Int32 value) { 
 hasResultID = true;
 resultID_ = value;
 }

public const int cardInfoIDFieldNumber = 8;
 private bool hasCardInfoID;
 private Int32 cardInfoID_ = 0;
 public bool HasCardInfoID {
 get { return hasCardInfoID; }
 }
 public Int32 CardInfoID {
 get { return cardInfoID_; }
 set { SetCardInfoID(value); }
 }
 public void SetCardInfoID(Int32 value) { 
 hasCardInfoID = true;
 cardInfoID_ = value;
 }

public const int timesFieldNumber = 9;
 private bool hasTimes;
 private Int32 times_ = 0;
 public bool HasTimes {
 get { return hasTimes; }
 }
 public Int32 Times {
 get { return times_; }
 set { SetTimes(value); }
 }
 public void SetTimes(Int32 value) { 
 hasTimes = true;
 times_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasChangeType_1) {
size += pb::CodedOutputStream.ComputeInt32Size(1, ChangeType_1);
}
 if (HasChangeNum_1) {
size += pb::CodedOutputStream.ComputeInt32Size(2, ChangeNum_1);
}
 if (HasChangeType_2) {
size += pb::CodedOutputStream.ComputeInt32Size(3, ChangeType_2);
}
 if (HasChangeNum_2) {
size += pb::CodedOutputStream.ComputeInt32Size(4, ChangeNum_2);
}
 if (HasChangeType_3) {
size += pb::CodedOutputStream.ComputeInt32Size(5, ChangeType_3);
}
 if (HasChangeNum_3) {
size += pb::CodedOutputStream.ComputeInt32Size(6, ChangeNum_3);
}
 if (HasResultID) {
size += pb::CodedOutputStream.ComputeInt32Size(7, ResultID);
}
 if (HasCardInfoID) {
size += pb::CodedOutputStream.ComputeInt32Size(8, CardInfoID);
}
 if (HasTimes) {
size += pb::CodedOutputStream.ComputeInt32Size(9, Times);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasChangeType_1) {
output.WriteInt32(1, ChangeType_1);
}
 
if (HasChangeNum_1) {
output.WriteInt32(2, ChangeNum_1);
}
 
if (HasChangeType_2) {
output.WriteInt32(3, ChangeType_2);
}
 
if (HasChangeNum_2) {
output.WriteInt32(4, ChangeNum_2);
}
 
if (HasChangeType_3) {
output.WriteInt32(5, ChangeType_3);
}
 
if (HasChangeNum_3) {
output.WriteInt32(6, ChangeNum_3);
}
 
if (HasResultID) {
output.WriteInt32(7, ResultID);
}
 
if (HasCardInfoID) {
output.WriteInt32(8, CardInfoID);
}
 
if (HasTimes) {
output.WriteInt32(9, Times);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 ChangeCardInfo _inst = (ChangeCardInfo) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.ChangeType_1 = input.ReadInt32();
break;
}
   case  16: {
 _inst.ChangeNum_1 = input.ReadInt32();
break;
}
   case  24: {
 _inst.ChangeType_2 = input.ReadInt32();
break;
}
   case  32: {
 _inst.ChangeNum_2 = input.ReadInt32();
break;
}
   case  40: {
 _inst.ChangeType_3 = input.ReadInt32();
break;
}
   case  48: {
 _inst.ChangeNum_3 = input.ReadInt32();
break;
}
   case  56: {
 _inst.ResultID = input.ReadInt32();
break;
}
   case  64: {
 _inst.CardInfoID = input.ReadInt32();
break;
}
   case  72: {
 _inst.Times = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class CopyInfo : PacketDistributed
{

public const int copyIdFieldNumber = 1;
 private bool hasCopyId;
 private Int32 copyId_ = 0;
 public bool HasCopyId {
 get { return hasCopyId; }
 }
 public Int32 CopyId {
 get { return copyId_; }
 set { SetCopyId(value); }
 }
 public void SetCopyId(Int32 value) { 
 hasCopyId = true;
 copyId_ = value;
 }

public const int start_rest_timeFieldNumber = 2;
 private bool hasStart_rest_time;
 private Int32 start_rest_time_ = 0;
 public bool HasStart_rest_time {
 get { return hasStart_rest_time; }
 }
 public Int32 Start_rest_time {
 get { return start_rest_time_; }
 set { SetStart_rest_time(value); }
 }
 public void SetStart_rest_time(Int32 value) { 
 hasStart_rest_time = true;
 start_rest_time_ = value;
 }

public const int close_rest_timeFieldNumber = 3;
 private bool hasClose_rest_time;
 private Int32 close_rest_time_ = 0;
 public bool HasClose_rest_time {
 get { return hasClose_rest_time; }
 }
 public Int32 Close_rest_time {
 get { return close_rest_time_; }
 set { SetClose_rest_time(value); }
 }
 public void SetClose_rest_time(Int32 value) { 
 hasClose_rest_time = true;
 close_rest_time_ = value;
 }

public const int subcopysFieldNumber = 4;
 private pbc::PopsicleList<MissionInfo> subcopys_ = new pbc::PopsicleList<MissionInfo>();
 public scg::IList<MissionInfo> subcopysList {
 get { return pbc::Lists.AsReadOnly(subcopys_); }
 }
 
 public int subcopysCount {
 get { return subcopys_.Count; }
 }
 
public MissionInfo GetSubcopys(int index) {
 return subcopys_[index];
 }
 public void AddSubcopys(MissionInfo value) {
 subcopys_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasCopyId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, CopyId);
}
 if (HasStart_rest_time) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Start_rest_time);
}
 if (HasClose_rest_time) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Close_rest_time);
}
{
foreach (MissionInfo element in subcopysList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)4) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasCopyId) {
output.WriteInt32(1, CopyId);
}
 
if (HasStart_rest_time) {
output.WriteInt32(2, Start_rest_time);
}
 
if (HasClose_rest_time) {
output.WriteInt32(3, Close_rest_time);
}

do{
foreach (MissionInfo element in subcopysList) {
output.WriteTag((int)4, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CopyInfo _inst = (CopyInfo) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.CopyId = input.ReadInt32();
break;
}
   case  16: {
 _inst.Start_rest_time = input.ReadInt32();
break;
}
   case  24: {
 _inst.Close_rest_time = input.ReadInt32();
break;
}
    case  34: {
MissionInfo subBuilder =  new MissionInfo();
input.ReadMessage(subBuilder);
_inst.AddSubcopys(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasCopyId) return false;
foreach (MissionInfo element in subcopysList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class DataAction : PacketDistributed
{

public const int attacker_actionsFieldNumber = 1;
 private pbc::PopsicleList<DataSingleAction> attacker_actions_ = new pbc::PopsicleList<DataSingleAction>();
 public scg::IList<DataSingleAction> attacker_actionsList {
 get { return pbc::Lists.AsReadOnly(attacker_actions_); }
 }
 
 public int attacker_actionsCount {
 get { return attacker_actions_.Count; }
 }
 
public DataSingleAction GetAttacker_actions(int index) {
 return attacker_actions_[index];
 }
 public void AddAttacker_actions(DataSingleAction value) {
 attacker_actions_.Add(value);
 }

public const int be_attacker_actionsFieldNumber = 2;
 private pbc::PopsicleList<DataSingleAction> be_attacker_actions_ = new pbc::PopsicleList<DataSingleAction>();
 public scg::IList<DataSingleAction> be_attacker_actionsList {
 get { return pbc::Lists.AsReadOnly(be_attacker_actions_); }
 }
 
 public int be_attacker_actionsCount {
 get { return be_attacker_actions_.Count; }
 }
 
public DataSingleAction GetBe_attacker_actions(int index) {
 return be_attacker_actions_[index];
 }
 public void AddBe_attacker_actions(DataSingleAction value) {
 be_attacker_actions_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
foreach (DataSingleAction element in attacker_actionsList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
{
foreach (DataSingleAction element in be_attacker_actionsList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 
do{
foreach (DataSingleAction element in attacker_actionsList) {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);

do{
foreach (DataSingleAction element in be_attacker_actionsList) {
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 DataAction _inst = (DataAction) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
DataSingleAction subBuilder =  new DataSingleAction();
input.ReadMessage(subBuilder);
_inst.AddAttacker_actions(subBuilder);
break;
}
    case  18: {
DataSingleAction subBuilder =  new DataSingleAction();
input.ReadMessage(subBuilder);
_inst.AddBe_attacker_actions(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
 foreach (DataSingleAction element in attacker_actionsList) {
if (!element.IsInitialized()) return false;
}
foreach (DataSingleAction element in be_attacker_actionsList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class DataBattle : PacketDistributed
{

public const int userCardFieldNumber = 1;
 private pbc::PopsicleList<BattleCard> userCard_ = new pbc::PopsicleList<BattleCard>();
 public scg::IList<BattleCard> userCardList {
 get { return pbc::Lists.AsReadOnly(userCard_); }
 }
 
 public int userCardCount {
 get { return userCard_.Count; }
 }
 
public BattleCard GetUserCard(int index) {
 return userCard_[index];
 }
 public void AddUserCard(BattleCard value) {
 userCard_.Add(value);
 }

public const int roundsFieldNumber = 2;
 private pbc::PopsicleList<DataRound> rounds_ = new pbc::PopsicleList<DataRound>();
 public scg::IList<DataRound> roundsList {
 get { return pbc::Lists.AsReadOnly(rounds_); }
 }
 
 public int roundsCount {
 get { return rounds_.Count; }
 }
 
public DataRound GetRounds(int index) {
 return rounds_[index];
 }
 public void AddRounds(DataRound value) {
 rounds_.Add(value);
 }

public const int win_idxFieldNumber = 3;
 private bool hasWin_idx;
 private Int32 win_idx_ = 0;
 public bool HasWin_idx {
 get { return hasWin_idx; }
 }
 public Int32 Win_idx {
 get { return win_idx_; }
 set { SetWin_idx(value); }
 }
 public void SetWin_idx(Int32 value) { 
 hasWin_idx = true;
 win_idx_ = value;
 }

public const int missionIDFieldNumber = 4;
 private bool hasMissionID;
 private Int64 missionID_ = 0;
 public bool HasMissionID {
 get { return hasMissionID; }
 }
 public Int64 MissionID {
 get { return missionID_; }
 set { SetMissionID(value); }
 }
 public void SetMissionID(Int64 value) { 
 hasMissionID = true;
 missionID_ = value;
 }

public const int round_countFieldNumber = 5;
 private bool hasRound_count;
 private Int32 round_count_ = 0;
 public bool HasRound_count {
 get { return hasRound_count; }
 }
 public Int32 Round_count {
 get { return round_count_; }
 set { SetRound_count(value); }
 }
 public void SetRound_count(Int32 value) { 
 hasRound_count = true;
 round_count_ = value;
 }

public const int dropsFieldNumber = 6;
 private pbc::PopsicleList<DropBag> drops_ = new pbc::PopsicleList<DropBag>();
 public scg::IList<DropBag> dropsList {
 get { return pbc::Lists.AsReadOnly(drops_); }
 }
 
 public int dropsCount {
 get { return drops_.Count; }
 }
 
public DropBag GetDrops(int index) {
 return drops_[index];
 }
 public void AddDrops(DropBag value) {
 drops_.Add(value);
 }

public const int add_expFieldNumber = 7;
 private bool hasAdd_exp;
 private Int32 add_exp_ = 0;
 public bool HasAdd_exp {
 get { return hasAdd_exp; }
 }
 public Int32 Add_exp {
 get { return add_exp_; }
 set { SetAdd_exp(value); }
 }
 public void SetAdd_exp(Int32 value) { 
 hasAdd_exp = true;
 add_exp_ = value;
 }

public const int friendguidFieldNumber = 8;
 private bool hasFriendguid;
 private UInt64 friendguid_ = 0;
 public bool HasFriendguid {
 get { return hasFriendguid; }
 }
 public UInt64 Friendguid {
 get { return friendguid_; }
 set { SetFriendguid(value); }
 }
 public void SetFriendguid(UInt64 value) { 
 hasFriendguid = true;
 friendguid_ = value;
 }

public const int friendnameFieldNumber = 9;
 private bool hasFriendname;
 private string friendname_ = "";
 public bool HasFriendname {
 get { return hasFriendname; }
 }
 public string Friendname {
 get { return friendname_; }
 set { SetFriendname(value); }
 }
 public void SetFriendname(string value) { 
 hasFriendname = true;
 friendname_ = value;
 }

public const int friendlevelFieldNumber = 10;
 private bool hasFriendlevel;
 private Int32 friendlevel_ = 0;
 public bool HasFriendlevel {
 get { return hasFriendlevel; }
 }
 public Int32 Friendlevel {
 get { return friendlevel_; }
 set { SetFriendlevel(value); }
 }
 public void SetFriendlevel(Int32 value) { 
 hasFriendlevel = true;
 friendlevel_ = value;
 }

public const int friendCardLevFieldNumber = 11;
 private bool hasFriendCardLev;
 private Int32 friendCardLev_ = 0;
 public bool HasFriendCardLev {
 get { return hasFriendCardLev; }
 }
 public Int32 FriendCardLev {
 get { return friendCardLev_; }
 set { SetFriendCardLev(value); }
 }
 public void SetFriendCardLev(Int32 value) { 
 hasFriendCardLev = true;
 friendCardLev_ = value;
 }

public const int getFriendPointFieldNumber = 12;
 private bool hasGetFriendPoint;
 private Int32 getFriendPoint_ = 0;
 public bool HasGetFriendPoint {
 get { return hasGetFriendPoint; }
 }
 public Int32 GetFriendPoint {
 get { return getFriendPoint_; }
 set { SetGetFriendPoint(value); }
 }
 public void SetGetFriendPoint(Int32 value) { 
 hasGetFriendPoint = true;
 getFriendPoint_ = value;
 }

public const int isFriendFieldNumber = 13;
 private bool hasIsFriend;
 private Int32 isFriend_ = 0;
 public bool HasIsFriend {
 get { return hasIsFriend; }
 }
 public Int32 IsFriend {
 get { return isFriend_; }
 set { SetIsFriend(value); }
 }
 public void SetIsFriend(Int32 value) { 
 hasIsFriend = true;
 isFriend_ = value;
 }

public const int copyStarFieldNumber = 14;
 private bool hasCopyStar;
 private Int32 copyStar_ = 0;
 public bool HasCopyStar {
 get { return hasCopyStar; }
 }
 public Int32 CopyStar {
 get { return copyStar_; }
 set { SetCopyStar(value); }
 }
 public void SetCopyStar(Int32 value) { 
 hasCopyStar = true;
 copyStar_ = value;
 }

public const int whichPVEFieldNumber = 15;
 private bool hasWhichPVE;
 private Int32 whichPVE_ = 0;
 public bool HasWhichPVE {
 get { return hasWhichPVE; }
 }
 public Int32 WhichPVE {
 get { return whichPVE_; }
 set { SetWhichPVE(value); }
 }
 public void SetWhichPVE(Int32 value) { 
 hasWhichPVE = true;
 whichPVE_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
foreach (BattleCard element in userCardList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
{
foreach (DataRound element in roundsList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 if (HasWin_idx) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Win_idx);
}
 if (HasMissionID) {
size += pb::CodedOutputStream.ComputeInt64Size(4, MissionID);
}
 if (HasRound_count) {
size += pb::CodedOutputStream.ComputeInt32Size(5, Round_count);
}
{
foreach (DropBag element in dropsList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)6) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 if (HasAdd_exp) {
size += pb::CodedOutputStream.ComputeInt32Size(7, Add_exp);
}
 if (HasFriendguid) {
size += pb::CodedOutputStream.ComputeUInt64Size(8, Friendguid);
}
 if (HasFriendname) {
size += pb::CodedOutputStream.ComputeStringSize(9, Friendname);
}
 if (HasFriendlevel) {
size += pb::CodedOutputStream.ComputeInt32Size(10, Friendlevel);
}
 if (HasFriendCardLev) {
size += pb::CodedOutputStream.ComputeInt32Size(11, FriendCardLev);
}
 if (HasGetFriendPoint) {
size += pb::CodedOutputStream.ComputeInt32Size(12, GetFriendPoint);
}
 if (HasIsFriend) {
size += pb::CodedOutputStream.ComputeInt32Size(13, IsFriend);
}
 if (HasCopyStar) {
size += pb::CodedOutputStream.ComputeInt32Size(14, CopyStar);
}
 if (HasWhichPVE) {
size += pb::CodedOutputStream.ComputeInt32Size(15, WhichPVE);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 
do{
foreach (BattleCard element in userCardList) {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);

do{
foreach (DataRound element in roundsList) {
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 
if (HasWin_idx) {
output.WriteInt32(3, Win_idx);
}
 
if (HasMissionID) {
output.WriteInt64(4, MissionID);
}
 
if (HasRound_count) {
output.WriteInt32(5, Round_count);
}

do{
foreach (DropBag element in dropsList) {
output.WriteTag((int)6, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 
if (HasAdd_exp) {
output.WriteInt32(7, Add_exp);
}
 
if (HasFriendguid) {
output.WriteUInt64(8, Friendguid);
}
 
if (HasFriendname) {
output.WriteString(9, Friendname);
}
 
if (HasFriendlevel) {
output.WriteInt32(10, Friendlevel);
}
 
if (HasFriendCardLev) {
output.WriteInt32(11, FriendCardLev);
}
 
if (HasGetFriendPoint) {
output.WriteInt32(12, GetFriendPoint);
}
 
if (HasIsFriend) {
output.WriteInt32(13, IsFriend);
}
 
if (HasCopyStar) {
output.WriteInt32(14, CopyStar);
}
 
if (HasWhichPVE) {
output.WriteInt32(15, WhichPVE);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 DataBattle _inst = (DataBattle) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
BattleCard subBuilder =  new BattleCard();
input.ReadMessage(subBuilder);
_inst.AddUserCard(subBuilder);
break;
}
    case  18: {
DataRound subBuilder =  new DataRound();
input.ReadMessage(subBuilder);
_inst.AddRounds(subBuilder);
break;
}
   case  24: {
 _inst.Win_idx = input.ReadInt32();
break;
}
   case  32: {
 _inst.MissionID = input.ReadInt64();
break;
}
   case  40: {
 _inst.Round_count = input.ReadInt32();
break;
}
    case  50: {
DropBag subBuilder =  new DropBag();
input.ReadMessage(subBuilder);
_inst.AddDrops(subBuilder);
break;
}
   case  56: {
 _inst.Add_exp = input.ReadInt32();
break;
}
   case  64: {
 _inst.Friendguid = input.ReadUInt64();
break;
}
   case  74: {
 _inst.Friendname = input.ReadString();
break;
}
   case  80: {
 _inst.Friendlevel = input.ReadInt32();
break;
}
   case  88: {
 _inst.FriendCardLev = input.ReadInt32();
break;
}
   case  96: {
 _inst.GetFriendPoint = input.ReadInt32();
break;
}
   case  104: {
 _inst.IsFriend = input.ReadInt32();
break;
}
   case  112: {
 _inst.CopyStar = input.ReadInt32();
break;
}
   case  120: {
 _inst.WhichPVE = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
 foreach (BattleCard element in userCardList) {
if (!element.IsInitialized()) return false;
}
foreach (DataRound element in roundsList) {
if (!element.IsInitialized()) return false;
}
 if (!hasWin_idx) return false;
 if (!hasMissionID) return false;
 if (!hasRound_count) return false;
foreach (DropBag element in dropsList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class DataBuffInfo : PacketDistributed
{

public const int buf_idFieldNumber = 1;
 private bool hasBuf_id;
 private Int32 buf_id_ = 0;
 public bool HasBuf_id {
 get { return hasBuf_id; }
 }
 public Int32 Buf_id {
 get { return buf_id_; }
 set { SetBuf_id(value); }
 }
 public void SetBuf_id(Int32 value) { 
 hasBuf_id = true;
 buf_id_ = value;
 }

public const int buf_valueFieldNumber = 2;
 private bool hasBuf_value;
 private Int32 buf_value_ = 0;
 public bool HasBuf_value {
 get { return hasBuf_value; }
 }
 public Int32 Buf_value {
 get { return buf_value_; }
 set { SetBuf_value(value); }
 }
 public void SetBuf_value(Int32 value) { 
 hasBuf_value = true;
 buf_value_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasBuf_id) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Buf_id);
}
 if (HasBuf_value) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Buf_value);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasBuf_id) {
output.WriteInt32(1, Buf_id);
}
 
if (HasBuf_value) {
output.WriteInt32(2, Buf_value);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 DataBuffInfo _inst = (DataBuffInfo) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Buf_id = input.ReadInt32();
break;
}
   case  16: {
 _inst.Buf_value = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasBuf_id) return false;
 return true;
 }

	}


[Serializable]
public class DataRound : PacketDistributed
{

public const int actionsFieldNumber = 1;
 private pbc::PopsicleList<DataAction> actions_ = new pbc::PopsicleList<DataAction>();
 public scg::IList<DataAction> actionsList {
 get { return pbc::Lists.AsReadOnly(actions_); }
 }
 
 public int actionsCount {
 get { return actions_.Count; }
 }
 
public DataAction GetActions(int index) {
 return actions_[index];
 }
 public void AddActions(DataAction value) {
 actions_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
foreach (DataAction element in actionsList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 
do{
foreach (DataAction element in actionsList) {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 DataRound _inst = (DataRound) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
DataAction subBuilder =  new DataAction();
input.ReadMessage(subBuilder);
_inst.AddActions(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
 foreach (DataAction element in actionsList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class DataSingleAction : PacketDistributed
{

public const int card_idxFieldNumber = 1;
 private bool hasCard_idx;
 private Int32 card_idx_ = 0;
 public bool HasCard_idx {
 get { return hasCard_idx; }
 }
 public Int32 Card_idx {
 get { return card_idx_; }
 set { SetCard_idx(value); }
 }
 public void SetCard_idx(Int32 value) { 
 hasCard_idx = true;
 card_idx_ = value;
 }

public const int skillidFieldNumber = 2;
 private bool hasSkillid;
 private Int32 skillid_ = 0;
 public bool HasSkillid {
 get { return hasSkillid; }
 }
 public Int32 Skillid {
 get { return skillid_; }
 set { SetSkillid(value); }
 }
 public void SetSkillid(Int32 value) { 
 hasSkillid = true;
 skillid_ = value;
 }

public const int att_valueFieldNumber = 3;
 private bool hasAtt_value;
 private Int32 att_value_ = 0;
 public bool HasAtt_value {
 get { return hasAtt_value; }
 }
 public Int32 Att_value {
 get { return att_value_; }
 set { SetAtt_value(value); }
 }
 public void SetAtt_value(Int32 value) { 
 hasAtt_value = true;
 att_value_ = value;
 }

public const int buffInfoFieldNumber = 4;
 private pbc::PopsicleList<DataBuffInfo> buffInfo_ = new pbc::PopsicleList<DataBuffInfo>();
 public scg::IList<DataBuffInfo> buffInfoList {
 get { return pbc::Lists.AsReadOnly(buffInfo_); }
 }
 
 public int buffInfoCount {
 get { return buffInfo_.Count; }
 }
 
public DataBuffInfo GetBuffInfo(int index) {
 return buffInfo_[index];
 }
 public void AddBuffInfo(DataBuffInfo value) {
 buffInfo_.Add(value);
 }

public const int att_typeFieldNumber = 6;
 private bool hasAtt_type;
 private Int32 att_type_ = 0;
 public bool HasAtt_type {
 get { return hasAtt_type; }
 }
 public Int32 Att_type {
 get { return att_type_; }
 set { SetAtt_type(value); }
 }
 public void SetAtt_type(Int32 value) { 
 hasAtt_type = true;
 att_type_ = value;
 }

public const int beCritFieldNumber = 7;
 private bool hasBeCrit;
 private Int32 beCrit_ = 0;
 public bool HasBeCrit {
 get { return hasBeCrit; }
 }
 public Int32 BeCrit {
 get { return beCrit_; }
 set { SetBeCrit(value); }
 }
 public void SetBeCrit(Int32 value) { 
 hasBeCrit = true;
 beCrit_ = value;
 }

public const int cur_hpFieldNumber = 8;
 private bool hasCur_hp;
 private Int64 cur_hp_ = 0;
 public bool HasCur_hp {
 get { return hasCur_hp; }
 }
 public Int64 Cur_hp {
 get { return cur_hp_; }
 set { SetCur_hp(value); }
 }
 public void SetCur_hp(Int64 value) { 
 hasCur_hp = true;
 cur_hp_ = value;
 }

public const int heti_idxFieldNumber = 9;
 private bool hasHeti_idx;
 private Int32 heti_idx_ = 0;
 public bool HasHeti_idx {
 get { return hasHeti_idx; }
 }
 public Int32 Heti_idx {
 get { return heti_idx_; }
 set { SetHeti_idx(value); }
 }
 public void SetHeti_idx(Int32 value) { 
 hasHeti_idx = true;
 heti_idx_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasCard_idx) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Card_idx);
}
 if (HasSkillid) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Skillid);
}
 if (HasAtt_value) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Att_value);
}
{
foreach (DataBuffInfo element in buffInfoList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)4) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 if (HasAtt_type) {
size += pb::CodedOutputStream.ComputeInt32Size(6, Att_type);
}
 if (HasBeCrit) {
size += pb::CodedOutputStream.ComputeInt32Size(7, BeCrit);
}
 if (HasCur_hp) {
size += pb::CodedOutputStream.ComputeInt64Size(8, Cur_hp);
}
 if (HasHeti_idx) {
size += pb::CodedOutputStream.ComputeInt32Size(9, Heti_idx);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasCard_idx) {
output.WriteInt32(1, Card_idx);
}
 
if (HasSkillid) {
output.WriteInt32(2, Skillid);
}
 
if (HasAtt_value) {
output.WriteInt32(3, Att_value);
}

do{
foreach (DataBuffInfo element in buffInfoList) {
output.WriteTag((int)4, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 
if (HasAtt_type) {
output.WriteInt32(6, Att_type);
}
 
if (HasBeCrit) {
output.WriteInt32(7, BeCrit);
}
 
if (HasCur_hp) {
output.WriteInt64(8, Cur_hp);
}
 
if (HasHeti_idx) {
output.WriteInt32(9, Heti_idx);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 DataSingleAction _inst = (DataSingleAction) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Card_idx = input.ReadInt32();
break;
}
   case  16: {
 _inst.Skillid = input.ReadInt32();
break;
}
   case  24: {
 _inst.Att_value = input.ReadInt32();
break;
}
    case  34: {
DataBuffInfo subBuilder =  new DataBuffInfo();
input.ReadMessage(subBuilder);
_inst.AddBuffInfo(subBuilder);
break;
}
   case  48: {
 _inst.Att_type = input.ReadInt32();
break;
}
   case  56: {
 _inst.BeCrit = input.ReadInt32();
break;
}
   case  64: {
 _inst.Cur_hp = input.ReadInt64();
break;
}
   case  72: {
 _inst.Heti_idx = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasCard_idx) return false;
foreach (DataBuffInfo element in buffInfoList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class DropBag : PacketDistributed
{

public const int typeFieldNumber = 1;
 private bool hasType;
 private Int32 type_ = 0;
 public bool HasType {
 get { return hasType; }
 }
 public Int32 Type {
 get { return type_; }
 set { SetType(value); }
 }
 public void SetType(Int32 value) { 
 hasType = true;
 type_ = value;
 }

public const int valueFieldNumber = 2;
 private bool hasValue;
 private Int32 value_ = 0;
 public bool HasValue {
 get { return hasValue; }
 }
 public Int32 Value {
 get { return value_; }
 set { SetValue(value); }
 }
 public void SetValue(Int32 value) { 
 hasValue = true;
 value_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasType) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Type);
}
 if (HasValue) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Value);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasType) {
output.WriteInt32(1, Type);
}
 
if (HasValue) {
output.WriteInt32(2, Value);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 DropBag _inst = (DropBag) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Type = input.ReadInt32();
break;
}
   case  16: {
 _inst.Value = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class FengshuiInfo : PacketDistributed
{

public const int wuxingInfoFieldNumber = 1;
 private pbc::PopsicleList<WuxingInfo> wuxingInfo_ = new pbc::PopsicleList<WuxingInfo>();
 public scg::IList<WuxingInfo> wuxingInfoList {
 get { return pbc::Lists.AsReadOnly(wuxingInfo_); }
 }
 
 public int wuxingInfoCount {
 get { return wuxingInfo_.Count; }
 }
 
public WuxingInfo GetWuxingInfo(int index) {
 return wuxingInfo_[index];
 }
 public void AddWuxingInfo(WuxingInfo value) {
 wuxingInfo_.Add(value);
 }

public const int suipianInfoFieldNumber = 2;
 private pbc::PopsicleList<SuipianInfo> suipianInfo_ = new pbc::PopsicleList<SuipianInfo>();
 public scg::IList<SuipianInfo> suipianInfoList {
 get { return pbc::Lists.AsReadOnly(suipianInfo_); }
 }
 
 public int suipianInfoCount {
 get { return suipianInfo_.Count; }
 }
 
public SuipianInfo GetSuipianInfo(int index) {
 return suipianInfo_[index];
 }
 public void AddSuipianInfo(SuipianInfo value) {
 suipianInfo_.Add(value);
 }

public const int starNumFieldNumber = 3;
 private bool hasStarNum;
 private Int32 starNum_ = 0;
 public bool HasStarNum {
 get { return hasStarNum; }
 }
 public Int32 StarNum {
 get { return starNum_; }
 set { SetStarNum(value); }
 }
 public void SetStarNum(Int32 value) { 
 hasStarNum = true;
 starNum_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
foreach (WuxingInfo element in wuxingInfoList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
{
foreach (SuipianInfo element in suipianInfoList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 if (HasStarNum) {
size += pb::CodedOutputStream.ComputeInt32Size(3, StarNum);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 
do{
foreach (WuxingInfo element in wuxingInfoList) {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);

do{
foreach (SuipianInfo element in suipianInfoList) {
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 
if (HasStarNum) {
output.WriteInt32(3, StarNum);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 FengshuiInfo _inst = (FengshuiInfo) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
WuxingInfo subBuilder =  new WuxingInfo();
input.ReadMessage(subBuilder);
_inst.AddWuxingInfo(subBuilder);
break;
}
    case  18: {
SuipianInfo subBuilder =  new SuipianInfo();
input.ReadMessage(subBuilder);
_inst.AddSuipianInfo(subBuilder);
break;
}
   case  24: {
 _inst.StarNum = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
 foreach (WuxingInfo element in wuxingInfoList) {
if (!element.IsInitialized()) return false;
}
foreach (SuipianInfo element in suipianInfoList) {
if (!element.IsInitialized()) return false;
}
 if (!hasStarNum) return false;
 return true;
 }

	}


[Serializable]
public class ItemInfo : PacketDistributed
{

public const int itemIdFieldNumber = 1;
 private bool hasItemId;
 private Int32 itemId_ = 0;
 public bool HasItemId {
 get { return hasItemId; }
 }
 public Int32 ItemId {
 get { return itemId_; }
 set { SetItemId(value); }
 }
 public void SetItemId(Int32 value) { 
 hasItemId = true;
 itemId_ = value;
 }

public const int countFieldNumber = 2;
 private bool hasCount;
 private Int32 count_ = 0;
 public bool HasCount {
 get { return hasCount; }
 }
 public Int32 Count {
 get { return count_; }
 set { SetCount(value); }
 }
 public void SetCount(Int32 value) { 
 hasCount = true;
 count_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasItemId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, ItemId);
}
 if (HasCount) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Count);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasItemId) {
output.WriteInt32(1, ItemId);
}
 
if (HasCount) {
output.WriteInt32(2, Count);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 ItemInfo _inst = (ItemInfo) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.ItemId = input.ReadInt32();
break;
}
   case  16: {
 _inst.Count = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasItemId) return false;
 if (!hasCount) return false;
 return true;
 }

	}


[Serializable]
public class MissionInfo : PacketDistributed
{

public const int battleIdFieldNumber = 1;
 private bool hasBattleId;
 private Int32 battleId_ = 0;
 public bool HasBattleId {
 get { return hasBattleId; }
 }
 public Int32 BattleId {
 get { return battleId_; }
 set { SetBattleId(value); }
 }
 public void SetBattleId(Int32 value) { 
 hasBattleId = true;
 battleId_ = value;
 }

public const int countFieldNumber = 2;
 private bool hasCount;
 private Int32 count_ = 0;
 public bool HasCount {
 get { return hasCount; }
 }
 public Int32 Count {
 get { return count_; }
 set { SetCount(value); }
 }
 public void SetCount(Int32 value) { 
 hasCount = true;
 count_ = value;
 }

public const int maxStarHisFieldNumber = 3;
 private bool hasMaxStarHis;
 private Int32 maxStarHis_ = 0;
 public bool HasMaxStarHis {
 get { return hasMaxStarHis; }
 }
 public Int32 MaxStarHis {
 get { return maxStarHis_; }
 set { SetMaxStarHis(value); }
 }
 public void SetMaxStarHis(Int32 value) { 
 hasMaxStarHis = true;
 maxStarHis_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasBattleId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, BattleId);
}
 if (HasCount) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Count);
}
 if (HasMaxStarHis) {
size += pb::CodedOutputStream.ComputeInt32Size(3, MaxStarHis);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasBattleId) {
output.WriteInt32(1, BattleId);
}
 
if (HasCount) {
output.WriteInt32(2, Count);
}
 
if (HasMaxStarHis) {
output.WriteInt32(3, MaxStarHis);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 MissionInfo _inst = (MissionInfo) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.BattleId = input.ReadInt32();
break;
}
   case  16: {
 _inst.Count = input.ReadInt32();
break;
}
   case  24: {
 _inst.MaxStarHis = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasBattleId) return false;
 if (!hasCount) return false;
 return true;
 }

	}


[Serializable]
public class MonthCardInfo : PacketDistributed
{

public const int totalTimeFieldNumber = 1;
 private bool hasTotalTime;
 private Int32 totalTime_ = 0;
 public bool HasTotalTime {
 get { return hasTotalTime; }
 }
 public Int32 TotalTime {
 get { return totalTime_; }
 set { SetTotalTime(value); }
 }
 public void SetTotalTime(Int32 value) { 
 hasTotalTime = true;
 totalTime_ = value;
 }

public const int remainTimeFieldNumber = 2;
 private bool hasRemainTime;
 private Int32 remainTime_ = 0;
 public bool HasRemainTime {
 get { return hasRemainTime; }
 }
 public Int32 RemainTime {
 get { return remainTime_; }
 set { SetRemainTime(value); }
 }
 public void SetRemainTime(Int32 value) { 
 hasRemainTime = true;
 remainTime_ = value;
 }

public const int totalPurchaseFieldNumber = 3;
 private bool hasTotalPurchase;
 private Int32 totalPurchase_ = 0;
 public bool HasTotalPurchase {
 get { return hasTotalPurchase; }
 }
 public Int32 TotalPurchase {
 get { return totalPurchase_; }
 set { SetTotalPurchase(value); }
 }
 public void SetTotalPurchase(Int32 value) { 
 hasTotalPurchase = true;
 totalPurchase_ = value;
 }

public const int currentPurchaseFieldNumber = 4;
 private bool hasCurrentPurchase;
 private Int32 currentPurchase_ = 0;
 public bool HasCurrentPurchase {
 get { return hasCurrentPurchase; }
 }
 public Int32 CurrentPurchase {
 get { return currentPurchase_; }
 set { SetCurrentPurchase(value); }
 }
 public void SetCurrentPurchase(Int32 value) { 
 hasCurrentPurchase = true;
 currentPurchase_ = value;
 }

public const int rewardsFieldNumber = 5;
 private pbc::PopsicleList<MonthReward> rewards_ = new pbc::PopsicleList<MonthReward>();
 public scg::IList<MonthReward> rewardsList {
 get { return pbc::Lists.AsReadOnly(rewards_); }
 }
 
 public int rewardsCount {
 get { return rewards_.Count; }
 }
 
public MonthReward GetRewards(int index) {
 return rewards_[index];
 }
 public void AddRewards(MonthReward value) {
 rewards_.Add(value);
 }

public const int monthCardStateFieldNumber = 6;
 private bool hasMonthCardState;
 private Int32 monthCardState_ = 0;
 public bool HasMonthCardState {
 get { return hasMonthCardState; }
 }
 public Int32 MonthCardState {
 get { return monthCardState_; }
 set { SetMonthCardState(value); }
 }
 public void SetMonthCardState(Int32 value) { 
 hasMonthCardState = true;
 monthCardState_ = value;
 }

public const int rewardRemainDaysFieldNumber = 7;
 private bool hasRewardRemainDays;
 private Int32 rewardRemainDays_ = 0;
 public bool HasRewardRemainDays {
 get { return hasRewardRemainDays; }
 }
 public Int32 RewardRemainDays {
 get { return rewardRemainDays_; }
 set { SetRewardRemainDays(value); }
 }
 public void SetRewardRemainDays(Int32 value) { 
 hasRewardRemainDays = true;
 rewardRemainDays_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasTotalTime) {
size += pb::CodedOutputStream.ComputeInt32Size(1, TotalTime);
}
 if (HasRemainTime) {
size += pb::CodedOutputStream.ComputeInt32Size(2, RemainTime);
}
 if (HasTotalPurchase) {
size += pb::CodedOutputStream.ComputeInt32Size(3, TotalPurchase);
}
 if (HasCurrentPurchase) {
size += pb::CodedOutputStream.ComputeInt32Size(4, CurrentPurchase);
}
{
foreach (MonthReward element in rewardsList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)5) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 if (HasMonthCardState) {
size += pb::CodedOutputStream.ComputeInt32Size(6, MonthCardState);
}
 if (HasRewardRemainDays) {
size += pb::CodedOutputStream.ComputeInt32Size(7, RewardRemainDays);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasTotalTime) {
output.WriteInt32(1, TotalTime);
}
 
if (HasRemainTime) {
output.WriteInt32(2, RemainTime);
}
 
if (HasTotalPurchase) {
output.WriteInt32(3, TotalPurchase);
}
 
if (HasCurrentPurchase) {
output.WriteInt32(4, CurrentPurchase);
}

do{
foreach (MonthReward element in rewardsList) {
output.WriteTag((int)5, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 
if (HasMonthCardState) {
output.WriteInt32(6, MonthCardState);
}
 
if (HasRewardRemainDays) {
output.WriteInt32(7, RewardRemainDays);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 MonthCardInfo _inst = (MonthCardInfo) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.TotalTime = input.ReadInt32();
break;
}
   case  16: {
 _inst.RemainTime = input.ReadInt32();
break;
}
   case  24: {
 _inst.TotalPurchase = input.ReadInt32();
break;
}
   case  32: {
 _inst.CurrentPurchase = input.ReadInt32();
break;
}
    case  42: {
MonthReward subBuilder =  new MonthReward();
input.ReadMessage(subBuilder);
_inst.AddRewards(subBuilder);
break;
}
   case  48: {
 _inst.MonthCardState = input.ReadInt32();
break;
}
   case  56: {
 _inst.RewardRemainDays = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
 foreach (MonthReward element in rewardsList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class MonthReward : PacketDistributed
{

public const int typeFieldNumber = 1;
 private bool hasType;
 private Int32 type_ = 0;
 public bool HasType {
 get { return hasType; }
 }
 public Int32 Type {
 get { return type_; }
 set { SetType(value); }
 }
 public void SetType(Int32 value) { 
 hasType = true;
 type_ = value;
 }

public const int idFieldNumber = 2;
 private bool hasId;
 private Int32 id_ = 0;
 public bool HasId {
 get { return hasId; }
 }
 public Int32 Id {
 get { return id_; }
 set { SetId(value); }
 }
 public void SetId(Int32 value) { 
 hasId = true;
 id_ = value;
 }

public const int numFieldNumber = 3;
 private bool hasNum;
 private Int32 num_ = 0;
 public bool HasNum {
 get { return hasNum; }
 }
 public Int32 Num {
 get { return num_; }
 set { SetNum(value); }
 }
 public void SetNum(Int32 value) { 
 hasNum = true;
 num_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasType) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Type);
}
 if (HasId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Id);
}
 if (HasNum) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Num);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasType) {
output.WriteInt32(1, Type);
}
 
if (HasId) {
output.WriteInt32(2, Id);
}
 
if (HasNum) {
output.WriteInt32(3, Num);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 MonthReward _inst = (MonthReward) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Type = input.ReadInt32();
break;
}
   case  16: {
 _inst.Id = input.ReadInt32();
break;
}
   case  24: {
 _inst.Num = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasType) return false;
 return true;
 }

	}


[Serializable]
public class PBFriend : PacketDistributed
{

public const int guidFieldNumber = 1;
 private bool hasGuid;
 private Int64 guid_ = 0;
 public bool HasGuid {
 get { return hasGuid; }
 }
 public Int64 Guid {
 get { return guid_; }
 set { SetGuid(value); }
 }
 public void SetGuid(Int64 value) { 
 hasGuid = true;
 guid_ = value;
 }

public const int nameFieldNumber = 2;
 private bool hasName;
 private string name_ = "";
 public bool HasName {
 get { return hasName; }
 }
 public string Name {
 get { return name_; }
 set { SetName(value); }
 }
 public void SetName(string value) { 
 hasName = true;
 name_ = value;
 }

public const int levelFieldNumber = 3;
 private bool hasLevel;
 private Int32 level_ = 0;
 public bool HasLevel {
 get { return hasLevel; }
 }
 public Int32 Level {
 get { return level_; }
 set { SetLevel(value); }
 }
 public void SetLevel(Int32 value) { 
 hasLevel = true;
 level_ = value;
 }

public const int last_online_hoursFieldNumber = 4;
 private bool hasLast_online_hours;
 private Int32 last_online_hours_ = 0;
 public bool HasLast_online_hours {
 get { return hasLast_online_hours; }
 }
 public Int32 Last_online_hours {
 get { return last_online_hours_; }
 set { SetLast_online_hours(value); }
 }
 public void SetLast_online_hours(Int32 value) { 
 hasLast_online_hours = true;
 last_online_hours_ = value;
 }

public const int can_give_powerFieldNumber = 5;
 private bool hasCan_give_power;
 private Int32 can_give_power_ = 0;
 public bool HasCan_give_power {
 get { return hasCan_give_power; }
 }
 public Int32 Can_give_power {
 get { return can_give_power_; }
 set { SetCan_give_power(value); }
 }
 public void SetCan_give_power(Int32 value) { 
 hasCan_give_power = true;
 can_give_power_ = value;
 }

public const int can_get_powerFieldNumber = 6;
 private bool hasCan_get_power;
 private Int32 can_get_power_ = 0;
 public bool HasCan_get_power {
 get { return hasCan_get_power; }
 }
 public Int32 Can_get_power {
 get { return can_get_power_; }
 set { SetCan_get_power(value); }
 }
 public void SetCan_get_power(Int32 value) { 
 hasCan_get_power = true;
 can_get_power_ = value;
 }

public const int give_power_dayFieldNumber = 7;
 private bool hasGive_power_day;
 private Int32 give_power_day_ = 0;
 public bool HasGive_power_day {
 get { return hasGive_power_day; }
 }
 public Int32 Give_power_day {
 get { return give_power_day_; }
 set { SetGive_power_day(value); }
 }
 public void SetGive_power_day(Int32 value) { 
 hasGive_power_day = true;
 give_power_day_ = value;
 }

public const int accept_power_dayFieldNumber = 8;
 private bool hasAccept_power_day;
 private Int32 accept_power_day_ = 0;
 public bool HasAccept_power_day {
 get { return hasAccept_power_day; }
 }
 public Int32 Accept_power_day {
 get { return accept_power_day_; }
 set { SetAccept_power_day(value); }
 }
 public void SetAccept_power_day(Int32 value) { 
 hasAccept_power_day = true;
 accept_power_day_ = value;
 }

public const int assist_card_guidFieldNumber = 9;
 private bool hasAssist_card_guid;
 private Int64 assist_card_guid_ = 0;
 public bool HasAssist_card_guid {
 get { return hasAssist_card_guid; }
 }
 public Int64 Assist_card_guid {
 get { return assist_card_guid_; }
 set { SetAssist_card_guid(value); }
 }
 public void SetAssist_card_guid(Int64 value) { 
 hasAssist_card_guid = true;
 assist_card_guid_ = value;
 }

public const int assist_card_templet_idFieldNumber = 10;
 private bool hasAssist_card_templet_id;
 private Int32 assist_card_templet_id_ = 0;
 public bool HasAssist_card_templet_id {
 get { return hasAssist_card_templet_id; }
 }
 public Int32 Assist_card_templet_id {
 get { return assist_card_templet_id_; }
 set { SetAssist_card_templet_id(value); }
 }
 public void SetAssist_card_templet_id(Int32 value) { 
 hasAssist_card_templet_id = true;
 assist_card_templet_id_ = value;
 }

public const int assist_card_levelFieldNumber = 11;
 private bool hasAssist_card_level;
 private Int32 assist_card_level_ = 0;
 public bool HasAssist_card_level {
 get { return hasAssist_card_level; }
 }
 public Int32 Assist_card_level {
 get { return assist_card_level_; }
 set { SetAssist_card_level(value); }
 }
 public void SetAssist_card_level(Int32 value) { 
 hasAssist_card_level = true;
 assist_card_level_ = value;
 }

public const int assist_card_skill_idFieldNumber = 12;
 private bool hasAssist_card_skill_id;
 private Int32 assist_card_skill_id_ = 0;
 public bool HasAssist_card_skill_id {
 get { return hasAssist_card_skill_id; }
 }
 public Int32 Assist_card_skill_id {
 get { return assist_card_skill_id_; }
 set { SetAssist_card_skill_id(value); }
 }
 public void SetAssist_card_skill_id(Int32 value) { 
 hasAssist_card_skill_id = true;
 assist_card_skill_id_ = value;
 }

public const int assist_card_att_timesFieldNumber = 13;
 private bool hasAssist_card_att_times;
 private Int32 assist_card_att_times_ = 0;
 public bool HasAssist_card_att_times {
 get { return hasAssist_card_att_times; }
 }
 public Int32 Assist_card_att_times {
 get { return assist_card_att_times_; }
 set { SetAssist_card_att_times(value); }
 }
 public void SetAssist_card_att_times(Int32 value) { 
 hasAssist_card_att_times = true;
 assist_card_att_times_ = value;
 }

public const int assist_card_hp_timesFieldNumber = 14;
 private bool hasAssist_card_hp_times;
 private Int32 assist_card_hp_times_ = 0;
 public bool HasAssist_card_hp_times {
 get { return hasAssist_card_hp_times; }
 }
 public Int32 Assist_card_hp_times {
 get { return assist_card_hp_times_; }
 set { SetAssist_card_hp_times(value); }
 }
 public void SetAssist_card_hp_times(Int32 value) { 
 hasAssist_card_hp_times = true;
 assist_card_hp_times_ = value;
 }

public const int assist_card_skill_levFieldNumber = 15;
 private bool hasAssist_card_skill_lev;
 private Int32 assist_card_skill_lev_ = 0;
 public bool HasAssist_card_skill_lev {
 get { return hasAssist_card_skill_lev; }
 }
 public Int32 Assist_card_skill_lev {
 get { return assist_card_skill_lev_; }
 set { SetAssist_card_skill_lev(value); }
 }
 public void SetAssist_card_skill_lev(Int32 value) { 
 hasAssist_card_skill_lev = true;
 assist_card_skill_lev_ = value;
 }

public const int assist_friendship_numFieldNumber = 16;
 private bool hasAssist_friendship_num;
 private Int32 assist_friendship_num_ = 0;
 public bool HasAssist_friendship_num {
 get { return hasAssist_friendship_num; }
 }
 public Int32 Assist_friendship_num {
 get { return assist_friendship_num_; }
 set { SetAssist_friendship_num(value); }
 }
 public void SetAssist_friendship_num(Int32 value) { 
 hasAssist_friendship_num = true;
 assist_friendship_num_ = value;
 }

public const int assist_is_my_friendFieldNumber = 17;
 private bool hasAssist_is_my_friend;
 private Int32 assist_is_my_friend_ = 0;
 public bool HasAssist_is_my_friend {
 get { return hasAssist_is_my_friend; }
 }
 public Int32 Assist_is_my_friend {
 get { return assist_is_my_friend_; }
 set { SetAssist_is_my_friend(value); }
 }
 public void SetAssist_is_my_friend(Int32 value) { 
 hasAssist_is_my_friend = true;
 assist_is_my_friend_ = value;
 }

public const int last_logout_hoursFieldNumber = 18;
 private bool hasLast_logout_hours;
 private Int32 last_logout_hours_ = 0;
 public bool HasLast_logout_hours {
 get { return hasLast_logout_hours; }
 }
 public Int32 Last_logout_hours {
 get { return last_logout_hours_; }
 set { SetLast_logout_hours(value); }
 }
 public void SetLast_logout_hours(Int32 value) { 
 hasLast_logout_hours = true;
 last_logout_hours_ = value;
 }

public const int studySkillLevFieldNumber = 19;
 private bool hasStudySkillLev;
 private Int32 studySkillLev_ = 0;
 public bool HasStudySkillLev {
 get { return hasStudySkillLev; }
 }
 public Int32 StudySkillLev {
 get { return studySkillLev_; }
 set { SetStudySkillLev(value); }
 }
 public void SetStudySkillLev(Int32 value) { 
 hasStudySkillLev = true;
 studySkillLev_ = value;
 }

public const int studySkillIDFieldNumber = 20;
 private bool hasStudySkillID;
 private Int32 studySkillID_ = 0;
 public bool HasStudySkillID {
 get { return hasStudySkillID; }
 }
 public Int32 StudySkillID {
 get { return studySkillID_; }
 set { SetStudySkillID(value); }
 }
 public void SetStudySkillID(Int32 value) { 
 hasStudySkillID = true;
 studySkillID_ = value;
 }

public const int qxzbTopStarFieldNumber = 21;
 private bool hasQxzbTopStar;
 private Int32 qxzbTopStar_ = 0;
 public bool HasQxzbTopStar {
 get { return hasQxzbTopStar; }
 }
 public Int32 QxzbTopStar {
 get { return qxzbTopStar_; }
 set { SetQxzbTopStar(value); }
 }
 public void SetQxzbTopStar(Int32 value) { 
 hasQxzbTopStar = true;
 qxzbTopStar_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasGuid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Guid);
}
 if (HasName) {
size += pb::CodedOutputStream.ComputeStringSize(2, Name);
}
 if (HasLevel) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Level);
}
 if (HasLast_online_hours) {
size += pb::CodedOutputStream.ComputeInt32Size(4, Last_online_hours);
}
 if (HasCan_give_power) {
size += pb::CodedOutputStream.ComputeInt32Size(5, Can_give_power);
}
 if (HasCan_get_power) {
size += pb::CodedOutputStream.ComputeInt32Size(6, Can_get_power);
}
 if (HasGive_power_day) {
size += pb::CodedOutputStream.ComputeInt32Size(7, Give_power_day);
}
 if (HasAccept_power_day) {
size += pb::CodedOutputStream.ComputeInt32Size(8, Accept_power_day);
}
 if (HasAssist_card_guid) {
size += pb::CodedOutputStream.ComputeInt64Size(9, Assist_card_guid);
}
 if (HasAssist_card_templet_id) {
size += pb::CodedOutputStream.ComputeInt32Size(10, Assist_card_templet_id);
}
 if (HasAssist_card_level) {
size += pb::CodedOutputStream.ComputeInt32Size(11, Assist_card_level);
}
 if (HasAssist_card_skill_id) {
size += pb::CodedOutputStream.ComputeInt32Size(12, Assist_card_skill_id);
}
 if (HasAssist_card_att_times) {
size += pb::CodedOutputStream.ComputeInt32Size(13, Assist_card_att_times);
}
 if (HasAssist_card_hp_times) {
size += pb::CodedOutputStream.ComputeInt32Size(14, Assist_card_hp_times);
}
 if (HasAssist_card_skill_lev) {
size += pb::CodedOutputStream.ComputeInt32Size(15, Assist_card_skill_lev);
}
 if (HasAssist_friendship_num) {
size += pb::CodedOutputStream.ComputeInt32Size(16, Assist_friendship_num);
}
 if (HasAssist_is_my_friend) {
size += pb::CodedOutputStream.ComputeInt32Size(17, Assist_is_my_friend);
}
 if (HasLast_logout_hours) {
size += pb::CodedOutputStream.ComputeInt32Size(18, Last_logout_hours);
}
 if (HasStudySkillLev) {
size += pb::CodedOutputStream.ComputeInt32Size(19, StudySkillLev);
}
 if (HasStudySkillID) {
size += pb::CodedOutputStream.ComputeInt32Size(20, StudySkillID);
}
 if (HasQxzbTopStar) {
size += pb::CodedOutputStream.ComputeInt32Size(21, QxzbTopStar);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasGuid) {
output.WriteInt64(1, Guid);
}
 
if (HasName) {
output.WriteString(2, Name);
}
 
if (HasLevel) {
output.WriteInt32(3, Level);
}
 
if (HasLast_online_hours) {
output.WriteInt32(4, Last_online_hours);
}
 
if (HasCan_give_power) {
output.WriteInt32(5, Can_give_power);
}
 
if (HasCan_get_power) {
output.WriteInt32(6, Can_get_power);
}
 
if (HasGive_power_day) {
output.WriteInt32(7, Give_power_day);
}
 
if (HasAccept_power_day) {
output.WriteInt32(8, Accept_power_day);
}
 
if (HasAssist_card_guid) {
output.WriteInt64(9, Assist_card_guid);
}
 
if (HasAssist_card_templet_id) {
output.WriteInt32(10, Assist_card_templet_id);
}
 
if (HasAssist_card_level) {
output.WriteInt32(11, Assist_card_level);
}
 
if (HasAssist_card_skill_id) {
output.WriteInt32(12, Assist_card_skill_id);
}
 
if (HasAssist_card_att_times) {
output.WriteInt32(13, Assist_card_att_times);
}
 
if (HasAssist_card_hp_times) {
output.WriteInt32(14, Assist_card_hp_times);
}
 
if (HasAssist_card_skill_lev) {
output.WriteInt32(15, Assist_card_skill_lev);
}
 
if (HasAssist_friendship_num) {
output.WriteInt32(16, Assist_friendship_num);
}
 
if (HasAssist_is_my_friend) {
output.WriteInt32(17, Assist_is_my_friend);
}
 
if (HasLast_logout_hours) {
output.WriteInt32(18, Last_logout_hours);
}
 
if (HasStudySkillLev) {
output.WriteInt32(19, StudySkillLev);
}
 
if (HasStudySkillID) {
output.WriteInt32(20, StudySkillID);
}
 
if (HasQxzbTopStar) {
output.WriteInt32(21, QxzbTopStar);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 PBFriend _inst = (PBFriend) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Guid = input.ReadInt64();
break;
}
   case  18: {
 _inst.Name = input.ReadString();
break;
}
   case  24: {
 _inst.Level = input.ReadInt32();
break;
}
   case  32: {
 _inst.Last_online_hours = input.ReadInt32();
break;
}
   case  40: {
 _inst.Can_give_power = input.ReadInt32();
break;
}
   case  48: {
 _inst.Can_get_power = input.ReadInt32();
break;
}
   case  56: {
 _inst.Give_power_day = input.ReadInt32();
break;
}
   case  64: {
 _inst.Accept_power_day = input.ReadInt32();
break;
}
   case  72: {
 _inst.Assist_card_guid = input.ReadInt64();
break;
}
   case  80: {
 _inst.Assist_card_templet_id = input.ReadInt32();
break;
}
   case  88: {
 _inst.Assist_card_level = input.ReadInt32();
break;
}
   case  96: {
 _inst.Assist_card_skill_id = input.ReadInt32();
break;
}
   case  104: {
 _inst.Assist_card_att_times = input.ReadInt32();
break;
}
   case  112: {
 _inst.Assist_card_hp_times = input.ReadInt32();
break;
}
   case  120: {
 _inst.Assist_card_skill_lev = input.ReadInt32();
break;
}
   case  128: {
 _inst.Assist_friendship_num = input.ReadInt32();
break;
}
   case  136: {
 _inst.Assist_is_my_friend = input.ReadInt32();
break;
}
   case  144: {
 _inst.Last_logout_hours = input.ReadInt32();
break;
}
   case  152: {
 _inst.StudySkillLev = input.ReadInt32();
break;
}
   case  160: {
 _inst.StudySkillID = input.ReadInt32();
break;
}
   case  168: {
 _inst.QxzbTopStar = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasGuid) return false;
 if (!hasName) return false;
 if (!hasLevel) return false;
 return true;
 }

	}


[Serializable]
public class PBMail : PacketDistributed
{

public const int mail_idFieldNumber = 1;
 private bool hasMail_id;
 private Int64 mail_id_ = 0;
 public bool HasMail_id {
 get { return hasMail_id; }
 }
 public Int64 Mail_id {
 get { return mail_id_; }
 set { SetMail_id(value); }
 }
 public void SetMail_id(Int64 value) { 
 hasMail_id = true;
 mail_id_ = value;
 }

public const int typeFieldNumber = 2;
 private bool hasType;
 private Int32 type_ = 0;
 public bool HasType {
 get { return hasType; }
 }
 public Int32 Type {
 get { return type_; }
 set { SetType(value); }
 }
 public void SetType(Int32 value) { 
 hasType = true;
 type_ = value;
 }

public const int icon_idFieldNumber = 3;
 private bool hasIcon_id;
 private Int32 icon_id_ = 0;
 public bool HasIcon_id {
 get { return hasIcon_id; }
 }
 public Int32 Icon_id {
 get { return icon_id_; }
 set { SetIcon_id(value); }
 }
 public void SetIcon_id(Int32 value) { 
 hasIcon_id = true;
 icon_id_ = value;
 }

public const int fromFieldNumber = 4;
 private bool hasFrom;
 private string from_ = "";
 public bool HasFrom {
 get { return hasFrom; }
 }
 public string From {
 get { return from_; }
 set { SetFrom(value); }
 }
 public void SetFrom(string value) { 
 hasFrom = true;
 from_ = value;
 }

public const int levelFieldNumber = 5;
 private bool hasLevel;
 private Int32 level_ = 0;
 public bool HasLevel {
 get { return hasLevel; }
 }
 public Int32 Level {
 get { return level_; }
 set { SetLevel(value); }
 }
 public void SetLevel(Int32 value) { 
 hasLevel = true;
 level_ = value;
 }

public const int friend_idFieldNumber = 6;
 private bool hasFriend_id;
 private Int64 friend_id_ = 0;
 public bool HasFriend_id {
 get { return hasFriend_id; }
 }
 public Int64 Friend_id {
 get { return friend_id_; }
 set { SetFriend_id(value); }
 }
 public void SetFriend_id(Int64 value) { 
 hasFriend_id = true;
 friend_id_ = value;
 }

public const int contentFieldNumber = 7;
 private bool hasContent;
 private string content_ = "";
 public bool HasContent {
 get { return hasContent; }
 }
 public string Content {
 get { return content_; }
 set { SetContent(value); }
 }
 public void SetContent(string value) { 
 hasContent = true;
 content_ = value;
 }

public const int from_timeFieldNumber = 8;
 private bool hasFrom_time;
 private Int64 from_time_ = 0;
 public bool HasFrom_time {
 get { return hasFrom_time; }
 }
 public Int64 From_time {
 get { return from_time_; }
 set { SetFrom_time(value); }
 }
 public void SetFrom_time(Int64 value) { 
 hasFrom_time = true;
 from_time_ = value;
 }

public const int item_typeFieldNumber = 9;
 private bool hasItem_type;
 private Int32 item_type_ = 0;
 public bool HasItem_type {
 get { return hasItem_type; }
 }
 public Int32 Item_type {
 get { return item_type_; }
 set { SetItem_type(value); }
 }
 public void SetItem_type(Int32 value) { 
 hasItem_type = true;
 item_type_ = value;
 }

public const int item_valueFieldNumber = 10;
 private bool hasItem_value;
 private Int32 item_value_ = 0;
 public bool HasItem_value {
 get { return hasItem_value; }
 }
 public Int32 Item_value {
 get { return item_value_; }
 set { SetItem_value(value); }
 }
 public void SetItem_value(Int32 value) { 
 hasItem_value = true;
 item_value_ = value;
 }

public const int stateFieldNumber = 11;
 private bool hasState;
 private Int32 state_ = 0;
 public bool HasState {
 get { return hasState; }
 }
 public Int32 State {
 get { return state_; }
 set { SetState(value); }
 }
 public void SetState(Int32 value) { 
 hasState = true;
 state_ = value;
 }

public const int assist_card_guidFieldNumber = 12;
 private bool hasAssist_card_guid;
 private Int64 assist_card_guid_ = 0;
 public bool HasAssist_card_guid {
 get { return hasAssist_card_guid; }
 }
 public Int64 Assist_card_guid {
 get { return assist_card_guid_; }
 set { SetAssist_card_guid(value); }
 }
 public void SetAssist_card_guid(Int64 value) { 
 hasAssist_card_guid = true;
 assist_card_guid_ = value;
 }

public const int assist_card_templet_idFieldNumber = 13;
 private bool hasAssist_card_templet_id;
 private Int32 assist_card_templet_id_ = 0;
 public bool HasAssist_card_templet_id {
 get { return hasAssist_card_templet_id; }
 }
 public Int32 Assist_card_templet_id {
 get { return assist_card_templet_id_; }
 set { SetAssist_card_templet_id(value); }
 }
 public void SetAssist_card_templet_id(Int32 value) { 
 hasAssist_card_templet_id = true;
 assist_card_templet_id_ = value;
 }

public const int assist_card_levelFieldNumber = 14;
 private bool hasAssist_card_level;
 private Int32 assist_card_level_ = 0;
 public bool HasAssist_card_level {
 get { return hasAssist_card_level; }
 }
 public Int32 Assist_card_level {
 get { return assist_card_level_; }
 set { SetAssist_card_level(value); }
 }
 public void SetAssist_card_level(Int32 value) { 
 hasAssist_card_level = true;
 assist_card_level_ = value;
 }

public const int assist_card_skill_idFieldNumber = 15;
 private bool hasAssist_card_skill_id;
 private Int32 assist_card_skill_id_ = 0;
 public bool HasAssist_card_skill_id {
 get { return hasAssist_card_skill_id; }
 }
 public Int32 Assist_card_skill_id {
 get { return assist_card_skill_id_; }
 set { SetAssist_card_skill_id(value); }
 }
 public void SetAssist_card_skill_id(Int32 value) { 
 hasAssist_card_skill_id = true;
 assist_card_skill_id_ = value;
 }

public const int assist_card_att_timesFieldNumber = 16;
 private bool hasAssist_card_att_times;
 private Int32 assist_card_att_times_ = 0;
 public bool HasAssist_card_att_times {
 get { return hasAssist_card_att_times; }
 }
 public Int32 Assist_card_att_times {
 get { return assist_card_att_times_; }
 set { SetAssist_card_att_times(value); }
 }
 public void SetAssist_card_att_times(Int32 value) { 
 hasAssist_card_att_times = true;
 assist_card_att_times_ = value;
 }

public const int assist_card_hp_timesFieldNumber = 17;
 private bool hasAssist_card_hp_times;
 private Int32 assist_card_hp_times_ = 0;
 public bool HasAssist_card_hp_times {
 get { return hasAssist_card_hp_times; }
 }
 public Int32 Assist_card_hp_times {
 get { return assist_card_hp_times_; }
 set { SetAssist_card_hp_times(value); }
 }
 public void SetAssist_card_hp_times(Int32 value) { 
 hasAssist_card_hp_times = true;
 assist_card_hp_times_ = value;
 }

public const int assist_card_skill_levFieldNumber = 18;
 private bool hasAssist_card_skill_lev;
 private Int32 assist_card_skill_lev_ = 0;
 public bool HasAssist_card_skill_lev {
 get { return hasAssist_card_skill_lev; }
 }
 public Int32 Assist_card_skill_lev {
 get { return assist_card_skill_lev_; }
 set { SetAssist_card_skill_lev(value); }
 }
 public void SetAssist_card_skill_lev(Int32 value) { 
 hasAssist_card_skill_lev = true;
 assist_card_skill_lev_ = value;
 }

public const int last_online_hoursFieldNumber = 19;
 private bool hasLast_online_hours;
 private Int32 last_online_hours_ = 0;
 public bool HasLast_online_hours {
 get { return hasLast_online_hours; }
 }
 public Int32 Last_online_hours {
 get { return last_online_hours_; }
 set { SetLast_online_hours(value); }
 }
 public void SetLast_online_hours(Int32 value) { 
 hasLast_online_hours = true;
 last_online_hours_ = value;
 }

public const int item_numFieldNumber = 20;
 private bool hasItem_num;
 private Int32 item_num_ = 0;
 public bool HasItem_num {
 get { return hasItem_num; }
 }
 public Int32 Item_num {
 get { return item_num_; }
 set { SetItem_num(value); }
 }
 public void SetItem_num(Int32 value) { 
 hasItem_num = true;
 item_num_ = value;
 }

public const int studySkillLevFieldNumber = 21;
 private bool hasStudySkillLev;
 private Int32 studySkillLev_ = 0;
 public bool HasStudySkillLev {
 get { return hasStudySkillLev; }
 }
 public Int32 StudySkillLev {
 get { return studySkillLev_; }
 set { SetStudySkillLev(value); }
 }
 public void SetStudySkillLev(Int32 value) { 
 hasStudySkillLev = true;
 studySkillLev_ = value;
 }

public const int studySkillIDFieldNumber = 22;
 private bool hasStudySkillID;
 private Int32 studySkillID_ = 0;
 public bool HasStudySkillID {
 get { return hasStudySkillID; }
 }
 public Int32 StudySkillID {
 get { return studySkillID_; }
 set { SetStudySkillID(value); }
 }
 public void SetStudySkillID(Int32 value) { 
 hasStudySkillID = true;
 studySkillID_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasMail_id) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Mail_id);
}
 if (HasType) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Type);
}
 if (HasIcon_id) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Icon_id);
}
 if (HasFrom) {
size += pb::CodedOutputStream.ComputeStringSize(4, From);
}
 if (HasLevel) {
size += pb::CodedOutputStream.ComputeInt32Size(5, Level);
}
 if (HasFriend_id) {
size += pb::CodedOutputStream.ComputeInt64Size(6, Friend_id);
}
 if (HasContent) {
size += pb::CodedOutputStream.ComputeStringSize(7, Content);
}
 if (HasFrom_time) {
size += pb::CodedOutputStream.ComputeInt64Size(8, From_time);
}
 if (HasItem_type) {
size += pb::CodedOutputStream.ComputeInt32Size(9, Item_type);
}
 if (HasItem_value) {
size += pb::CodedOutputStream.ComputeInt32Size(10, Item_value);
}
 if (HasState) {
size += pb::CodedOutputStream.ComputeInt32Size(11, State);
}
 if (HasAssist_card_guid) {
size += pb::CodedOutputStream.ComputeInt64Size(12, Assist_card_guid);
}
 if (HasAssist_card_templet_id) {
size += pb::CodedOutputStream.ComputeInt32Size(13, Assist_card_templet_id);
}
 if (HasAssist_card_level) {
size += pb::CodedOutputStream.ComputeInt32Size(14, Assist_card_level);
}
 if (HasAssist_card_skill_id) {
size += pb::CodedOutputStream.ComputeInt32Size(15, Assist_card_skill_id);
}
 if (HasAssist_card_att_times) {
size += pb::CodedOutputStream.ComputeInt32Size(16, Assist_card_att_times);
}
 if (HasAssist_card_hp_times) {
size += pb::CodedOutputStream.ComputeInt32Size(17, Assist_card_hp_times);
}
 if (HasAssist_card_skill_lev) {
size += pb::CodedOutputStream.ComputeInt32Size(18, Assist_card_skill_lev);
}
 if (HasLast_online_hours) {
size += pb::CodedOutputStream.ComputeInt32Size(19, Last_online_hours);
}
 if (HasItem_num) {
size += pb::CodedOutputStream.ComputeInt32Size(20, Item_num);
}
 if (HasStudySkillLev) {
size += pb::CodedOutputStream.ComputeInt32Size(21, StudySkillLev);
}
 if (HasStudySkillID) {
size += pb::CodedOutputStream.ComputeInt32Size(22, StudySkillID);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasMail_id) {
output.WriteInt64(1, Mail_id);
}
 
if (HasType) {
output.WriteInt32(2, Type);
}
 
if (HasIcon_id) {
output.WriteInt32(3, Icon_id);
}
 
if (HasFrom) {
output.WriteString(4, From);
}
 
if (HasLevel) {
output.WriteInt32(5, Level);
}
 
if (HasFriend_id) {
output.WriteInt64(6, Friend_id);
}
 
if (HasContent) {
output.WriteString(7, Content);
}
 
if (HasFrom_time) {
output.WriteInt64(8, From_time);
}
 
if (HasItem_type) {
output.WriteInt32(9, Item_type);
}
 
if (HasItem_value) {
output.WriteInt32(10, Item_value);
}
 
if (HasState) {
output.WriteInt32(11, State);
}
 
if (HasAssist_card_guid) {
output.WriteInt64(12, Assist_card_guid);
}
 
if (HasAssist_card_templet_id) {
output.WriteInt32(13, Assist_card_templet_id);
}
 
if (HasAssist_card_level) {
output.WriteInt32(14, Assist_card_level);
}
 
if (HasAssist_card_skill_id) {
output.WriteInt32(15, Assist_card_skill_id);
}
 
if (HasAssist_card_att_times) {
output.WriteInt32(16, Assist_card_att_times);
}
 
if (HasAssist_card_hp_times) {
output.WriteInt32(17, Assist_card_hp_times);
}
 
if (HasAssist_card_skill_lev) {
output.WriteInt32(18, Assist_card_skill_lev);
}
 
if (HasLast_online_hours) {
output.WriteInt32(19, Last_online_hours);
}
 
if (HasItem_num) {
output.WriteInt32(20, Item_num);
}
 
if (HasStudySkillLev) {
output.WriteInt32(21, StudySkillLev);
}
 
if (HasStudySkillID) {
output.WriteInt32(22, StudySkillID);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 PBMail _inst = (PBMail) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Mail_id = input.ReadInt64();
break;
}
   case  16: {
 _inst.Type = input.ReadInt32();
break;
}
   case  24: {
 _inst.Icon_id = input.ReadInt32();
break;
}
   case  34: {
 _inst.From = input.ReadString();
break;
}
   case  40: {
 _inst.Level = input.ReadInt32();
break;
}
   case  48: {
 _inst.Friend_id = input.ReadInt64();
break;
}
   case  58: {
 _inst.Content = input.ReadString();
break;
}
   case  64: {
 _inst.From_time = input.ReadInt64();
break;
}
   case  72: {
 _inst.Item_type = input.ReadInt32();
break;
}
   case  80: {
 _inst.Item_value = input.ReadInt32();
break;
}
   case  88: {
 _inst.State = input.ReadInt32();
break;
}
   case  96: {
 _inst.Assist_card_guid = input.ReadInt64();
break;
}
   case  104: {
 _inst.Assist_card_templet_id = input.ReadInt32();
break;
}
   case  112: {
 _inst.Assist_card_level = input.ReadInt32();
break;
}
   case  120: {
 _inst.Assist_card_skill_id = input.ReadInt32();
break;
}
   case  128: {
 _inst.Assist_card_att_times = input.ReadInt32();
break;
}
   case  136: {
 _inst.Assist_card_hp_times = input.ReadInt32();
break;
}
   case  144: {
 _inst.Assist_card_skill_lev = input.ReadInt32();
break;
}
   case  152: {
 _inst.Last_online_hours = input.ReadInt32();
break;
}
   case  160: {
 _inst.Item_num = input.ReadInt32();
break;
}
   case  168: {
 _inst.StudySkillLev = input.ReadInt32();
break;
}
   case  176: {
 _inst.StudySkillID = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasMail_id) return false;
 if (!hasType) return false;
 if (!hasIcon_id) return false;
 if (!hasState) return false;
 return true;
 }

	}


[Serializable]
public class PBTask : PacketDistributed
{

public const int templet_idFieldNumber = 1;
 private bool hasTemplet_id;
 private Int32 templet_id_ = 0;
 public bool HasTemplet_id {
 get { return hasTemplet_id; }
 }
 public Int32 Templet_id {
 get { return templet_id_; }
 set { SetTemplet_id(value); }
 }
 public void SetTemplet_id(Int32 value) { 
 hasTemplet_id = true;
 templet_id_ = value;
 }

public const int processFieldNumber = 2;
 private bool hasProcess;
 private Int32 process_ = 0;
 public bool HasProcess {
 get { return hasProcess; }
 }
 public Int32 Process {
 get { return process_; }
 set { SetProcess(value); }
 }
 public void SetProcess(Int32 value) { 
 hasProcess = true;
 process_ = value;
 }

public const int stateFieldNumber = 3;
 private bool hasState;
 private Int32 state_ = 0;
 public bool HasState {
 get { return hasState; }
 }
 public Int32 State {
 get { return state_; }
 set { SetState(value); }
 }
 public void SetState(Int32 value) { 
 hasState = true;
 state_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasTemplet_id) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Templet_id);
}
 if (HasProcess) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Process);
}
 if (HasState) {
size += pb::CodedOutputStream.ComputeInt32Size(3, State);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasTemplet_id) {
output.WriteInt32(1, Templet_id);
}
 
if (HasProcess) {
output.WriteInt32(2, Process);
}
 
if (HasState) {
output.WriteInt32(3, State);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 PBTask _inst = (PBTask) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Templet_id = input.ReadInt32();
break;
}
   case  16: {
 _inst.Process = input.ReadInt32();
break;
}
   case  24: {
 _inst.State = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasTemplet_id) return false;
 if (!hasProcess) return false;
 if (!hasState) return false;
 return true;
 }

	}


[Serializable]
public class PBUserBagData : PacketDistributed
{

public const int bagCapacityFieldNumber = 1;
 private bool hasBagCapacity;
 private Int32 bagCapacity_ = 0;
 public bool HasBagCapacity {
 get { return hasBagCapacity; }
 }
 public Int32 BagCapacity {
 get { return bagCapacity_; }
 set { SetBagCapacity(value); }
 }
 public void SetBagCapacity(Int32 value) { 
 hasBagCapacity = true;
 bagCapacity_ = value;
 }

public const int cardInfoFieldNumber = 2;
 private pbc::PopsicleList<CardInfo> cardInfo_ = new pbc::PopsicleList<CardInfo>();
 public scg::IList<CardInfo> cardInfoList {
 get { return pbc::Lists.AsReadOnly(cardInfo_); }
 }
 
 public int cardInfoCount {
 get { return cardInfo_.Count; }
 }
 
public CardInfo GetCardInfo(int index) {
 return cardInfo_[index];
 }
 public void AddCardInfo(CardInfo value) {
 cardInfo_.Add(value);
 }

public const int itemInfoFieldNumber = 3;
 private pbc::PopsicleList<ItemInfo> itemInfo_ = new pbc::PopsicleList<ItemInfo>();
 public scg::IList<ItemInfo> itemInfoList {
 get { return pbc::Lists.AsReadOnly(itemInfo_); }
 }
 
 public int itemInfoCount {
 get { return itemInfo_.Count; }
 }
 
public ItemInfo GetItemInfo(int index) {
 return itemInfo_[index];
 }
 public void AddItemInfo(ItemInfo value) {
 itemInfo_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasBagCapacity) {
size += pb::CodedOutputStream.ComputeInt32Size(1, BagCapacity);
}
{
foreach (CardInfo element in cardInfoList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
{
foreach (ItemInfo element in itemInfoList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasBagCapacity) {
output.WriteInt32(1, BagCapacity);
}

do{
foreach (CardInfo element in cardInfoList) {
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);

do{
foreach (ItemInfo element in itemInfoList) {
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 PBUserBagData _inst = (PBUserBagData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.BagCapacity = input.ReadInt32();
break;
}
    case  18: {
CardInfo subBuilder =  new CardInfo();
input.ReadMessage(subBuilder);
_inst.AddCardInfo(subBuilder);
break;
}
    case  26: {
ItemInfo subBuilder =  new ItemInfo();
input.ReadMessage(subBuilder);
_inst.AddItemInfo(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasBagCapacity) return false;
foreach (CardInfo element in cardInfoList) {
if (!element.IsInitialized()) return false;
}
foreach (ItemInfo element in itemInfoList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class PBUserBaseData : PacketDistributed
{

public const int nameFieldNumber = 2;
 private bool hasName;
 private string name_ = "";
 public bool HasName {
 get { return hasName; }
 }
 public string Name {
 get { return name_; }
 set { SetName(value); }
 }
 public void SetName(string value) { 
 hasName = true;
 name_ = value;
 }

public const int mailFieldNumber = 3;
 private bool hasMail;
 private Int32 mail_ = 0;
 public bool HasMail {
 get { return hasMail; }
 }
 public Int32 Mail {
 get { return mail_; }
 set { SetMail(value); }
 }
 public void SetMail(Int32 value) { 
 hasMail = true;
 mail_ = value;
 }

public const int levelFieldNumber = 4;
 private bool hasLevel;
 private Int32 level_ = 0;
 public bool HasLevel {
 get { return hasLevel; }
 }
 public Int32 Level {
 get { return level_; }
 set { SetLevel(value); }
 }
 public void SetLevel(Int32 value) { 
 hasLevel = true;
 level_ = value;
 }

public const int expFieldNumber = 5;
 private bool hasExp;
 private Int32 exp_ = 0;
 public bool HasExp {
 get { return hasExp; }
 }
 public Int32 Exp {
 get { return exp_; }
 set { SetExp(value); }
 }
 public void SetExp(Int32 value) { 
 hasExp = true;
 exp_ = value;
 }

public const int moneyFieldNumber = 6;
 private bool hasMoney;
 private Int32 money_ = 0;
 public bool HasMoney {
 get { return hasMoney; }
 }
 public Int32 Money {
 get { return money_; }
 set { SetMoney(value); }
 }
 public void SetMoney(Int32 value) { 
 hasMoney = true;
 money_ = value;
 }

public const int dollarFieldNumber = 7;
 private bool hasDollar;
 private Int32 dollar_ = 0;
 public bool HasDollar {
 get { return hasDollar; }
 }
 public Int32 Dollar {
 get { return dollar_; }
 set { SetDollar(value); }
 }
 public void SetDollar(Int32 value) { 
 hasDollar = true;
 dollar_ = value;
 }

public const int powerFieldNumber = 8;
 private bool hasPower;
 private Int32 power_ = 0;
 public bool HasPower {
 get { return hasPower; }
 }
 public Int32 Power {
 get { return power_; }
 set { SetPower(value); }
 }
 public void SetPower(Int32 value) { 
 hasPower = true;
 power_ = value;
 }

public const int leadershipFieldNumber = 9;
 private bool hasLeadership;
 private Int32 leadership_ = 0;
 public bool HasLeadership {
 get { return hasLeadership; }
 }
 public Int32 Leadership {
 get { return leadership_; }
 set { SetLeadership(value); }
 }
 public void SetLeadership(Int32 value) { 
 hasLeadership = true;
 leadership_ = value;
 }

public const int friendpointFieldNumber = 10;
 private bool hasFriendpoint;
 private Int32 friendpoint_ = 0;
 public bool HasFriendpoint {
 get { return hasFriendpoint; }
 }
 public Int32 Friendpoint {
 get { return friendpoint_; }
 set { SetFriendpoint(value); }
 }
 public void SetFriendpoint(Int32 value) { 
 hasFriendpoint = true;
 friendpoint_ = value;
 }

public const int powertimeFieldNumber = 11;
 private bool hasPowertime;
 private Int64 powertime_ = 0;
 public bool HasPowertime {
 get { return hasPowertime; }
 }
 public Int64 Powertime {
 get { return powertime_; }
 set { SetPowertime(value); }
 }
 public void SetPowertime(Int64 value) { 
 hasPowertime = true;
 powertime_ = value;
 }

public const int luckyPointFieldNumber = 12;
 private bool hasLuckyPoint;
 private Int32 luckyPoint_ = 0;
 public bool HasLuckyPoint {
 get { return hasLuckyPoint; }
 }
 public Int32 LuckyPoint {
 get { return luckyPoint_; }
 set { SetLuckyPoint(value); }
 }
 public void SetLuckyPoint(Int32 value) { 
 hasLuckyPoint = true;
 luckyPoint_ = value;
 }

public const int friendNumMaxFieldNumber = 13;
 private bool hasFriendNumMax;
 private Int32 friendNumMax_ = 0;
 public bool HasFriendNumMax {
 get { return hasFriendNumMax; }
 }
 public Int32 FriendNumMax {
 get { return friendNumMax_; }
 set { SetFriendNumMax(value); }
 }
 public void SetFriendNumMax(Int32 value) { 
 hasFriendNumMax = true;
 friendNumMax_ = value;
 }

public const int bagCapacityFieldNumber = 14;
 private bool hasBagCapacity;
 private Int32 bagCapacity_ = 0;
 public bool HasBagCapacity {
 get { return hasBagCapacity; }
 }
 public Int32 BagCapacity {
 get { return bagCapacity_; }
 set { SetBagCapacity(value); }
 }
 public void SetBagCapacity(Int32 value) { 
 hasBagCapacity = true;
 bagCapacity_ = value;
 }

public const int noReadMailFieldNumber = 15;
 private bool hasNoReadMail;
 private Int32 noReadMail_ = 0;
 public bool HasNoReadMail {
 get { return hasNoReadMail; }
 }
 public Int32 NoReadMail {
 get { return noReadMail_; }
 set { SetNoReadMail(value); }
 }
 public void SetNoReadMail(Int32 value) { 
 hasNoReadMail = true;
 noReadMail_ = value;
 }

public const int noGetQuestFieldNumber = 16;
 private bool hasNoGetQuest;
 private Int32 noGetQuest_ = 0;
 public bool HasNoGetQuest {
 get { return hasNoGetQuest; }
 }
 public Int32 NoGetQuest {
 get { return noGetQuest_; }
 set { SetNoGetQuest(value); }
 }
 public void SetNoGetQuest(Int32 value) { 
 hasNoGetQuest = true;
 noGetQuest_ = value;
 }

public const int lastBattleDataFieldNumber = 17;
 private bool hasLastBattleData;
 private DataBattle lastBattleData_ =  new DataBattle();
 public bool HasLastBattleData {
 get { return hasLastBattleData; }
 }
 public DataBattle LastBattleData {
 get { return lastBattleData_; }
 set { SetLastBattleData(value); }
 }
 public void SetLastBattleData(DataBattle value) { 
 hasLastBattleData = true;
 lastBattleData_ = value;
 }

public const int ifFreeGambleFieldNumber = 18;
 private bool hasIfFreeGamble;
 private Int32 ifFreeGamble_ = 0;
 public bool HasIfFreeGamble {
 get { return hasIfFreeGamble; }
 }
 public Int32 IfFreeGamble {
 get { return ifFreeGamble_; }
 set { SetIfFreeGamble(value); }
 }
 public void SetIfFreeGamble(Int32 value) { 
 hasIfFreeGamble = true;
 ifFreeGamble_ = value;
 }

public const int freeCDFieldNumber = 19;
 private bool hasFreeCD;
 private Int64 freeCD_ = 0;
 public bool HasFreeCD {
 get { return hasFreeCD; }
 }
 public Int64 FreeCD {
 get { return freeCD_; }
 set { SetFreeCD(value); }
 }
 public void SetFreeCD(Int64 value) { 
 hasFreeCD = true;
 freeCD_ = value;
 }

public const int pvptimesFieldNumber = 20;
 private bool hasPvptimes;
 private Int32 pvptimes_ = 0;
 public bool HasPvptimes {
 get { return hasPvptimes; }
 }
 public Int32 Pvptimes {
 get { return pvptimes_; }
 set { SetPvptimes(value); }
 }
 public void SetPvptimes(Int32 value) { 
 hasPvptimes = true;
 pvptimes_ = value;
 }

public const int purchaseDollarFieldNumber = 21;
 private bool hasPurchaseDollar;
 private Int32 purchaseDollar_ = 0;
 public bool HasPurchaseDollar {
 get { return hasPurchaseDollar; }
 }
 public Int32 PurchaseDollar {
 get { return purchaseDollar_; }
 set { SetPurchaseDollar(value); }
 }
 public void SetPurchaseDollar(Int32 value) { 
 hasPurchaseDollar = true;
 purchaseDollar_ = value;
 }

public const int daysAfterFirstLoginFieldNumber = 22;
 private bool hasDaysAfterFirstLogin;
 private Int32 daysAfterFirstLogin_ = 0;
 public bool HasDaysAfterFirstLogin {
 get { return hasDaysAfterFirstLogin; }
 }
 public Int32 DaysAfterFirstLogin {
 get { return daysAfterFirstLogin_; }
 set { SetDaysAfterFirstLogin(value); }
 }
 public void SetDaysAfterFirstLogin(Int32 value) { 
 hasDaysAfterFirstLogin = true;
 daysAfterFirstLogin_ = value;
 }

public const int finish_stepFieldNumber = 23;
 private bool hasFinish_step;
 private Int32 finish_step_ = 0;
 public bool HasFinish_step {
 get { return hasFinish_step; }
 }
 public Int32 Finish_step {
 get { return finish_step_; }
 set { SetFinish_step(value); }
 }
 public void SetFinish_step(Int32 value) { 
 hasFinish_step = true;
 finish_step_ = value;
 }

public const int beforeDropsFieldNumber = 24;
 private pbc::PopsicleList<DropBag> beforeDrops_ = new pbc::PopsicleList<DropBag>();
 public scg::IList<DropBag> beforeDropsList {
 get { return pbc::Lists.AsReadOnly(beforeDrops_); }
 }
 
 public int beforeDropsCount {
 get { return beforeDrops_.Count; }
 }
 
public DropBag GetBeforeDrops(int index) {
 return beforeDrops_[index];
 }
 public void AddBeforeDrops(DropBag value) {
 beforeDrops_.Add(value);
 }

public const int buyMoneyTimesFieldNumber = 25;
 private bool hasBuyMoneyTimes;
 private Int32 buyMoneyTimes_ = 0;
 public bool HasBuyMoneyTimes {
 get { return hasBuyMoneyTimes; }
 }
 public Int32 BuyMoneyTimes {
 get { return buyMoneyTimes_; }
 set { SetBuyMoneyTimes(value); }
 }
 public void SetBuyMoneyTimes(Int32 value) { 
 hasBuyMoneyTimes = true;
 buyMoneyTimes_ = value;
 }

public const int buyPowerTimesFieldNumber = 26;
 private bool hasBuyPowerTimes;
 private Int32 buyPowerTimes_ = 0;
 public bool HasBuyPowerTimes {
 get { return hasBuyPowerTimes; }
 }
 public Int32 BuyPowerTimes {
 get { return buyPowerTimes_; }
 set { SetBuyPowerTimes(value); }
 }
 public void SetBuyPowerTimes(Int32 value) { 
 hasBuyPowerTimes = true;
 buyPowerTimes_ = value;
 }

public const int GGLTimesFieldNumber = 27;
 private bool hasGGLTimes;
 private Int32 GGLTimes_ = 0;
 public bool HasGGLTimes {
 get { return hasGGLTimes; }
 }
 public Int32 GGLTimes {
 get { return GGLTimes_; }
 set { SetGGLTimes(value); }
 }
 public void SetGGLTimes(Int32 value) { 
 hasGGLTimes = true;
 GGLTimes_ = value;
 }

public const int fengshuiInfoFieldNumber = 28;
 private bool hasFengshuiInfo;
 private FengshuiInfo fengshuiInfo_ =  new FengshuiInfo();
 public bool HasFengshuiInfo {
 get { return hasFengshuiInfo; }
 }
 public FengshuiInfo FengshuiInfo {
 get { return fengshuiInfo_; }
 set { SetFengshuiInfo(value); }
 }
 public void SetFengshuiInfo(FengshuiInfo value) { 
 hasFengshuiInfo = true;
 fengshuiInfo_ = value;
 }

public const int BBZTimesFieldNumber = 29;
 private bool hasBBZTimes;
 private Int32 BBZTimes_ = 0;
 public bool HasBBZTimes {
 get { return hasBBZTimes; }
 }
 public Int32 BBZTimes {
 get { return BBZTimes_; }
 set { SetBBZTimes(value); }
 }
 public void SetBBZTimes(Int32 value) { 
 hasBBZTimes = true;
 BBZTimes_ = value;
 }

public const int BBZFlagFieldNumber = 30;
 private bool hasBBZFlag;
 private Int32 BBZFlag_ = 0;
 public bool HasBBZFlag {
 get { return hasBBZFlag; }
 }
 public Int32 BBZFlag {
 get { return BBZFlag_; }
 set { SetBBZFlag(value); }
 }
 public void SetBBZFlag(Int32 value) { 
 hasBBZFlag = true;
 BBZFlag_ = value;
 }

public const int monthCardFlagFieldNumber = 31;
 private bool hasMonthCardFlag;
 private Int32 monthCardFlag_ = 0;
 public bool HasMonthCardFlag {
 get { return hasMonthCardFlag; }
 }
 public Int32 MonthCardFlag {
 get { return monthCardFlag_; }
 set { SetMonthCardFlag(value); }
 }
 public void SetMonthCardFlag(Int32 value) { 
 hasMonthCardFlag = true;
 monthCardFlag_ = value;
 }

public const int gglLastRewardIDFieldNumber = 32;
 private bool hasGglLastRewardID;
 private Int32 gglLastRewardID_ = 0;
 public bool HasGglLastRewardID {
 get { return hasGglLastRewardID; }
 }
 public Int32 GglLastRewardID {
 get { return gglLastRewardID_; }
 set { SetGglLastRewardID(value); }
 }
 public void SetGglLastRewardID(Int32 value) { 
 hasGglLastRewardID = true;
 gglLastRewardID_ = value;
 }

public const int pataTimesFieldNumber = 33;
 private bool hasPataTimes;
 private Int32 pataTimes_ = 0;
 public bool HasPataTimes {
 get { return hasPataTimes; }
 }
 public Int32 PataTimes {
 get { return pataTimes_; }
 set { SetPataTimes(value); }
 }
 public void SetPataTimes(Int32 value) { 
 hasPataTimes = true;
 pataTimes_ = value;
 }

public const int pataNumFieldNumber = 34;
 private bool hasPataNum;
 private Int32 pataNum_ = 0;
 public bool HasPataNum {
 get { return hasPataNum; }
 }
 public Int32 PataNum {
 get { return pataNum_; }
 set { SetPataNum(value); }
 }
 public void SetPataNum(Int32 value) { 
 hasPataNum = true;
 pataNum_ = value;
 }

public const int areaidFieldNumber = 35;
 private bool hasAreaid;
 private Int32 areaid_ = 0;
 public bool HasAreaid {
 get { return hasAreaid; }
 }
 public Int32 Areaid {
 get { return areaid_; }
 set { SetAreaid(value); }
 }
 public void SetAreaid(Int32 value) { 
 hasAreaid = true;
 areaid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasName) {
size += pb::CodedOutputStream.ComputeStringSize(2, Name);
}
 if (HasMail) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Mail);
}
 if (HasLevel) {
size += pb::CodedOutputStream.ComputeInt32Size(4, Level);
}
 if (HasExp) {
size += pb::CodedOutputStream.ComputeInt32Size(5, Exp);
}
 if (HasMoney) {
size += pb::CodedOutputStream.ComputeInt32Size(6, Money);
}
 if (HasDollar) {
size += pb::CodedOutputStream.ComputeInt32Size(7, Dollar);
}
 if (HasPower) {
size += pb::CodedOutputStream.ComputeInt32Size(8, Power);
}
 if (HasLeadership) {
size += pb::CodedOutputStream.ComputeInt32Size(9, Leadership);
}
 if (HasFriendpoint) {
size += pb::CodedOutputStream.ComputeInt32Size(10, Friendpoint);
}
 if (HasPowertime) {
size += pb::CodedOutputStream.ComputeInt64Size(11, Powertime);
}
 if (HasLuckyPoint) {
size += pb::CodedOutputStream.ComputeInt32Size(12, LuckyPoint);
}
 if (HasFriendNumMax) {
size += pb::CodedOutputStream.ComputeInt32Size(13, FriendNumMax);
}
 if (HasBagCapacity) {
size += pb::CodedOutputStream.ComputeInt32Size(14, BagCapacity);
}
 if (HasNoReadMail) {
size += pb::CodedOutputStream.ComputeInt32Size(15, NoReadMail);
}
 if (HasNoGetQuest) {
size += pb::CodedOutputStream.ComputeInt32Size(16, NoGetQuest);
}
{
int subsize = LastBattleData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)17) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 if (HasIfFreeGamble) {
size += pb::CodedOutputStream.ComputeInt32Size(18, IfFreeGamble);
}
 if (HasFreeCD) {
size += pb::CodedOutputStream.ComputeInt64Size(19, FreeCD);
}
 if (HasPvptimes) {
size += pb::CodedOutputStream.ComputeInt32Size(20, Pvptimes);
}
 if (HasPurchaseDollar) {
size += pb::CodedOutputStream.ComputeInt32Size(21, PurchaseDollar);
}
 if (HasDaysAfterFirstLogin) {
size += pb::CodedOutputStream.ComputeInt32Size(22, DaysAfterFirstLogin);
}
 if (HasFinish_step) {
size += pb::CodedOutputStream.ComputeInt32Size(23, Finish_step);
}
{
foreach (DropBag element in beforeDropsList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)24) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 if (HasBuyMoneyTimes) {
size += pb::CodedOutputStream.ComputeInt32Size(25, BuyMoneyTimes);
}
 if (HasBuyPowerTimes) {
size += pb::CodedOutputStream.ComputeInt32Size(26, BuyPowerTimes);
}
 if (HasGGLTimes) {
size += pb::CodedOutputStream.ComputeInt32Size(27, GGLTimes);
}
{
int subsize = FengshuiInfo.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)28) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 if (HasBBZTimes) {
size += pb::CodedOutputStream.ComputeInt32Size(29, BBZTimes);
}
 if (HasBBZFlag) {
size += pb::CodedOutputStream.ComputeInt32Size(30, BBZFlag);
}
 if (HasMonthCardFlag) {
size += pb::CodedOutputStream.ComputeInt32Size(31, MonthCardFlag);
}
 if (HasGglLastRewardID) {
size += pb::CodedOutputStream.ComputeInt32Size(32, GglLastRewardID);
}
 if (HasPataTimes) {
size += pb::CodedOutputStream.ComputeInt32Size(33, PataTimes);
}
 if (HasPataNum) {
size += pb::CodedOutputStream.ComputeInt32Size(34, PataNum);
}
 if (HasAreaid) {
size += pb::CodedOutputStream.ComputeInt32Size(35, Areaid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasName) {
output.WriteString(2, Name);
}
 
if (HasMail) {
output.WriteInt32(3, Mail);
}
 
if (HasLevel) {
output.WriteInt32(4, Level);
}
 
if (HasExp) {
output.WriteInt32(5, Exp);
}
 
if (HasMoney) {
output.WriteInt32(6, Money);
}
 
if (HasDollar) {
output.WriteInt32(7, Dollar);
}
 
if (HasPower) {
output.WriteInt32(8, Power);
}
 
if (HasLeadership) {
output.WriteInt32(9, Leadership);
}
 
if (HasFriendpoint) {
output.WriteInt32(10, Friendpoint);
}
 
if (HasPowertime) {
output.WriteInt64(11, Powertime);
}
 
if (HasLuckyPoint) {
output.WriteInt32(12, LuckyPoint);
}
 
if (HasFriendNumMax) {
output.WriteInt32(13, FriendNumMax);
}
 
if (HasBagCapacity) {
output.WriteInt32(14, BagCapacity);
}
 
if (HasNoReadMail) {
output.WriteInt32(15, NoReadMail);
}
 
if (HasNoGetQuest) {
output.WriteInt32(16, NoGetQuest);
}
{
output.WriteTag((int)17, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)LastBattleData.SerializedSize());
LastBattleData.WriteTo(output);

}
 
if (HasIfFreeGamble) {
output.WriteInt32(18, IfFreeGamble);
}
 
if (HasFreeCD) {
output.WriteInt64(19, FreeCD);
}
 
if (HasPvptimes) {
output.WriteInt32(20, Pvptimes);
}
 
if (HasPurchaseDollar) {
output.WriteInt32(21, PurchaseDollar);
}
 
if (HasDaysAfterFirstLogin) {
output.WriteInt32(22, DaysAfterFirstLogin);
}
 
if (HasFinish_step) {
output.WriteInt32(23, Finish_step);
}

do{
foreach (DropBag element in beforeDropsList) {
output.WriteTag((int)24, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 
if (HasBuyMoneyTimes) {
output.WriteInt32(25, BuyMoneyTimes);
}
 
if (HasBuyPowerTimes) {
output.WriteInt32(26, BuyPowerTimes);
}
 
if (HasGGLTimes) {
output.WriteInt32(27, GGLTimes);
}
{
output.WriteTag((int)28, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)FengshuiInfo.SerializedSize());
FengshuiInfo.WriteTo(output);

}
 
if (HasBBZTimes) {
output.WriteInt32(29, BBZTimes);
}
 
if (HasBBZFlag) {
output.WriteInt32(30, BBZFlag);
}
 
if (HasMonthCardFlag) {
output.WriteInt32(31, MonthCardFlag);
}
 
if (HasGglLastRewardID) {
output.WriteInt32(32, GglLastRewardID);
}
 
if (HasPataTimes) {
output.WriteInt32(33, PataTimes);
}
 
if (HasPataNum) {
output.WriteInt32(34, PataNum);
}
 
if (HasAreaid) {
output.WriteInt32(35, Areaid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 PBUserBaseData _inst = (PBUserBaseData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  18: {
 _inst.Name = input.ReadString();
break;
}
   case  24: {
 _inst.Mail = input.ReadInt32();
break;
}
   case  32: {
 _inst.Level = input.ReadInt32();
break;
}
   case  40: {
 _inst.Exp = input.ReadInt32();
break;
}
   case  48: {
 _inst.Money = input.ReadInt32();
break;
}
   case  56: {
 _inst.Dollar = input.ReadInt32();
break;
}
   case  64: {
 _inst.Power = input.ReadInt32();
break;
}
   case  72: {
 _inst.Leadership = input.ReadInt32();
break;
}
   case  80: {
 _inst.Friendpoint = input.ReadInt32();
break;
}
   case  88: {
 _inst.Powertime = input.ReadInt64();
break;
}
   case  96: {
 _inst.LuckyPoint = input.ReadInt32();
break;
}
   case  104: {
 _inst.FriendNumMax = input.ReadInt32();
break;
}
   case  112: {
 _inst.BagCapacity = input.ReadInt32();
break;
}
   case  120: {
 _inst.NoReadMail = input.ReadInt32();
break;
}
   case  128: {
 _inst.NoGetQuest = input.ReadInt32();
break;
}
    case  138: {
DataBattle subBuilder =  new DataBattle();
 input.ReadMessage(subBuilder);
_inst.LastBattleData = subBuilder;
break;
}
   case  144: {
 _inst.IfFreeGamble = input.ReadInt32();
break;
}
   case  152: {
 _inst.FreeCD = input.ReadInt64();
break;
}
   case  160: {
 _inst.Pvptimes = input.ReadInt32();
break;
}
   case  168: {
 _inst.PurchaseDollar = input.ReadInt32();
break;
}
   case  176: {
 _inst.DaysAfterFirstLogin = input.ReadInt32();
break;
}
   case  184: {
 _inst.Finish_step = input.ReadInt32();
break;
}
    case  194: {
DropBag subBuilder =  new DropBag();
input.ReadMessage(subBuilder);
_inst.AddBeforeDrops(subBuilder);
break;
}
   case  200: {
 _inst.BuyMoneyTimes = input.ReadInt32();
break;
}
   case  208: {
 _inst.BuyPowerTimes = input.ReadInt32();
break;
}
   case  216: {
 _inst.GGLTimes = input.ReadInt32();
break;
}
    case  226: {
FengshuiInfo subBuilder =  new FengshuiInfo();
 input.ReadMessage(subBuilder);
_inst.FengshuiInfo = subBuilder;
break;
}
   case  232: {
 _inst.BBZTimes = input.ReadInt32();
break;
}
   case  240: {
 _inst.BBZFlag = input.ReadInt32();
break;
}
   case  248: {
 _inst.MonthCardFlag = input.ReadInt32();
break;
}
   case  256: {
 _inst.GglLastRewardID = input.ReadInt32();
break;
}
   case  264: {
 _inst.PataTimes = input.ReadInt32();
break;
}
   case  272: {
 _inst.PataNum = input.ReadInt32();
break;
}
   case  280: {
 _inst.Areaid = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasName) return false;
 if (!hasMail) return false;
 if (!hasLevel) return false;
 if (!hasExp) return false;
 if (!hasMoney) return false;
 if (!hasDollar) return false;
 if (!hasPower) return false;
 if (!hasLeadership) return false;
 if (!hasFriendpoint) return false;
  if (HasLastBattleData) {
if (!LastBattleData.IsInitialized()) return false;
}
foreach (DropBag element in beforeDropsList) {
if (!element.IsInitialized()) return false;
}
  if (HasFengshuiInfo) {
if (!FengshuiInfo.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class PBUserCopyData : PacketDistributed
{

public const int activityCopyFieldNumber = 1;
 private pbc::PopsicleList<CopyInfo> activityCopy_ = new pbc::PopsicleList<CopyInfo>();
 public scg::IList<CopyInfo> activityCopyList {
 get { return pbc::Lists.AsReadOnly(activityCopy_); }
 }
 
 public int activityCopyCount {
 get { return activityCopy_.Count; }
 }
 
public CopyInfo GetActivityCopy(int index) {
 return activityCopy_[index];
 }
 public void AddActivityCopy(CopyInfo value) {
 activityCopy_.Add(value);
 }

public const int normalCopyFieldNumber = 2;
 private pbc::PopsicleList<MissionInfo> normalCopy_ = new pbc::PopsicleList<MissionInfo>();
 public scg::IList<MissionInfo> normalCopyList {
 get { return pbc::Lists.AsReadOnly(normalCopy_); }
 }
 
 public int normalCopyCount {
 get { return normalCopy_.Count; }
 }
 
public MissionInfo GetNormalCopy(int index) {
 return normalCopy_[index];
 }
 public void AddNormalCopy(MissionInfo value) {
 normalCopy_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
foreach (CopyInfo element in activityCopyList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
{
foreach (MissionInfo element in normalCopyList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 
do{
foreach (CopyInfo element in activityCopyList) {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);

do{
foreach (MissionInfo element in normalCopyList) {
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 PBUserCopyData _inst = (PBUserCopyData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
CopyInfo subBuilder =  new CopyInfo();
input.ReadMessage(subBuilder);
_inst.AddActivityCopy(subBuilder);
break;
}
    case  18: {
MissionInfo subBuilder =  new MissionInfo();
input.ReadMessage(subBuilder);
_inst.AddNormalCopy(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
 foreach (CopyInfo element in activityCopyList) {
if (!element.IsInitialized()) return false;
}
foreach (MissionInfo element in normalCopyList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class PBYunyingHuodong : PacketDistributed
{

public const int ItemTypeFieldNumber = 1;
 private bool hasItemType;
 private Int32 ItemType_ = 0;
 public bool HasItemType {
 get { return hasItemType; }
 }
 public Int32 ItemType {
 get { return ItemType_; }
 set { SetItemType(value); }
 }
 public void SetItemType(Int32 value) { 
 hasItemType = true;
 ItemType_ = value;
 }

public const int ItemValueFieldNumber = 2;
 private bool hasItemValue;
 private Int32 ItemValue_ = 0;
 public bool HasItemValue {
 get { return hasItemValue; }
 }
 public Int32 ItemValue {
 get { return ItemValue_; }
 set { SetItemValue(value); }
 }
 public void SetItemValue(Int32 value) { 
 hasItemValue = true;
 ItemValue_ = value;
 }

public const int ItemNumberFieldNumber = 3;
 private bool hasItemNumber;
 private Int32 ItemNumber_ = 0;
 public bool HasItemNumber {
 get { return hasItemNumber; }
 }
 public Int32 ItemNumber {
 get { return ItemNumber_; }
 set { SetItemNumber(value); }
 }
 public void SetItemNumber(Int32 value) { 
 hasItemNumber = true;
 ItemNumber_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasItemType) {
size += pb::CodedOutputStream.ComputeInt32Size(1, ItemType);
}
 if (HasItemValue) {
size += pb::CodedOutputStream.ComputeInt32Size(2, ItemValue);
}
 if (HasItemNumber) {
size += pb::CodedOutputStream.ComputeInt32Size(3, ItemNumber);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasItemType) {
output.WriteInt32(1, ItemType);
}
 
if (HasItemValue) {
output.WriteInt32(2, ItemValue);
}
 
if (HasItemNumber) {
output.WriteInt32(3, ItemNumber);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 PBYunyingHuodong _inst = (PBYunyingHuodong) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.ItemType = input.ReadInt32();
break;
}
   case  16: {
 _inst.ItemValue = input.ReadInt32();
break;
}
   case  24: {
 _inst.ItemNumber = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasItemType) return false;
 if (!hasItemValue) return false;
 if (!hasItemNumber) return false;
 return true;
 }

	}


[Serializable]
public class PVPUserBaseData : PacketDistributed
{

public const int nGuidFieldNumber = 1;
 private bool hasNGuid;
 private Int64 nGuid_ = 0;
 public bool HasNGuid {
 get { return hasNGuid; }
 }
 public Int64 NGuid {
 get { return nGuid_; }
 set { SetNGuid(value); }
 }
 public void SetNGuid(Int64 value) { 
 hasNGuid = true;
 nGuid_ = value;
 }

public const int nameFieldNumber = 2;
 private bool hasName;
 private string name_ = "";
 public bool HasName {
 get { return hasName; }
 }
 public string Name {
 get { return name_; }
 set { SetName(value); }
 }
 public void SetName(string value) { 
 hasName = true;
 name_ = value;
 }

public const int nTempleIDFieldNumber = 3;
 private bool hasNTempleID;
 private Int32 nTempleID_ = 0;
 public bool HasNTempleID {
 get { return hasNTempleID; }
 }
 public Int32 NTempleID {
 get { return nTempleID_; }
 set { SetNTempleID(value); }
 }
 public void SetNTempleID(Int32 value) { 
 hasNTempleID = true;
 nTempleID_ = value;
 }

public const int nLevelFieldNumber = 4;
 private bool hasNLevel;
 private Int32 nLevel_ = 0;
 public bool HasNLevel {
 get { return hasNLevel; }
 }
 public Int32 NLevel {
 get { return nLevel_; }
 set { SetNLevel(value); }
 }
 public void SetNLevel(Int32 value) { 
 hasNLevel = true;
 nLevel_ = value;
 }

public const int nRankFieldNumber = 5;
 private bool hasNRank;
 private Int32 nRank_ = 0;
 public bool HasNRank {
 get { return hasNRank; }
 }
 public Int32 NRank {
 get { return nRank_; }
 set { SetNRank(value); }
 }
 public void SetNRank(Int32 value) { 
 hasNRank = true;
 nRank_ = value;
 }

public const int nFightFieldNumber = 6;
 private bool hasNFight;
 private Int32 nFight_ = 0;
 public bool HasNFight {
 get { return hasNFight; }
 }
 public Int32 NFight {
 get { return nFight_; }
 set { SetNFight(value); }
 }
 public void SetNFight(Int32 value) { 
 hasNFight = true;
 nFight_ = value;
 }

public const int skill_levelFieldNumber = 7;
 private bool hasSkill_level;
 private Int32 skill_level_ = 0;
 public bool HasSkill_level {
 get { return hasSkill_level; }
 }
 public Int32 Skill_level {
 get { return skill_level_; }
 set { SetSkill_level(value); }
 }
 public void SetSkill_level(Int32 value) { 
 hasSkill_level = true;
 skill_level_ = value;
 }

public const int add_quality_attFieldNumber = 8;
 private bool hasAdd_quality_att;
 private Int32 add_quality_att_ = 0;
 public bool HasAdd_quality_att {
 get { return hasAdd_quality_att; }
 }
 public Int32 Add_quality_att {
 get { return add_quality_att_; }
 set { SetAdd_quality_att(value); }
 }
 public void SetAdd_quality_att(Int32 value) { 
 hasAdd_quality_att = true;
 add_quality_att_ = value;
 }

public const int add_quality_hpFieldNumber = 9;
 private bool hasAdd_quality_hp;
 private Int32 add_quality_hp_ = 0;
 public bool HasAdd_quality_hp {
 get { return hasAdd_quality_hp; }
 }
 public Int32 Add_quality_hp {
 get { return add_quality_hp_; }
 set { SetAdd_quality_hp(value); }
 }
 public void SetAdd_quality_hp(Int32 value) { 
 hasAdd_quality_hp = true;
 add_quality_hp_ = value;
 }

public const int studySkillLevFieldNumber = 10;
 private bool hasStudySkillLev;
 private Int32 studySkillLev_ = 0;
 public bool HasStudySkillLev {
 get { return hasStudySkillLev; }
 }
 public Int32 StudySkillLev {
 get { return studySkillLev_; }
 set { SetStudySkillLev(value); }
 }
 public void SetStudySkillLev(Int32 value) { 
 hasStudySkillLev = true;
 studySkillLev_ = value;
 }

public const int studySkillIDFieldNumber = 11;
 private bool hasStudySkillID;
 private Int32 studySkillID_ = 0;
 public bool HasStudySkillID {
 get { return hasStudySkillID; }
 }
 public Int32 StudySkillID {
 get { return studySkillID_; }
 set { SetStudySkillID(value); }
 }
 public void SetStudySkillID(Int32 value) { 
 hasStudySkillID = true;
 studySkillID_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasNGuid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, NGuid);
}
 if (HasName) {
size += pb::CodedOutputStream.ComputeStringSize(2, Name);
}
 if (HasNTempleID) {
size += pb::CodedOutputStream.ComputeInt32Size(3, NTempleID);
}
 if (HasNLevel) {
size += pb::CodedOutputStream.ComputeInt32Size(4, NLevel);
}
 if (HasNRank) {
size += pb::CodedOutputStream.ComputeInt32Size(5, NRank);
}
 if (HasNFight) {
size += pb::CodedOutputStream.ComputeInt32Size(6, NFight);
}
 if (HasSkill_level) {
size += pb::CodedOutputStream.ComputeInt32Size(7, Skill_level);
}
 if (HasAdd_quality_att) {
size += pb::CodedOutputStream.ComputeInt32Size(8, Add_quality_att);
}
 if (HasAdd_quality_hp) {
size += pb::CodedOutputStream.ComputeInt32Size(9, Add_quality_hp);
}
 if (HasStudySkillLev) {
size += pb::CodedOutputStream.ComputeInt32Size(10, StudySkillLev);
}
 if (HasStudySkillID) {
size += pb::CodedOutputStream.ComputeInt32Size(11, StudySkillID);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasNGuid) {
output.WriteInt64(1, NGuid);
}
 
if (HasName) {
output.WriteString(2, Name);
}
 
if (HasNTempleID) {
output.WriteInt32(3, NTempleID);
}
 
if (HasNLevel) {
output.WriteInt32(4, NLevel);
}
 
if (HasNRank) {
output.WriteInt32(5, NRank);
}
 
if (HasNFight) {
output.WriteInt32(6, NFight);
}
 
if (HasSkill_level) {
output.WriteInt32(7, Skill_level);
}
 
if (HasAdd_quality_att) {
output.WriteInt32(8, Add_quality_att);
}
 
if (HasAdd_quality_hp) {
output.WriteInt32(9, Add_quality_hp);
}
 
if (HasStudySkillLev) {
output.WriteInt32(10, StudySkillLev);
}
 
if (HasStudySkillID) {
output.WriteInt32(11, StudySkillID);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 PVPUserBaseData _inst = (PVPUserBaseData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.NGuid = input.ReadInt64();
break;
}
   case  18: {
 _inst.Name = input.ReadString();
break;
}
   case  24: {
 _inst.NTempleID = input.ReadInt32();
break;
}
   case  32: {
 _inst.NLevel = input.ReadInt32();
break;
}
   case  40: {
 _inst.NRank = input.ReadInt32();
break;
}
   case  48: {
 _inst.NFight = input.ReadInt32();
break;
}
   case  56: {
 _inst.Skill_level = input.ReadInt32();
break;
}
   case  64: {
 _inst.Add_quality_att = input.ReadInt32();
break;
}
   case  72: {
 _inst.Add_quality_hp = input.ReadInt32();
break;
}
   case  80: {
 _inst.StudySkillLev = input.ReadInt32();
break;
}
   case  88: {
 _inst.StudySkillID = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class ProductInfo : PacketDistributed
{

public const int goodsIdFieldNumber = 1;
 private bool hasGoodsId;
 private Int32 goodsId_ = 0;
 public bool HasGoodsId {
 get { return hasGoodsId; }
 }
 public Int32 GoodsId {
 get { return goodsId_; }
 set { SetGoodsId(value); }
 }
 public void SetGoodsId(Int32 value) { 
 hasGoodsId = true;
 goodsId_ = value;
 }

public const int productIdFieldNumber = 2;
 private bool hasProductId;
 private string productId_ = "";
 public bool HasProductId {
 get { return hasProductId; }
 }
 public string ProductId {
 get { return productId_; }
 set { SetProductId(value); }
 }
 public void SetProductId(string value) { 
 hasProductId = true;
 productId_ = value;
 }

public const int goodsIconFieldNumber = 3;
 private bool hasGoodsIcon;
 private string goodsIcon_ = "";
 public bool HasGoodsIcon {
 get { return hasGoodsIcon; }
 }
 public string GoodsIcon {
 get { return goodsIcon_; }
 set { SetGoodsIcon(value); }
 }
 public void SetGoodsIcon(string value) { 
 hasGoodsIcon = true;
 goodsIcon_ = value;
 }

public const int goodsNumberFieldNumber = 4;
 private bool hasGoodsNumber;
 private Int32 goodsNumber_ = 0;
 public bool HasGoodsNumber {
 get { return hasGoodsNumber; }
 }
 public Int32 GoodsNumber {
 get { return goodsNumber_; }
 set { SetGoodsNumber(value); }
 }
 public void SetGoodsNumber(Int32 value) { 
 hasGoodsNumber = true;
 goodsNumber_ = value;
 }

public const int goodsPriceFieldNumber = 5;
 private bool hasGoodsPrice;
 private string goodsPrice_ = "";
 public bool HasGoodsPrice {
 get { return hasGoodsPrice; }
 }
 public string GoodsPrice {
 get { return goodsPrice_; }
 set { SetGoodsPrice(value); }
 }
 public void SetGoodsPrice(string value) { 
 hasGoodsPrice = true;
 goodsPrice_ = value;
 }

public const int goodsNameFieldNumber = 6;
 private bool hasGoodsName;
 private string goodsName_ = "";
 public bool HasGoodsName {
 get { return hasGoodsName; }
 }
 public string GoodsName {
 get { return goodsName_; }
 set { SetGoodsName(value); }
 }
 public void SetGoodsName(string value) { 
 hasGoodsName = true;
 goodsName_ = value;
 }

public const int goodDecFieldNumber = 7;
 private bool hasGoodDec;
 private string goodDec_ = "";
 public bool HasGoodDec {
 get { return hasGoodDec; }
 }
 public string GoodDec {
 get { return goodDec_; }
 set { SetGoodDec(value); }
 }
 public void SetGoodDec(string value) { 
 hasGoodDec = true;
 goodDec_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasGoodsId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, GoodsId);
}
 if (HasProductId) {
size += pb::CodedOutputStream.ComputeStringSize(2, ProductId);
}
 if (HasGoodsIcon) {
size += pb::CodedOutputStream.ComputeStringSize(3, GoodsIcon);
}
 if (HasGoodsNumber) {
size += pb::CodedOutputStream.ComputeInt32Size(4, GoodsNumber);
}
 if (HasGoodsPrice) {
size += pb::CodedOutputStream.ComputeStringSize(5, GoodsPrice);
}
 if (HasGoodsName) {
size += pb::CodedOutputStream.ComputeStringSize(6, GoodsName);
}
 if (HasGoodDec) {
size += pb::CodedOutputStream.ComputeStringSize(7, GoodDec);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasGoodsId) {
output.WriteInt32(1, GoodsId);
}
 
if (HasProductId) {
output.WriteString(2, ProductId);
}
 
if (HasGoodsIcon) {
output.WriteString(3, GoodsIcon);
}
 
if (HasGoodsNumber) {
output.WriteInt32(4, GoodsNumber);
}
 
if (HasGoodsPrice) {
output.WriteString(5, GoodsPrice);
}
 
if (HasGoodsName) {
output.WriteString(6, GoodsName);
}
 
if (HasGoodDec) {
output.WriteString(7, GoodDec);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 ProductInfo _inst = (ProductInfo) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.GoodsId = input.ReadInt32();
break;
}
   case  18: {
 _inst.ProductId = input.ReadString();
break;
}
   case  26: {
 _inst.GoodsIcon = input.ReadString();
break;
}
   case  32: {
 _inst.GoodsNumber = input.ReadInt32();
break;
}
   case  42: {
 _inst.GoodsPrice = input.ReadString();
break;
}
   case  50: {
 _inst.GoodsName = input.ReadString();
break;
}
   case  58: {
 _inst.GoodDec = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasGoodsId) return false;
 if (!hasProductId) return false;
 if (!hasGoodsIcon) return false;
 if (!hasGoodsNumber) return false;
 if (!hasGoodsPrice) return false;
 if (!hasGoodsName) return false;
 return true;
 }

	}


[Serializable]
public class SC30038 : PacketDistributed
{

public const int productFieldNumber = 1;
 private pbc::PopsicleList<ProductInfo> product_ = new pbc::PopsicleList<ProductInfo>();
 public scg::IList<ProductInfo> productList {
 get { return pbc::Lists.AsReadOnly(product_); }
 }
 
 public int productCount {
 get { return product_.Count; }
 }
 
public ProductInfo GetProduct(int index) {
 return product_[index];
 }
 public void AddProduct(ProductInfo value) {
 product_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
foreach (ProductInfo element in productList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 
do{
foreach (ProductInfo element in productList) {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SC30038 _inst = (SC30038) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
ProductInfo subBuilder =  new ProductInfo();
input.ReadMessage(subBuilder);
_inst.AddProduct(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
 foreach (ProductInfo element in productList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SC30039 : PacketDistributed
{

public const int resultFieldNumber = 1;
 private bool hasResult;
 private Int32 result_ = 0;
 public bool HasResult {
 get { return hasResult; }
 }
 public Int32 Result {
 get { return result_; }
 set { SetResult(value); }
 }
 public void SetResult(Int32 value) { 
 hasResult = true;
 result_ = value;
 }

public const int goodsIdFieldNumber = 2;
 private bool hasGoodsId;
 private Int32 goodsId_ = 0;
 public bool HasGoodsId {
 get { return hasGoodsId; }
 }
 public Int32 GoodsId {
 get { return goodsId_; }
 set { SetGoodsId(value); }
 }
 public void SetGoodsId(Int32 value) { 
 hasGoodsId = true;
 goodsId_ = value;
 }

public const int playerDollarFieldNumber = 3;
 private bool hasPlayerDollar;
 private Int32 playerDollar_ = 0;
 public bool HasPlayerDollar {
 get { return hasPlayerDollar; }
 }
 public Int32 PlayerDollar {
 get { return playerDollar_; }
 set { SetPlayerDollar(value); }
 }
 public void SetPlayerDollar(Int32 value) { 
 hasPlayerDollar = true;
 playerDollar_ = value;
 }

public const int orderIdFieldNumber = 4;
 private bool hasOrderId;
 private string orderId_ = "";
 public bool HasOrderId {
 get { return hasOrderId; }
 }
 public string OrderId {
 get { return orderId_; }
 set { SetOrderId(value); }
 }
 public void SetOrderId(string value) { 
 hasOrderId = true;
 orderId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasResult) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Result);
}
 if (HasGoodsId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, GoodsId);
}
 if (HasPlayerDollar) {
size += pb::CodedOutputStream.ComputeInt32Size(3, PlayerDollar);
}
 if (HasOrderId) {
size += pb::CodedOutputStream.ComputeStringSize(4, OrderId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasResult) {
output.WriteInt32(1, Result);
}
 
if (HasGoodsId) {
output.WriteInt32(2, GoodsId);
}
 
if (HasPlayerDollar) {
output.WriteInt32(3, PlayerDollar);
}
 
if (HasOrderId) {
output.WriteString(4, OrderId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SC30039 _inst = (SC30039) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Result = input.ReadInt32();
break;
}
   case  16: {
 _inst.GoodsId = input.ReadInt32();
break;
}
   case  24: {
 _inst.PlayerDollar = input.ReadInt32();
break;
}
   case  34: {
 _inst.OrderId = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasResult) return false;
 if (!hasGoodsId) return false;
 if (!hasPlayerDollar) return false;
 if (!hasOrderId) return false;
 return true;
 }

	}


[Serializable]
public class SC30040 : PacketDistributed
{

public const int productFieldNumber = 1;
 private pbc::PopsicleList<ProductInfo> product_ = new pbc::PopsicleList<ProductInfo>();
 public scg::IList<ProductInfo> productList {
 get { return pbc::Lists.AsReadOnly(product_); }
 }
 
 public int productCount {
 get { return product_.Count; }
 }
 
public ProductInfo GetProduct(int index) {
 return product_[index];
 }
 public void AddProduct(ProductInfo value) {
 product_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
foreach (ProductInfo element in productList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 
do{
foreach (ProductInfo element in productList) {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SC30040 _inst = (SC30040) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
ProductInfo subBuilder =  new ProductInfo();
input.ReadMessage(subBuilder);
_inst.AddProduct(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
 foreach (ProductInfo element in productList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SC30041 : PacketDistributed
{

public const int resultFieldNumber = 1;
 private bool hasResult;
 private Int32 result_ = 0;
 public bool HasResult {
 get { return hasResult; }
 }
 public Int32 Result {
 get { return result_; }
 set { SetResult(value); }
 }
 public void SetResult(Int32 value) { 
 hasResult = true;
 result_ = value;
 }

public const int goodsIdFieldNumber = 2;
 private bool hasGoodsId;
 private Int32 goodsId_ = 0;
 public bool HasGoodsId {
 get { return hasGoodsId; }
 }
 public Int32 GoodsId {
 get { return goodsId_; }
 set { SetGoodsId(value); }
 }
 public void SetGoodsId(Int32 value) { 
 hasGoodsId = true;
 goodsId_ = value;
 }

public const int playerDollarFieldNumber = 3;
 private bool hasPlayerDollar;
 private Int32 playerDollar_ = 0;
 public bool HasPlayerDollar {
 get { return hasPlayerDollar; }
 }
 public Int32 PlayerDollar {
 get { return playerDollar_; }
 set { SetPlayerDollar(value); }
 }
 public void SetPlayerDollar(Int32 value) { 
 hasPlayerDollar = true;
 playerDollar_ = value;
 }

public const int orderIdFieldNumber = 4;
 private bool hasOrderId;
 private string orderId_ = "";
 public bool HasOrderId {
 get { return hasOrderId; }
 }
 public string OrderId {
 get { return orderId_; }
 set { SetOrderId(value); }
 }
 public void SetOrderId(string value) { 
 hasOrderId = true;
 orderId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasResult) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Result);
}
 if (HasGoodsId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, GoodsId);
}
 if (HasPlayerDollar) {
size += pb::CodedOutputStream.ComputeInt32Size(3, PlayerDollar);
}
 if (HasOrderId) {
size += pb::CodedOutputStream.ComputeStringSize(4, OrderId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasResult) {
output.WriteInt32(1, Result);
}
 
if (HasGoodsId) {
output.WriteInt32(2, GoodsId);
}
 
if (HasPlayerDollar) {
output.WriteInt32(3, PlayerDollar);
}
 
if (HasOrderId) {
output.WriteString(4, OrderId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SC30041 _inst = (SC30041) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Result = input.ReadInt32();
break;
}
   case  16: {
 _inst.GoodsId = input.ReadInt32();
break;
}
   case  24: {
 _inst.PlayerDollar = input.ReadInt32();
break;
}
   case  34: {
 _inst.OrderId = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasResult) return false;
 if (!hasGoodsId) return false;
 if (!hasPlayerDollar) return false;
 if (!hasOrderId) return false;
 return true;
 }

	}


[Serializable]
public class SCADDFriend : PacketDistributed
{

public const int stateFieldNumber = 1;
 private bool hasState;
 private Int32 state_ = 0;
 public bool HasState {
 get { return hasState; }
 }
 public Int32 State {
 get { return state_; }
 set { SetState(value); }
 }
 public void SetState(Int32 value) { 
 hasState = true;
 state_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasState) {
size += pb::CodedOutputStream.ComputeInt32Size(1, State);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasState) {
output.WriteInt32(1, State);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCADDFriend _inst = (SCADDFriend) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.State = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasState) return false;
 return true;
 }

	}


[Serializable]
public class SCAskActivity : PacketDistributed
{

public const int activityDataFieldNumber = 1;
 private bool hasActivityData;
 private ActivityInfo activityData_ =  new ActivityInfo();
 public bool HasActivityData {
 get { return hasActivityData; }
 }
 public ActivityInfo ActivityData {
 get { return activityData_; }
 set { SetActivityData(value); }
 }
 public void SetActivityData(ActivityInfo value) { 
 hasActivityData = true;
 activityData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int subsize = ActivityData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)ActivityData.SerializedSize());
ActivityData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCAskActivity _inst = (SCAskActivity) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
ActivityInfo subBuilder =  new ActivityInfo();
 input.ReadMessage(subBuilder);
_inst.ActivityData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasActivityData) {
if (!ActivityData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCAskChangeCardList : PacketDistributed
{

public const int infoFieldNumber = 1;
 private pbc::PopsicleList<ChangeCardInfo> info_ = new pbc::PopsicleList<ChangeCardInfo>();
 public scg::IList<ChangeCardInfo> infoList {
 get { return pbc::Lists.AsReadOnly(info_); }
 }
 
 public int infoCount {
 get { return info_.Count; }
 }
 
public ChangeCardInfo GetInfo(int index) {
 return info_[index];
 }
 public void AddInfo(ChangeCardInfo value) {
 info_.Add(value);
 }

public const int endTimeFieldNumber = 2;
 private bool hasEndTime;
 private Int64 endTime_ = 0;
 public bool HasEndTime {
 get { return hasEndTime; }
 }
 public Int64 EndTime {
 get { return endTime_; }
 set { SetEndTime(value); }
 }
 public void SetEndTime(Int64 value) { 
 hasEndTime = true;
 endTime_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
foreach (ChangeCardInfo element in infoList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 if (HasEndTime) {
size += pb::CodedOutputStream.ComputeInt64Size(2, EndTime);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 
do{
foreach (ChangeCardInfo element in infoList) {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 
if (HasEndTime) {
output.WriteInt64(2, EndTime);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCAskChangeCardList _inst = (SCAskChangeCardList) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
ChangeCardInfo subBuilder =  new ChangeCardInfo();
input.ReadMessage(subBuilder);
_inst.AddInfo(subBuilder);
break;
}
   case  16: {
 _inst.EndTime = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
 foreach (ChangeCardInfo element in infoList) {
if (!element.IsInitialized()) return false;
}
 if (!hasEndTime) return false;
 return true;
 }

	}


[Serializable]
public class SCAskPVPList : PacketDistributed
{

public const int nHeroRankFieldNumber = 1;
 private bool hasNHeroRank;
 private Int32 nHeroRank_ = 0;
 public bool HasNHeroRank {
 get { return hasNHeroRank; }
 }
 public Int32 NHeroRank {
 get { return nHeroRank_; }
 set { SetNHeroRank(value); }
 }
 public void SetNHeroRank(Int32 value) { 
 hasNHeroRank = true;
 nHeroRank_ = value;
 }

public const int nHeroPVPTimesFieldNumber = 2;
 private bool hasNHeroPVPTimes;
 private Int32 nHeroPVPTimes_ = 0;
 public bool HasNHeroPVPTimes {
 get { return hasNHeroPVPTimes; }
 }
 public Int32 NHeroPVPTimes {
 get { return nHeroPVPTimes_; }
 set { SetNHeroPVPTimes(value); }
 }
 public void SetNHeroPVPTimes(Int32 value) { 
 hasNHeroPVPTimes = true;
 nHeroPVPTimes_ = value;
 }

public const int nHeroScoreFieldNumber = 3;
 private bool hasNHeroScore;
 private Int64 nHeroScore_ = 0;
 public bool HasNHeroScore {
 get { return hasNHeroScore; }
 }
 public Int64 NHeroScore {
 get { return nHeroScore_; }
 set { SetNHeroScore(value); }
 }
 public void SetNHeroScore(Int64 value) { 
 hasNHeroScore = true;
 nHeroScore_ = value;
 }

public const int userInfoFieldNumber = 4;
 private pbc::PopsicleList<PVPUserBaseData> userInfo_ = new pbc::PopsicleList<PVPUserBaseData>();
 public scg::IList<PVPUserBaseData> userInfoList {
 get { return pbc::Lists.AsReadOnly(userInfo_); }
 }
 
 public int userInfoCount {
 get { return userInfo_.Count; }
 }
 
public PVPUserBaseData GetUserInfo(int index) {
 return userInfo_[index];
 }
 public void AddUserInfo(PVPUserBaseData value) {
 userInfo_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasNHeroRank) {
size += pb::CodedOutputStream.ComputeInt32Size(1, NHeroRank);
}
 if (HasNHeroPVPTimes) {
size += pb::CodedOutputStream.ComputeInt32Size(2, NHeroPVPTimes);
}
 if (HasNHeroScore) {
size += pb::CodedOutputStream.ComputeInt64Size(3, NHeroScore);
}
{
foreach (PVPUserBaseData element in userInfoList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)4) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasNHeroRank) {
output.WriteInt32(1, NHeroRank);
}
 
if (HasNHeroPVPTimes) {
output.WriteInt32(2, NHeroPVPTimes);
}
 
if (HasNHeroScore) {
output.WriteInt64(3, NHeroScore);
}

do{
foreach (PVPUserBaseData element in userInfoList) {
output.WriteTag((int)4, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCAskPVPList _inst = (SCAskPVPList) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.NHeroRank = input.ReadInt32();
break;
}
   case  16: {
 _inst.NHeroPVPTimes = input.ReadInt32();
break;
}
   case  24: {
 _inst.NHeroScore = input.ReadInt64();
break;
}
    case  34: {
PVPUserBaseData subBuilder =  new PVPUserBaseData();
input.ReadMessage(subBuilder);
_inst.AddUserInfo(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasNHeroRank) return false;
 if (!hasNHeroPVPTimes) return false;
 if (!hasNHeroScore) return false;
foreach (PVPUserBaseData element in userInfoList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCAskScoreShopFresh : PacketDistributed
{

public const int nScoreFieldNumber = 1;
 private bool hasNScore;
 private Int32 nScore_ = 0;
 public bool HasNScore {
 get { return hasNScore; }
 }
 public Int32 NScore {
 get { return nScore_; }
 set { SetNScore(value); }
 }
 public void SetNScore(Int32 value) { 
 hasNScore = true;
 nScore_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasNScore) {
size += pb::CodedOutputStream.ComputeInt32Size(1, NScore);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasNScore) {
output.WriteInt32(1, NScore);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCAskScoreShopFresh _inst = (SCAskScoreShopFresh) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.NScore = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasNScore) return false;
 return true;
 }

	}


[Serializable]
public class SCAskUserData : PacketDistributed
{

public const int baseDataFieldNumber = 1;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int bagDataFieldNumber = 2;
 private bool hasBagData;
 private PBUserBagData bagData_ =  new PBUserBagData();
 public bool HasBagData {
 get { return hasBagData; }
 }
 public PBUserBagData BagData {
 get { return bagData_; }
 set { SetBagData(value); }
 }
 public void SetBagData(PBUserBagData value) { 
 hasBagData = true;
 bagData_ = value;
 }

public const int copyDataFieldNumber = 3;
 private bool hasCopyData;
 private PBUserCopyData copyData_ =  new PBUserCopyData();
 public bool HasCopyData {
 get { return hasCopyData; }
 }
 public PBUserCopyData CopyData {
 get { return copyData_; }
 set { SetCopyData(value); }
 }
 public void SetCopyData(PBUserCopyData value) { 
 hasCopyData = true;
 copyData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = CopyData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BagData.SerializedSize());
BagData.WriteTo(output);

}
{
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)CopyData.SerializedSize());
CopyData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCAskUserData _inst = (SCAskUserData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
    case  18: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BagData = subBuilder;
break;
}
    case  26: {
PBUserCopyData subBuilder =  new PBUserCopyData();
 input.ReadMessage(subBuilder);
_inst.CopyData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
  if (HasBagData) {
if (!BagData.IsInitialized()) return false;
}
  if (HasCopyData) {
if (!CopyData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCAskWorldBossBattle : PacketDistributed
{

public const int bossIsDeadFieldNumber = 1;
 private bool hasBossIsDead;
 private Int32 bossIsDead_ = 0;
 public bool HasBossIsDead {
 get { return hasBossIsDead; }
 }
 public Int32 BossIsDead {
 get { return bossIsDead_; }
 set { SetBossIsDead(value); }
 }
 public void SetBossIsDead(Int32 value) { 
 hasBossIsDead = true;
 bossIsDead_ = value;
 }

public const int battleFieldNumber = 2;
 private bool hasBattle;
 private DataBattle battle_ =  new DataBattle();
 public bool HasBattle {
 get { return hasBattle; }
 }
 public DataBattle Battle {
 get { return battle_; }
 set { SetBattle(value); }
 }
 public void SetBattle(DataBattle value) { 
 hasBattle = true;
 battle_ = value;
 }

public const int baseDataFieldNumber = 3;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int currentDamageFieldNumber = 4;
 private bool hasCurrentDamage;
 private Int64 currentDamage_ = 0;
 public bool HasCurrentDamage {
 get { return hasCurrentDamage; }
 }
 public Int64 CurrentDamage {
 get { return currentDamage_; }
 set { SetCurrentDamage(value); }
 }
 public void SetCurrentDamage(Int64 value) { 
 hasCurrentDamage = true;
 currentDamage_ = value;
 }

public const int rewardMoneyFieldNumber = 5;
 private bool hasRewardMoney;
 private Int32 rewardMoney_ = 0;
 public bool HasRewardMoney {
 get { return hasRewardMoney; }
 }
 public Int32 RewardMoney {
 get { return rewardMoney_; }
 set { SetRewardMoney(value); }
 }
 public void SetRewardMoney(Int32 value) { 
 hasRewardMoney = true;
 rewardMoney_ = value;
 }

public const int currentBossHpFieldNumber = 6;
 private bool hasCurrentBossHp;
 private Int64 currentBossHp_ = 0;
 public bool HasCurrentBossHp {
 get { return hasCurrentBossHp; }
 }
 public Int64 CurrentBossHp {
 get { return currentBossHp_; }
 set { SetCurrentBossHp(value); }
 }
 public void SetCurrentBossHp(Int64 value) { 
 hasCurrentBossHp = true;
 currentBossHp_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasBossIsDead) {
size += pb::CodedOutputStream.ComputeInt32Size(1, BossIsDead);
}
{
int subsize = Battle.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 if (HasCurrentDamage) {
size += pb::CodedOutputStream.ComputeInt64Size(4, CurrentDamage);
}
 if (HasRewardMoney) {
size += pb::CodedOutputStream.ComputeInt32Size(5, RewardMoney);
}
 if (HasCurrentBossHp) {
size += pb::CodedOutputStream.ComputeInt64Size(6, CurrentBossHp);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasBossIsDead) {
output.WriteInt32(1, BossIsDead);
}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)Battle.SerializedSize());
Battle.WriteTo(output);

}
{
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
 
if (HasCurrentDamage) {
output.WriteInt64(4, CurrentDamage);
}
 
if (HasRewardMoney) {
output.WriteInt32(5, RewardMoney);
}
 
if (HasCurrentBossHp) {
output.WriteInt64(6, CurrentBossHp);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCAskWorldBossBattle _inst = (SCAskWorldBossBattle) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.BossIsDead = input.ReadInt32();
break;
}
    case  18: {
DataBattle subBuilder =  new DataBattle();
 input.ReadMessage(subBuilder);
_inst.Battle = subBuilder;
break;
}
    case  26: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
   case  32: {
 _inst.CurrentDamage = input.ReadInt64();
break;
}
   case  40: {
 _inst.RewardMoney = input.ReadInt32();
break;
}
   case  48: {
 _inst.CurrentBossHp = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasBossIsDead) return false;
  if (HasBattle) {
if (!Battle.IsInitialized()) return false;
}
  if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCAskWorldBossInfo : PacketDistributed
{

public const int activeBossFieldNumber = 1;
 private bool hasActiveBoss;
 private WorldBoss activeBoss_ =  new WorldBoss();
 public bool HasActiveBoss {
 get { return hasActiveBoss; }
 }
 public WorldBoss ActiveBoss {
 get { return activeBoss_; }
 set { SetActiveBoss(value); }
 }
 public void SetActiveBoss(WorldBoss value) { 
 hasActiveBoss = true;
 activeBoss_ = value;
 }

public const int lastKillInfoFieldNumber = 2;
 private bool hasLastKillInfo;
 private WorldBossKillInfo lastKillInfo_ =  new WorldBossKillInfo();
 public bool HasLastKillInfo {
 get { return hasLastKillInfo; }
 }
 public WorldBossKillInfo LastKillInfo {
 get { return lastKillInfo_; }
 set { SetLastKillInfo(value); }
 }
 public void SetLastKillInfo(WorldBossKillInfo value) { 
 hasLastKillInfo = true;
 lastKillInfo_ = value;
 }

public const int currentKillInfoFieldNumber = 3;
 private bool hasCurrentKillInfo;
 private WorldBossAttInfo currentKillInfo_ =  new WorldBossAttInfo();
 public bool HasCurrentKillInfo {
 get { return hasCurrentKillInfo; }
 }
 public WorldBossAttInfo CurrentKillInfo {
 get { return currentKillInfo_; }
 set { SetCurrentKillInfo(value); }
 }
 public void SetCurrentKillInfo(WorldBossAttInfo value) { 
 hasCurrentKillInfo = true;
 currentKillInfo_ = value;
 }

public const int hasRewardFieldNumber = 4;
 private bool hasHasReward;
 private Int32 hasReward_ = 0;
 public bool HasHasReward {
 get { return hasHasReward; }
 }
 public Int32 HasReward {
 get { return hasReward_; }
 set { SetHasReward(value); }
 }
 public void SetHasReward(Int32 value) { 
 hasHasReward = true;
 hasReward_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int subsize = ActiveBoss.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = LastKillInfo.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = CurrentKillInfo.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 if (HasHasReward) {
size += pb::CodedOutputStream.ComputeInt32Size(4, HasReward);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)ActiveBoss.SerializedSize());
ActiveBoss.WriteTo(output);

}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)LastKillInfo.SerializedSize());
LastKillInfo.WriteTo(output);

}
{
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)CurrentKillInfo.SerializedSize());
CurrentKillInfo.WriteTo(output);

}
 
if (HasHasReward) {
output.WriteInt32(4, HasReward);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCAskWorldBossInfo _inst = (SCAskWorldBossInfo) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
WorldBoss subBuilder =  new WorldBoss();
 input.ReadMessage(subBuilder);
_inst.ActiveBoss = subBuilder;
break;
}
    case  18: {
WorldBossKillInfo subBuilder =  new WorldBossKillInfo();
 input.ReadMessage(subBuilder);
_inst.LastKillInfo = subBuilder;
break;
}
    case  26: {
WorldBossAttInfo subBuilder =  new WorldBossAttInfo();
 input.ReadMessage(subBuilder);
_inst.CurrentKillInfo = subBuilder;
break;
}
   case  32: {
 _inst.HasReward = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasActiveBoss) {
if (!ActiveBoss.IsInitialized()) return false;
}
  if (HasLastKillInfo) {
if (!LastKillInfo.IsInitialized()) return false;
}
  if (HasCurrentKillInfo) {
if (!CurrentKillInfo.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCBGZ : PacketDistributed
{

public const int showRewardIDFieldNumber = 1;
 private bool hasShowRewardID;
 private Int32 showRewardID_ = 0;
 public bool HasShowRewardID {
 get { return hasShowRewardID; }
 }
 public Int32 ShowRewardID {
 get { return showRewardID_; }
 set { SetShowRewardID(value); }
 }
 public void SetShowRewardID(Int32 value) { 
 hasShowRewardID = true;
 showRewardID_ = value;
 }

public const int baseDataFieldNumber = 2;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasShowRewardID) {
size += pb::CodedOutputStream.ComputeInt32Size(1, ShowRewardID);
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasShowRewardID) {
output.WriteInt32(1, ShowRewardID);
}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCBGZ _inst = (SCBGZ) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.ShowRewardID = input.ReadInt32();
break;
}
    case  18: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasShowRewardID) return false;
  if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCBattleData : PacketDistributed
{

public const int battleFieldNumber = 1;
 private bool hasBattle;
 private DataBattle battle_ =  new DataBattle();
 public bool HasBattle {
 get { return hasBattle; }
 }
 public DataBattle Battle {
 get { return battle_; }
 set { SetBattle(value); }
 }
 public void SetBattle(DataBattle value) { 
 hasBattle = true;
 battle_ = value;
 }

public const int baseDataFieldNumber = 2;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int bagDataFieldNumber = 3;
 private bool hasBagData;
 private PBUserBagData bagData_ =  new PBUserBagData();
 public bool HasBagData {
 get { return hasBagData; }
 }
 public PBUserBagData BagData {
 get { return bagData_; }
 set { SetBagData(value); }
 }
 public void SetBagData(PBUserBagData value) { 
 hasBagData = true;
 bagData_ = value;
 }

public const int copyDataFieldNumber = 4;
 private bool hasCopyData;
 private PBUserCopyData copyData_ =  new PBUserCopyData();
 public bool HasCopyData {
 get { return hasCopyData; }
 }
 public PBUserCopyData CopyData {
 get { return copyData_; }
 set { SetCopyData(value); }
 }
 public void SetCopyData(PBUserCopyData value) { 
 hasCopyData = true;
 copyData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int subsize = Battle.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = CopyData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)4) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)Battle.SerializedSize());
Battle.WriteTo(output);

}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
{
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BagData.SerializedSize());
BagData.WriteTo(output);

}
{
output.WriteTag((int)4, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)CopyData.SerializedSize());
CopyData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCBattleData _inst = (SCBattleData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
DataBattle subBuilder =  new DataBattle();
 input.ReadMessage(subBuilder);
_inst.Battle = subBuilder;
break;
}
    case  18: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
    case  26: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BagData = subBuilder;
break;
}
    case  34: {
PBUserCopyData subBuilder =  new PBUserCopyData();
 input.ReadMessage(subBuilder);
_inst.CopyData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasBattle) {
if (!Battle.IsInitialized()) return false;
}
  if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
  if (HasBagData) {
if (!BagData.IsInitialized()) return false;
}
  if (HasCopyData) {
if (!CopyData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCBindAccount : PacketDistributed
{

public const int retCodeFieldNumber = 1;
 private bool hasRetCode;
 private Int32 retCode_ = 0;
 public bool HasRetCode {
 get { return hasRetCode; }
 }
 public Int32 RetCode {
 get { return retCode_; }
 set { SetRetCode(value); }
 }
 public void SetRetCode(Int32 value) { 
 hasRetCode = true;
 retCode_ = value;
 }

public const int accountidFieldNumber = 2;
 private bool hasAccountid;
 private Int32 accountid_ = 0;
 public bool HasAccountid {
 get { return hasAccountid; }
 }
 public Int32 Accountid {
 get { return accountid_; }
 set { SetAccountid(value); }
 }
 public void SetAccountid(Int32 value) { 
 hasAccountid = true;
 accountid_ = value;
 }

public const int usernameFieldNumber = 3;
 private bool hasUsername;
 private string username_ = "";
 public bool HasUsername {
 get { return hasUsername; }
 }
 public string Username {
 get { return username_; }
 set { SetUsername(value); }
 }
 public void SetUsername(string value) { 
 hasUsername = true;
 username_ = value;
 }

public const int passwardFieldNumber = 4;
 private bool hasPassward;
 private string passward_ = "";
 public bool HasPassward {
 get { return hasPassward; }
 }
 public string Passward {
 get { return passward_; }
 set { SetPassward(value); }
 }
 public void SetPassward(string value) { 
 hasPassward = true;
 passward_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasRetCode) {
size += pb::CodedOutputStream.ComputeInt32Size(1, RetCode);
}
 if (HasAccountid) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Accountid);
}
 if (HasUsername) {
size += pb::CodedOutputStream.ComputeStringSize(3, Username);
}
 if (HasPassward) {
size += pb::CodedOutputStream.ComputeStringSize(4, Passward);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasRetCode) {
output.WriteInt32(1, RetCode);
}
 
if (HasAccountid) {
output.WriteInt32(2, Accountid);
}
 
if (HasUsername) {
output.WriteString(3, Username);
}
 
if (HasPassward) {
output.WriteString(4, Passward);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCBindAccount _inst = (SCBindAccount) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.RetCode = input.ReadInt32();
break;
}
   case  16: {
 _inst.Accountid = input.ReadInt32();
break;
}
   case  26: {
 _inst.Username = input.ReadString();
break;
}
   case  34: {
 _inst.Passward = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasRetCode) return false;
 return true;
 }

	}


[Serializable]
public class SCBuyMoney : PacketDistributed
{

public const int resultFieldNumber = 1;
 private bool hasResult;
 private Int32 result_ = 0;
 public bool HasResult {
 get { return hasResult; }
 }
 public Int32 Result {
 get { return result_; }
 set { SetResult(value); }
 }
 public void SetResult(Int32 value) { 
 hasResult = true;
 result_ = value;
 }

public const int baseDataFieldNumber = 2;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasResult) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Result);
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasResult) {
output.WriteInt32(1, Result);
}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCBuyMoney _inst = (SCBuyMoney) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Result = input.ReadInt32();
break;
}
    case  18: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasResult) return false;
  if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCBuyPower : PacketDistributed
{

public const int resultFieldNumber = 1;
 private bool hasResult;
 private Int32 result_ = 0;
 public bool HasResult {
 get { return hasResult; }
 }
 public Int32 Result {
 get { return result_; }
 set { SetResult(value); }
 }
 public void SetResult(Int32 value) { 
 hasResult = true;
 result_ = value;
 }

public const int baseDataFieldNumber = 2;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasResult) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Result);
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasResult) {
output.WriteInt32(1, Result);
}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCBuyPower _inst = (SCBuyPower) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Result = input.ReadInt32();
break;
}
    case  18: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasResult) return false;
  if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCCYouPayVerifyChargeRet : PacketDistributed
{

public const int resultFieldNumber = 1;
 private bool hasResult;
 private Int32 result_ = 0;
 public bool HasResult {
 get { return hasResult; }
 }
 public Int32 Result {
 get { return result_; }
 set { SetResult(value); }
 }
 public void SetResult(Int32 value) { 
 hasResult = true;
 result_ = value;
 }

public const int goodsIdFieldNumber = 2;
 private bool hasGoodsId;
 private Int32 goodsId_ = 0;
 public bool HasGoodsId {
 get { return hasGoodsId; }
 }
 public Int32 GoodsId {
 get { return goodsId_; }
 set { SetGoodsId(value); }
 }
 public void SetGoodsId(Int32 value) { 
 hasGoodsId = true;
 goodsId_ = value;
 }

public const int playerDollarFieldNumber = 3;
 private bool hasPlayerDollar;
 private Int32 playerDollar_ = 0;
 public bool HasPlayerDollar {
 get { return hasPlayerDollar; }
 }
 public Int32 PlayerDollar {
 get { return playerDollar_; }
 set { SetPlayerDollar(value); }
 }
 public void SetPlayerDollar(Int32 value) { 
 hasPlayerDollar = true;
 playerDollar_ = value;
 }

public const int orderIdFieldNumber = 4;
 private bool hasOrderId;
 private string orderId_ = "";
 public bool HasOrderId {
 get { return hasOrderId; }
 }
 public string OrderId {
 get { return orderId_; }
 set { SetOrderId(value); }
 }
 public void SetOrderId(string value) { 
 hasOrderId = true;
 orderId_ = value;
 }

public const int goodsPriceFieldNumber = 5;
 private bool hasGoodsPrice;
 private Int32 goodsPrice_ = 0;
 public bool HasGoodsPrice {
 get { return hasGoodsPrice; }
 }
 public Int32 GoodsPrice {
 get { return goodsPrice_; }
 set { SetGoodsPrice(value); }
 }
 public void SetGoodsPrice(Int32 value) { 
 hasGoodsPrice = true;
 goodsPrice_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasResult) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Result);
}
 if (HasGoodsId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, GoodsId);
}
 if (HasPlayerDollar) {
size += pb::CodedOutputStream.ComputeInt32Size(3, PlayerDollar);
}
 if (HasOrderId) {
size += pb::CodedOutputStream.ComputeStringSize(4, OrderId);
}
 if (HasGoodsPrice) {
size += pb::CodedOutputStream.ComputeInt32Size(5, GoodsPrice);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasResult) {
output.WriteInt32(1, Result);
}
 
if (HasGoodsId) {
output.WriteInt32(2, GoodsId);
}
 
if (HasPlayerDollar) {
output.WriteInt32(3, PlayerDollar);
}
 
if (HasOrderId) {
output.WriteString(4, OrderId);
}
 
if (HasGoodsPrice) {
output.WriteInt32(5, GoodsPrice);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCCYouPayVerifyChargeRet _inst = (SCCYouPayVerifyChargeRet) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Result = input.ReadInt32();
break;
}
   case  16: {
 _inst.GoodsId = input.ReadInt32();
break;
}
   case  24: {
 _inst.PlayerDollar = input.ReadInt32();
break;
}
   case  34: {
 _inst.OrderId = input.ReadString();
break;
}
   case  40: {
 _inst.GoodsPrice = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class SCCYouProductList : PacketDistributed
{

public const int productFieldNumber = 1;
 private pbc::PopsicleList<ProductInfo> product_ = new pbc::PopsicleList<ProductInfo>();
 public scg::IList<ProductInfo> productList {
 get { return pbc::Lists.AsReadOnly(product_); }
 }
 
 public int productCount {
 get { return product_.Count; }
 }
 
public ProductInfo GetProduct(int index) {
 return product_[index];
 }
 public void AddProduct(ProductInfo value) {
 product_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
foreach (ProductInfo element in productList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 
do{
foreach (ProductInfo element in productList) {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCCYouProductList _inst = (SCCYouProductList) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
ProductInfo subBuilder =  new ProductInfo();
input.ReadMessage(subBuilder);
_inst.AddProduct(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
 foreach (ProductInfo element in productList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCCYouVerifyCharge : PacketDistributed
{

public const int resultFieldNumber = 1;
 private bool hasResult;
 private Int32 result_ = 0;
 public bool HasResult {
 get { return hasResult; }
 }
 public Int32 Result {
 get { return result_; }
 set { SetResult(value); }
 }
 public void SetResult(Int32 value) { 
 hasResult = true;
 result_ = value;
 }

public const int goodsIdFieldNumber = 2;
 private bool hasGoodsId;
 private Int32 goodsId_ = 0;
 public bool HasGoodsId {
 get { return hasGoodsId; }
 }
 public Int32 GoodsId {
 get { return goodsId_; }
 set { SetGoodsId(value); }
 }
 public void SetGoodsId(Int32 value) { 
 hasGoodsId = true;
 goodsId_ = value;
 }

public const int playerDollarFieldNumber = 3;
 private bool hasPlayerDollar;
 private Int32 playerDollar_ = 0;
 public bool HasPlayerDollar {
 get { return hasPlayerDollar; }
 }
 public Int32 PlayerDollar {
 get { return playerDollar_; }
 set { SetPlayerDollar(value); }
 }
 public void SetPlayerDollar(Int32 value) { 
 hasPlayerDollar = true;
 playerDollar_ = value;
 }

public const int orderIdFieldNumber = 4;
 private bool hasOrderId;
 private string orderId_ = "";
 public bool HasOrderId {
 get { return hasOrderId; }
 }
 public string OrderId {
 get { return orderId_; }
 set { SetOrderId(value); }
 }
 public void SetOrderId(string value) { 
 hasOrderId = true;
 orderId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasResult) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Result);
}
 if (HasGoodsId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, GoodsId);
}
 if (HasPlayerDollar) {
size += pb::CodedOutputStream.ComputeInt32Size(3, PlayerDollar);
}
 if (HasOrderId) {
size += pb::CodedOutputStream.ComputeStringSize(4, OrderId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasResult) {
output.WriteInt32(1, Result);
}
 
if (HasGoodsId) {
output.WriteInt32(2, GoodsId);
}
 
if (HasPlayerDollar) {
output.WriteInt32(3, PlayerDollar);
}
 
if (HasOrderId) {
output.WriteString(4, OrderId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCCYouVerifyCharge _inst = (SCCYouVerifyCharge) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Result = input.ReadInt32();
break;
}
   case  16: {
 _inst.GoodsId = input.ReadInt32();
break;
}
   case  24: {
 _inst.PlayerDollar = input.ReadInt32();
break;
}
   case  34: {
 _inst.OrderId = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasResult) return false;
 if (!hasGoodsId) return false;
 if (!hasPlayerDollar) return false;
 if (!hasOrderId) return false;
 return true;
 }

	}


[Serializable]
public class SCCardCombiningRet : PacketDistributed
{

public const int combine_resultFieldNumber = 1;
 private bool hasCombine_result;
 private Int32 combine_result_ = 0;
 public bool HasCombine_result {
 get { return hasCombine_result; }
 }
 public Int32 Combine_result {
 get { return combine_result_; }
 set { SetCombine_result(value); }
 }
 public void SetCombine_result(Int32 value) { 
 hasCombine_result = true;
 combine_result_ = value;
 }

public const int baseDataFieldNumber = 2;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int bagDataFieldNumber = 3;
 private bool hasBagData;
 private PBUserBagData bagData_ =  new PBUserBagData();
 public bool HasBagData {
 get { return hasBagData; }
 }
 public PBUserBagData BagData {
 get { return bagData_; }
 set { SetBagData(value); }
 }
 public void SetBagData(PBUserBagData value) { 
 hasBagData = true;
 bagData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasCombine_result) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Combine_result);
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasCombine_result) {
output.WriteInt32(1, Combine_result);
}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
{
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BagData.SerializedSize());
BagData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCCardCombiningRet _inst = (SCCardCombiningRet) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Combine_result = input.ReadInt32();
break;
}
    case  18: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
    case  26: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BagData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasCombine_result) return false;
  if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
  if (HasBagData) {
if (!BagData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCCardEvolveRet : PacketDistributed
{

public const int evolve_resultFieldNumber = 1;
 private bool hasEvolve_result;
 private Int32 evolve_result_ = 0;
 public bool HasEvolve_result {
 get { return hasEvolve_result; }
 }
 public Int32 Evolve_result {
 get { return evolve_result_; }
 set { SetEvolve_result(value); }
 }
 public void SetEvolve_result(Int32 value) { 
 hasEvolve_result = true;
 evolve_result_ = value;
 }

public const int bagDataFieldNumber = 2;
 private bool hasBagData;
 private PBUserBagData bagData_ =  new PBUserBagData();
 public bool HasBagData {
 get { return hasBagData; }
 }
 public PBUserBagData BagData {
 get { return bagData_; }
 set { SetBagData(value); }
 }
 public void SetBagData(PBUserBagData value) { 
 hasBagData = true;
 bagData_ = value;
 }

public const int baseDataFieldNumber = 3;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasEvolve_result) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Evolve_result);
}
{
int subsize = BagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasEvolve_result) {
output.WriteInt32(1, Evolve_result);
}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BagData.SerializedSize());
BagData.WriteTo(output);

}
{
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCCardEvolveRet _inst = (SCCardEvolveRet) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Evolve_result = input.ReadInt32();
break;
}
    case  18: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BagData = subBuilder;
break;
}
    case  26: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasEvolve_result) return false;
  if (HasBagData) {
if (!BagData.IsInitialized()) return false;
}
  if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCCardStrengthenRet : PacketDistributed
{

public const int streng_resultFieldNumber = 1;
 private bool hasStreng_result;
 private Int32 streng_result_ = 0;
 public bool HasStreng_result {
 get { return hasStreng_result; }
 }
 public Int32 Streng_result {
 get { return streng_result_; }
 set { SetStreng_result(value); }
 }
 public void SetStreng_result(Int32 value) { 
 hasStreng_result = true;
 streng_result_ = value;
 }

public const int baseDataFieldNumber = 2;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int bagDataFieldNumber = 3;
 private bool hasBagData;
 private PBUserBagData bagData_ =  new PBUserBagData();
 public bool HasBagData {
 get { return hasBagData; }
 }
 public PBUserBagData BagData {
 get { return bagData_; }
 set { SetBagData(value); }
 }
 public void SetBagData(PBUserBagData value) { 
 hasBagData = true;
 bagData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasStreng_result) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Streng_result);
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasStreng_result) {
output.WriteInt32(1, Streng_result);
}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
{
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BagData.SerializedSize());
BagData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCCardStrengthenRet _inst = (SCCardStrengthenRet) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Streng_result = input.ReadInt32();
break;
}
    case  18: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
    case  26: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BagData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasStreng_result) return false;
  if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
  if (HasBagData) {
if (!BagData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCChangeCardConfirm : PacketDistributed
{

public const int resultFieldNumber = 1;
 private bool hasResult;
 private Int32 result_ = 0;
 public bool HasResult {
 get { return hasResult; }
 }
 public Int32 Result {
 get { return result_; }
 set { SetResult(value); }
 }
 public void SetResult(Int32 value) { 
 hasResult = true;
 result_ = value;
 }

public const int templeIDFieldNumber = 2;
 private bool hasTempleID;
 private Int32 templeID_ = 0;
 public bool HasTempleID {
 get { return hasTempleID; }
 }
 public Int32 TempleID {
 get { return templeID_; }
 set { SetTempleID(value); }
 }
 public void SetTempleID(Int32 value) { 
 hasTempleID = true;
 templeID_ = value;
 }

public const int baseDataFieldNumber = 3;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int bagDataFieldNumber = 4;
 private bool hasBagData;
 private PBUserBagData bagData_ =  new PBUserBagData();
 public bool HasBagData {
 get { return hasBagData; }
 }
 public PBUserBagData BagData {
 get { return bagData_; }
 set { SetBagData(value); }
 }
 public void SetBagData(PBUserBagData value) { 
 hasBagData = true;
 bagData_ = value;
 }

public const int copyDataFieldNumber = 5;
 private bool hasCopyData;
 private PBUserCopyData copyData_ =  new PBUserCopyData();
 public bool HasCopyData {
 get { return hasCopyData; }
 }
 public PBUserCopyData CopyData {
 get { return copyData_; }
 set { SetCopyData(value); }
 }
 public void SetCopyData(PBUserCopyData value) { 
 hasCopyData = true;
 copyData_ = value;
 }

public const int activityDataFieldNumber = 6;
 private bool hasActivityData;
 private ActivityInfo activityData_ =  new ActivityInfo();
 public bool HasActivityData {
 get { return hasActivityData; }
 }
 public ActivityInfo ActivityData {
 get { return activityData_; }
 set { SetActivityData(value); }
 }
 public void SetActivityData(ActivityInfo value) { 
 hasActivityData = true;
 activityData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasResult) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Result);
}
 if (HasTempleID) {
size += pb::CodedOutputStream.ComputeInt32Size(2, TempleID);
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)4) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = CopyData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)5) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = ActivityData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)6) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasResult) {
output.WriteInt32(1, Result);
}
 
if (HasTempleID) {
output.WriteInt32(2, TempleID);
}
{
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
{
output.WriteTag((int)4, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BagData.SerializedSize());
BagData.WriteTo(output);

}
{
output.WriteTag((int)5, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)CopyData.SerializedSize());
CopyData.WriteTo(output);

}
{
output.WriteTag((int)6, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)ActivityData.SerializedSize());
ActivityData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCChangeCardConfirm _inst = (SCChangeCardConfirm) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Result = input.ReadInt32();
break;
}
   case  16: {
 _inst.TempleID = input.ReadInt32();
break;
}
    case  26: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
    case  34: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BagData = subBuilder;
break;
}
    case  42: {
PBUserCopyData subBuilder =  new PBUserCopyData();
 input.ReadMessage(subBuilder);
_inst.CopyData = subBuilder;
break;
}
    case  50: {
ActivityInfo subBuilder =  new ActivityInfo();
 input.ReadMessage(subBuilder);
_inst.ActivityData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasResult) return false;
 if (!hasTempleID) return false;
  if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
  if (HasBagData) {
if (!BagData.IsInitialized()) return false;
}
  if (HasCopyData) {
if (!CopyData.IsInitialized()) return false;
}
  if (HasActivityData) {
if (!ActivityData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCChangeMember : PacketDistributed
{

public const int issuccessFieldNumber = 1;
 private bool hasIssuccess;
 private Int32 issuccess_ = 0;
 public bool HasIssuccess {
 get { return hasIssuccess; }
 }
 public Int32 Issuccess {
 get { return issuccess_; }
 set { SetIssuccess(value); }
 }
 public void SetIssuccess(Int32 value) { 
 hasIssuccess = true;
 issuccess_ = value;
 }

public const int bagDataFieldNumber = 2;
 private bool hasBagData;
 private PBUserBagData bagData_ =  new PBUserBagData();
 public bool HasBagData {
 get { return hasBagData; }
 }
 public PBUserBagData BagData {
 get { return bagData_; }
 set { SetBagData(value); }
 }
 public void SetBagData(PBUserBagData value) { 
 hasBagData = true;
 bagData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasIssuccess) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Issuccess);
}
{
int subsize = BagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasIssuccess) {
output.WriteInt32(1, Issuccess);
}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BagData.SerializedSize());
BagData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCChangeMember _inst = (SCChangeMember) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Issuccess = input.ReadInt32();
break;
}
    case  18: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BagData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasIssuccess) return false;
  if (HasBagData) {
if (!BagData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCChangeName : PacketDistributed
{

public const int typeFieldNumber = 1;
 private bool hasType;
 private Int32 type_ = 0;
 public bool HasType {
 get { return hasType; }
 }
 public Int32 Type {
 get { return type_; }
 set { SetType(value); }
 }
 public void SetType(Int32 value) { 
 hasType = true;
 type_ = value;
 }

public const int baseDataFieldNumber = 2;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int bagDataFieldNumber = 3;
 private bool hasBagData;
 private PBUserBagData bagData_ =  new PBUserBagData();
 public bool HasBagData {
 get { return hasBagData; }
 }
 public PBUserBagData BagData {
 get { return bagData_; }
 set { SetBagData(value); }
 }
 public void SetBagData(PBUserBagData value) { 
 hasBagData = true;
 bagData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasType) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Type);
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasType) {
output.WriteInt32(1, Type);
}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
{
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BagData.SerializedSize());
BagData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCChangeName _inst = (SCChangeName) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Type = input.ReadInt32();
break;
}
    case  18: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
    case  26: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BagData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasType) return false;
  if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
  if (HasBagData) {
if (!BagData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCClearBattleData : PacketDistributed
{

public const int resultFieldNumber = 1;
 private bool hasResult;
 private Int32 result_ = 0;
 public bool HasResult {
 get { return hasResult; }
 }
 public Int32 Result {
 get { return result_; }
 set { SetResult(value); }
 }
 public void SetResult(Int32 value) { 
 hasResult = true;
 result_ = value;
 }

public const int baseDataFieldNumber = 2;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int copyDataFieldNumber = 3;
 private bool hasCopyData;
 private PBUserCopyData copyData_ =  new PBUserCopyData();
 public bool HasCopyData {
 get { return hasCopyData; }
 }
 public PBUserCopyData CopyData {
 get { return copyData_; }
 set { SetCopyData(value); }
 }
 public void SetCopyData(PBUserCopyData value) { 
 hasCopyData = true;
 copyData_ = value;
 }

public const int copyStarFieldNumber = 4;
 private bool hasCopyStar;
 private Int32 copyStar_ = 0;
 public bool HasCopyStar {
 get { return hasCopyStar; }
 }
 public Int32 CopyStar {
 get { return copyStar_; }
 set { SetCopyStar(value); }
 }
 public void SetCopyStar(Int32 value) { 
 hasCopyStar = true;
 copyStar_ = value;
 }

public const int suipianFieldNumber = 5;
 private pbc::PopsicleList<SuipianInfo> suipian_ = new pbc::PopsicleList<SuipianInfo>();
 public scg::IList<SuipianInfo> suipianList {
 get { return pbc::Lists.AsReadOnly(suipian_); }
 }
 
 public int suipianCount {
 get { return suipian_.Count; }
 }
 
public SuipianInfo GetSuipian(int index) {
 return suipian_[index];
 }
 public void AddSuipian(SuipianInfo value) {
 suipian_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasResult) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Result);
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = CopyData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 if (HasCopyStar) {
size += pb::CodedOutputStream.ComputeInt32Size(4, CopyStar);
}
{
foreach (SuipianInfo element in suipianList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)5) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasResult) {
output.WriteInt32(1, Result);
}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
{
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)CopyData.SerializedSize());
CopyData.WriteTo(output);

}
 
if (HasCopyStar) {
output.WriteInt32(4, CopyStar);
}

do{
foreach (SuipianInfo element in suipianList) {
output.WriteTag((int)5, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCClearBattleData _inst = (SCClearBattleData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Result = input.ReadInt32();
break;
}
    case  18: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
    case  26: {
PBUserCopyData subBuilder =  new PBUserCopyData();
 input.ReadMessage(subBuilder);
_inst.CopyData = subBuilder;
break;
}
   case  32: {
 _inst.CopyStar = input.ReadInt32();
break;
}
    case  42: {
SuipianInfo subBuilder =  new SuipianInfo();
input.ReadMessage(subBuilder);
_inst.AddSuipian(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
  if (HasCopyData) {
if (!CopyData.IsInitialized()) return false;
}
foreach (SuipianInfo element in suipianList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCClearPaiTaBattleData : PacketDistributed
{

public const int resultFieldNumber = 1;
 private bool hasResult;
 private Int32 result_ = 0;
 public bool HasResult {
 get { return hasResult; }
 }
 public Int32 Result {
 get { return result_; }
 set { SetResult(value); }
 }
 public void SetResult(Int32 value) { 
 hasResult = true;
 result_ = value;
 }

public const int baseDataFieldNumber = 2;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int moneynumFieldNumber = 4;
 private bool hasMoneynum;
 private Int32 moneynum_ = 0;
 public bool HasMoneynum {
 get { return hasMoneynum; }
 }
 public Int32 Moneynum {
 get { return moneynum_; }
 set { SetMoneynum(value); }
 }
 public void SetMoneynum(Int32 value) { 
 hasMoneynum = true;
 moneynum_ = value;
 }

public const int rubynumFieldNumber = 5;
 private bool hasRubynum;
 private Int32 rubynum_ = 0;
 public bool HasRubynum {
 get { return hasRubynum; }
 }
 public Int32 Rubynum {
 get { return rubynum_; }
 set { SetRubynum(value); }
 }
 public void SetRubynum(Int32 value) { 
 hasRubynum = true;
 rubynum_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasResult) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Result);
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 if (HasMoneynum) {
size += pb::CodedOutputStream.ComputeInt32Size(4, Moneynum);
}
 if (HasRubynum) {
size += pb::CodedOutputStream.ComputeInt32Size(5, Rubynum);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasResult) {
output.WriteInt32(1, Result);
}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
 
if (HasMoneynum) {
output.WriteInt32(4, Moneynum);
}
 
if (HasRubynum) {
output.WriteInt32(5, Rubynum);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCClearPaiTaBattleData _inst = (SCClearPaiTaBattleData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Result = input.ReadInt32();
break;
}
    case  18: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
   case  32: {
 _inst.Moneynum = input.ReadInt32();
break;
}
   case  40: {
 _inst.Rubynum = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCCommonProductListRet : PacketDistributed
{

public const int productFieldNumber = 1;
 private pbc::PopsicleList<ProductInfo> product_ = new pbc::PopsicleList<ProductInfo>();
 public scg::IList<ProductInfo> productList {
 get { return pbc::Lists.AsReadOnly(product_); }
 }
 
 public int productCount {
 get { return product_.Count; }
 }
 
public ProductInfo GetProduct(int index) {
 return product_[index];
 }
 public void AddProduct(ProductInfo value) {
 product_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
foreach (ProductInfo element in productList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 
do{
foreach (ProductInfo element in productList) {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCCommonProductListRet _inst = (SCCommonProductListRet) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
ProductInfo subBuilder =  new ProductInfo();
input.ReadMessage(subBuilder);
_inst.AddProduct(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
 foreach (ProductInfo element in productList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCDeleteFriend : PacketDistributed
{

public const int friend_guidFieldNumber = 2;
 private bool hasFriend_guid;
 private Int64 friend_guid_ = 0;
 public bool HasFriend_guid {
 get { return hasFriend_guid; }
 }
 public Int64 Friend_guid {
 get { return friend_guid_; }
 set { SetFriend_guid(value); }
 }
 public void SetFriend_guid(Int64 value) { 
 hasFriend_guid = true;
 friend_guid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasFriend_guid) {
size += pb::CodedOutputStream.ComputeInt64Size(2, Friend_guid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasFriend_guid) {
output.WriteInt64(2, Friend_guid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCDeleteFriend _inst = (SCDeleteFriend) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  16: {
 _inst.Friend_guid = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasFriend_guid) return false;
 return true;
 }

	}


[Serializable]
public class SCErrorMsg : PacketDistributed
{

public const int typeFieldNumber = 1;
 private bool hasType;
 private Int32 type_ = 0;
 public bool HasType {
 get { return hasType; }
 }
 public Int32 Type {
 get { return type_; }
 set { SetType(value); }
 }
 public void SetType(Int32 value) { 
 hasType = true;
 type_ = value;
 }

public const int versionFieldNumber = 2;
 private bool hasVersion;
 private string version_ = "";
 public bool HasVersion {
 get { return hasVersion; }
 }
 public string Version {
 get { return version_; }
 set { SetVersion(value); }
 }
 public void SetVersion(string value) { 
 hasVersion = true;
 version_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasType) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Type);
}
 if (HasVersion) {
size += pb::CodedOutputStream.ComputeStringSize(2, Version);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasType) {
output.WriteInt32(1, Type);
}
 
if (HasVersion) {
output.WriteString(2, Version);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCErrorMsg _inst = (SCErrorMsg) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Type = input.ReadInt32();
break;
}
   case  18: {
 _inst.Version = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasType) return false;
 return true;
 }

	}


[Serializable]
public class SCFinishTask : PacketDistributed
{

public const int templet_idFieldNumber = 1;
 private bool hasTemplet_id;
 private Int32 templet_id_ = 0;
 public bool HasTemplet_id {
 get { return hasTemplet_id; }
 }
 public Int32 Templet_id {
 get { return templet_id_; }
 set { SetTemplet_id(value); }
 }
 public void SetTemplet_id(Int32 value) { 
 hasTemplet_id = true;
 templet_id_ = value;
 }

public const int bagDataFieldNumber = 2;
 private bool hasBagData;
 private PBUserBagData bagData_ =  new PBUserBagData();
 public bool HasBagData {
 get { return hasBagData; }
 }
 public PBUserBagData BagData {
 get { return bagData_; }
 set { SetBagData(value); }
 }
 public void SetBagData(PBUserBagData value) { 
 hasBagData = true;
 bagData_ = value;
 }

public const int baseDataFieldNumber = 3;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasTemplet_id) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Templet_id);
}
{
int subsize = BagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasTemplet_id) {
output.WriteInt32(1, Templet_id);
}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BagData.SerializedSize());
BagData.WriteTo(output);

}
{
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCFinishTask _inst = (SCFinishTask) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Templet_id = input.ReadInt32();
break;
}
    case  18: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BagData = subBuilder;
break;
}
    case  26: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasTemplet_id) return false;
  if (HasBagData) {
if (!BagData.IsInitialized()) return false;
}
  if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCFriendMailDelete : PacketDistributed
{

public const int stateFieldNumber = 1;
 private bool hasState;
 private Int32 state_ = 0;
 public bool HasState {
 get { return hasState; }
 }
 public Int32 State {
 get { return state_; }
 set { SetState(value); }
 }
 public void SetState(Int32 value) { 
 hasState = true;
 state_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasState) {
size += pb::CodedOutputStream.ComputeInt32Size(1, State);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasState) {
output.WriteInt32(1, State);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCFriendMailDelete _inst = (SCFriendMailDelete) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.State = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasState) return false;
 return true;
 }

	}


[Serializable]
public class SCFriendsList : PacketDistributed
{

public const int friends_numFieldNumber = 1;
 private bool hasFriends_num;
 private Int32 friends_num_ = 0;
 public bool HasFriends_num {
 get { return hasFriends_num; }
 }
 public Int32 Friends_num {
 get { return friends_num_; }
 set { SetFriends_num(value); }
 }
 public void SetFriends_num(Int32 value) { 
 hasFriends_num = true;
 friends_num_ = value;
 }

public const int friends_maxFieldNumber = 2;
 private bool hasFriends_max;
 private Int32 friends_max_ = 0;
 public bool HasFriends_max {
 get { return hasFriends_max; }
 }
 public Int32 Friends_max {
 get { return friends_max_; }
 set { SetFriends_max(value); }
 }
 public void SetFriends_max(Int32 value) { 
 hasFriends_max = true;
 friends_max_ = value;
 }

public const int friendsListFieldNumber = 3;
 private pbc::PopsicleList<PBFriend> friendsList_ = new pbc::PopsicleList<PBFriend>();
 public scg::IList<PBFriend> friendsListList {
 get { return pbc::Lists.AsReadOnly(friendsList_); }
 }
 
 public int friendsListCount {
 get { return friendsList_.Count; }
 }
 
public PBFriend GetFriendsList(int index) {
 return friendsList_[index];
 }
 public void AddFriendsList(PBFriend value) {
 friendsList_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasFriends_num) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Friends_num);
}
 if (HasFriends_max) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Friends_max);
}
{
foreach (PBFriend element in friendsListList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasFriends_num) {
output.WriteInt32(1, Friends_num);
}
 
if (HasFriends_max) {
output.WriteInt32(2, Friends_max);
}

do{
foreach (PBFriend element in friendsListList) {
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCFriendsList _inst = (SCFriendsList) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Friends_num = input.ReadInt32();
break;
}
   case  16: {
 _inst.Friends_max = input.ReadInt32();
break;
}
    case  26: {
PBFriend subBuilder =  new PBFriend();
input.ReadMessage(subBuilder);
_inst.AddFriendsList(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasFriends_num) return false;
 if (!hasFriends_max) return false;
foreach (PBFriend element in friendsListList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCGGL : PacketDistributed
{

public const int showRewardIDFieldNumber = 1;
 private bool hasShowRewardID;
 private Int32 showRewardID_ = 0;
 public bool HasShowRewardID {
 get { return hasShowRewardID; }
 }
 public Int32 ShowRewardID {
 get { return showRewardID_; }
 set { SetShowRewardID(value); }
 }
 public void SetShowRewardID(Int32 value) { 
 hasShowRewardID = true;
 showRewardID_ = value;
 }

public const int nowTimesFieldNumber = 2;
 private bool hasNowTimes;
 private Int32 nowTimes_ = 0;
 public bool HasNowTimes {
 get { return hasNowTimes; }
 }
 public Int32 NowTimes {
 get { return nowTimes_; }
 set { SetNowTimes(value); }
 }
 public void SetNowTimes(Int32 value) { 
 hasNowTimes = true;
 nowTimes_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasShowRewardID) {
size += pb::CodedOutputStream.ComputeInt32Size(1, ShowRewardID);
}
 if (HasNowTimes) {
size += pb::CodedOutputStream.ComputeInt32Size(2, NowTimes);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasShowRewardID) {
output.WriteInt32(1, ShowRewardID);
}
 
if (HasNowTimes) {
output.WriteInt32(2, NowTimes);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCGGL _inst = (SCGGL) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.ShowRewardID = input.ReadInt32();
break;
}
   case  16: {
 _inst.NowTimes = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasShowRewardID) return false;
 if (!hasNowTimes) return false;
 return true;
 }

	}


[Serializable]
public class SCGMcmds : PacketDistributed
{

public const int resultFieldNumber = 1;
 private bool hasResult;
 private Int32 result_ = 0;
 public bool HasResult {
 get { return hasResult; }
 }
 public Int32 Result {
 get { return result_; }
 set { SetResult(value); }
 }
 public void SetResult(Int32 value) { 
 hasResult = true;
 result_ = value;
 }

public const int bagDataFieldNumber = 2;
 private bool hasBagData;
 private PBUserBagData bagData_ =  new PBUserBagData();
 public bool HasBagData {
 get { return hasBagData; }
 }
 public PBUserBagData BagData {
 get { return bagData_; }
 set { SetBagData(value); }
 }
 public void SetBagData(PBUserBagData value) { 
 hasBagData = true;
 bagData_ = value;
 }

public const int baseDataFieldNumber = 3;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int copyDataFieldNumber = 4;
 private bool hasCopyData;
 private PBUserCopyData copyData_ =  new PBUserCopyData();
 public bool HasCopyData {
 get { return hasCopyData; }
 }
 public PBUserCopyData CopyData {
 get { return copyData_; }
 set { SetCopyData(value); }
 }
 public void SetCopyData(PBUserCopyData value) { 
 hasCopyData = true;
 copyData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasResult) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Result);
}
{
int subsize = BagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = CopyData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)4) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasResult) {
output.WriteInt32(1, Result);
}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BagData.SerializedSize());
BagData.WriteTo(output);

}
{
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
{
output.WriteTag((int)4, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)CopyData.SerializedSize());
CopyData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCGMcmds _inst = (SCGMcmds) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Result = input.ReadInt32();
break;
}
    case  18: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BagData = subBuilder;
break;
}
    case  26: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
    case  34: {
PBUserCopyData subBuilder =  new PBUserCopyData();
 input.ReadMessage(subBuilder);
_inst.CopyData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasResult) return false;
  if (HasBagData) {
if (!BagData.IsInitialized()) return false;
}
  if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
  if (HasCopyData) {
if (!CopyData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCGambleRet : PacketDistributed
{

public const int templateIDFieldNumber = 1;
 private pbc::PopsicleList<Int32> templateID_ = new pbc::PopsicleList<Int32>();
 public scg::IList<Int32> templateIDList {
 get { return pbc::Lists.AsReadOnly(templateID_); }
 }
 
 public int templateIDCount {
 get { return templateID_.Count; }
 }
 
public Int32 GetTemplateID(int index) {
 return templateID_[index];
 }
 public void AddTemplateID(Int32 value) {
 templateID_.Add(value);
 }

public const int baseDataFieldNumber = 2;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int bagDataFieldNumber = 3;
 private bool hasBagData;
 private PBUserBagData bagData_ =  new PBUserBagData();
 public bool HasBagData {
 get { return hasBagData; }
 }
 public PBUserBagData BagData {
 get { return bagData_; }
 set { SetBagData(value); }
 }
 public void SetBagData(PBUserBagData value) { 
 hasBagData = true;
 bagData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int dataSize = 0;
foreach (Int32 element in templateIDList) {
dataSize += pb::CodedOutputStream.ComputeInt32SizeNoTag(element);
}
size += dataSize;
size += 1 * templateID_.Count;
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
if (templateID_.Count > 0) {
foreach (Int32 element in templateIDList) {
output.WriteInt32(1,element);
}
}

}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
{
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BagData.SerializedSize());
BagData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCGambleRet _inst = (SCGambleRet) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.AddTemplateID(input.ReadInt32());
break;
}
    case  18: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
    case  26: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BagData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
  if (HasBagData) {
if (!BagData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCGetFriendPower : PacketDistributed
{

public const int friend_guidFieldNumber = 1;
 private bool hasFriend_guid;
 private Int64 friend_guid_ = 0;
 public bool HasFriend_guid {
 get { return hasFriend_guid; }
 }
 public Int64 Friend_guid {
 get { return friend_guid_; }
 set { SetFriend_guid(value); }
 }
 public void SetFriend_guid(Int64 value) { 
 hasFriend_guid = true;
 friend_guid_ = value;
 }

public const int new_power_numFieldNumber = 3;
 private bool hasNew_power_num;
 private Int32 new_power_num_ = 0;
 public bool HasNew_power_num {
 get { return hasNew_power_num; }
 }
 public Int32 New_power_num {
 get { return new_power_num_; }
 set { SetNew_power_num(value); }
 }
 public void SetNew_power_num(Int32 value) { 
 hasNew_power_num = true;
 new_power_num_ = value;
 }

public const int receive_power_timeFieldNumber = 4;
 private bool hasReceive_power_time;
 private Int32 receive_power_time_ = 0;
 public bool HasReceive_power_time {
 get { return hasReceive_power_time; }
 }
 public Int32 Receive_power_time {
 get { return receive_power_time_; }
 set { SetReceive_power_time(value); }
 }
 public void SetReceive_power_time(Int32 value) { 
 hasReceive_power_time = true;
 receive_power_time_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasFriend_guid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Friend_guid);
}
 if (HasNew_power_num) {
size += pb::CodedOutputStream.ComputeInt32Size(3, New_power_num);
}
 if (HasReceive_power_time) {
size += pb::CodedOutputStream.ComputeInt32Size(4, Receive_power_time);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasFriend_guid) {
output.WriteInt64(1, Friend_guid);
}
 
if (HasNew_power_num) {
output.WriteInt32(3, New_power_num);
}
 
if (HasReceive_power_time) {
output.WriteInt32(4, Receive_power_time);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCGetFriendPower _inst = (SCGetFriendPower) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Friend_guid = input.ReadInt64();
break;
}
   case  24: {
 _inst.New_power_num = input.ReadInt32();
break;
}
   case  32: {
 _inst.Receive_power_time = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasFriend_guid) return false;
 if (!hasNew_power_num) return false;
 if (!hasReceive_power_time) return false;
 return true;
 }

	}


[Serializable]
public class SCGetRandomAssistanceList : PacketDistributed
{

public const int assistanceListFieldNumber = 1;
 private pbc::PopsicleList<PBFriend> assistanceList_ = new pbc::PopsicleList<PBFriend>();
 public scg::IList<PBFriend> assistanceListList {
 get { return pbc::Lists.AsReadOnly(assistanceList_); }
 }
 
 public int assistanceListCount {
 get { return assistanceList_.Count; }
 }
 
public PBFriend GetAssistanceList(int index) {
 return assistanceList_[index];
 }
 public void AddAssistanceList(PBFriend value) {
 assistanceList_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
foreach (PBFriend element in assistanceListList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 
do{
foreach (PBFriend element in assistanceListList) {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCGetRandomAssistanceList _inst = (SCGetRandomAssistanceList) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
PBFriend subBuilder =  new PBFriend();
input.ReadMessage(subBuilder);
_inst.AddAssistanceList(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
 foreach (PBFriend element in assistanceListList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCGiveFriendPower : PacketDistributed
{

public const int friend_guidFieldNumber = 1;
 private bool hasFriend_guid;
 private Int64 friend_guid_ = 0;
 public bool HasFriend_guid {
 get { return hasFriend_guid; }
 }
 public Int64 Friend_guid {
 get { return friend_guid_; }
 set { SetFriend_guid(value); }
 }
 public void SetFriend_guid(Int64 value) { 
 hasFriend_guid = true;
 friend_guid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasFriend_guid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Friend_guid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasFriend_guid) {
output.WriteInt64(1, Friend_guid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCGiveFriendPower _inst = (SCGiveFriendPower) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Friend_guid = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasFriend_guid) return false;
 return true;
 }

	}


[Serializable]
public class SCGooglePayVerifyChargeRet : PacketDistributed
{

public const int resultFieldNumber = 1;
 private bool hasResult;
 private Int32 result_ = 0;
 public bool HasResult {
 get { return hasResult; }
 }
 public Int32 Result {
 get { return result_; }
 set { SetResult(value); }
 }
 public void SetResult(Int32 value) { 
 hasResult = true;
 result_ = value;
 }

public const int goodsIdFieldNumber = 2;
 private bool hasGoodsId;
 private Int32 goodsId_ = 0;
 public bool HasGoodsId {
 get { return hasGoodsId; }
 }
 public Int32 GoodsId {
 get { return goodsId_; }
 set { SetGoodsId(value); }
 }
 public void SetGoodsId(Int32 value) { 
 hasGoodsId = true;
 goodsId_ = value;
 }

public const int playerDollarFieldNumber = 3;
 private bool hasPlayerDollar;
 private Int32 playerDollar_ = 0;
 public bool HasPlayerDollar {
 get { return hasPlayerDollar; }
 }
 public Int32 PlayerDollar {
 get { return playerDollar_; }
 set { SetPlayerDollar(value); }
 }
 public void SetPlayerDollar(Int32 value) { 
 hasPlayerDollar = true;
 playerDollar_ = value;
 }

public const int orderIdFieldNumber = 4;
 private bool hasOrderId;
 private string orderId_ = "";
 public bool HasOrderId {
 get { return hasOrderId; }
 }
 public string OrderId {
 get { return orderId_; }
 set { SetOrderId(value); }
 }
 public void SetOrderId(string value) { 
 hasOrderId = true;
 orderId_ = value;
 }

public const int goodsPriceFieldNumber = 5;
 private bool hasGoodsPrice;
 private Int32 goodsPrice_ = 0;
 public bool HasGoodsPrice {
 get { return hasGoodsPrice; }
 }
 public Int32 GoodsPrice {
 get { return goodsPrice_; }
 set { SetGoodsPrice(value); }
 }
 public void SetGoodsPrice(Int32 value) { 
 hasGoodsPrice = true;
 goodsPrice_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasResult) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Result);
}
 if (HasGoodsId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, GoodsId);
}
 if (HasPlayerDollar) {
size += pb::CodedOutputStream.ComputeInt32Size(3, PlayerDollar);
}
 if (HasOrderId) {
size += pb::CodedOutputStream.ComputeStringSize(4, OrderId);
}
 if (HasGoodsPrice) {
size += pb::CodedOutputStream.ComputeInt32Size(5, GoodsPrice);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasResult) {
output.WriteInt32(1, Result);
}
 
if (HasGoodsId) {
output.WriteInt32(2, GoodsId);
}
 
if (HasPlayerDollar) {
output.WriteInt32(3, PlayerDollar);
}
 
if (HasOrderId) {
output.WriteString(4, OrderId);
}
 
if (HasGoodsPrice) {
output.WriteInt32(5, GoodsPrice);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCGooglePayVerifyChargeRet _inst = (SCGooglePayVerifyChargeRet) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Result = input.ReadInt32();
break;
}
   case  16: {
 _inst.GoodsId = input.ReadInt32();
break;
}
   case  24: {
 _inst.PlayerDollar = input.ReadInt32();
break;
}
   case  34: {
 _inst.OrderId = input.ReadString();
break;
}
   case  40: {
 _inst.GoodsPrice = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class SCGuide : PacketDistributed
{

public const int player_guidFieldNumber = 1;
 private bool hasPlayer_guid;
 private Int32 player_guid_ = 0;
 public bool HasPlayer_guid {
 get { return hasPlayer_guid; }
 }
 public Int32 Player_guid {
 get { return player_guid_; }
 set { SetPlayer_guid(value); }
 }
 public void SetPlayer_guid(Int32 value) { 
 hasPlayer_guid = true;
 player_guid_ = value;
 }

public const int finish_stepFieldNumber = 2;
 private bool hasFinish_step;
 private Int32 finish_step_ = 0;
 public bool HasFinish_step {
 get { return hasFinish_step; }
 }
 public Int32 Finish_step {
 get { return finish_step_; }
 set { SetFinish_step(value); }
 }
 public void SetFinish_step(Int32 value) { 
 hasFinish_step = true;
 finish_step_ = value;
 }

public const int baseDataFieldNumber = 3;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int bagDataFieldNumber = 4;
 private bool hasBagData;
 private PBUserBagData bagData_ =  new PBUserBagData();
 public bool HasBagData {
 get { return hasBagData; }
 }
 public PBUserBagData BagData {
 get { return bagData_; }
 set { SetBagData(value); }
 }
 public void SetBagData(PBUserBagData value) { 
 hasBagData = true;
 bagData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasPlayer_guid) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Player_guid);
}
 if (HasFinish_step) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Finish_step);
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)4) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasPlayer_guid) {
output.WriteInt32(1, Player_guid);
}
 
if (HasFinish_step) {
output.WriteInt32(2, Finish_step);
}
{
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
{
output.WriteTag((int)4, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BagData.SerializedSize());
BagData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCGuide _inst = (SCGuide) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Player_guid = input.ReadInt32();
break;
}
   case  16: {
 _inst.Finish_step = input.ReadInt32();
break;
}
    case  26: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
    case  34: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BagData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasPlayer_guid) return false;
 if (!hasFinish_step) return false;
  if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
  if (HasBagData) {
if (!BagData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCLoginRet : PacketDistributed
{

public const int stateFieldNumber = 1;
 private bool hasState;
 private Int32 state_ = 0;
 public bool HasState {
 get { return hasState; }
 }
 public Int32 State {
 get { return state_; }
 set { SetState(value); }
 }
 public void SetState(Int32 value) { 
 hasState = true;
 state_ = value;
 }

public const int accountIdFieldNumber = 2;
 private bool hasAccountId;
 private Int64 accountId_ = 0;
 public bool HasAccountId {
 get { return hasAccountId; }
 }
 public Int64 AccountId {
 get { return accountId_; }
 set { SetAccountId(value); }
 }
 public void SetAccountId(Int64 value) { 
 hasAccountId = true;
 accountId_ = value;
 }

public const int guide_finished_stepFieldNumber = 3;
 private bool hasGuide_finished_step;
 private Int32 guide_finished_step_ = 0;
 public bool HasGuide_finished_step {
 get { return hasGuide_finished_step; }
 }
 public Int32 Guide_finished_step {
 get { return guide_finished_step_; }
 set { SetGuide_finished_step(value); }
 }
 public void SetGuide_finished_step(Int32 value) { 
 hasGuide_finished_step = true;
 guide_finished_step_ = value;
 }

public const int retCodeFieldNumber = 4;
 private bool hasRetCode;
 private Int32 retCode_ = 0;
 public bool HasRetCode {
 get { return hasRetCode; }
 }
 public Int32 RetCode {
 get { return retCode_; }
 set { SetRetCode(value); }
 }
 public void SetRetCode(Int32 value) { 
 hasRetCode = true;
 retCode_ = value;
 }

public const int typeFieldNumber = 5;
 private bool hasType;
 private Int32 type_ = 0;
 public bool HasType {
 get { return hasType; }
 }
 public Int32 Type {
 get { return type_; }
 set { SetType(value); }
 }
 public void SetType(Int32 value) { 
 hasType = true;
 type_ = value;
 }

public const int uidFieldNumber = 6;
 private bool hasUid;
 private string uid_ = "";
 public bool HasUid {
 get { return hasUid; }
 }
 public string Uid {
 get { return uid_; }
 set { SetUid(value); }
 }
 public void SetUid(string value) { 
 hasUid = true;
 uid_ = value;
 }

public const int usernameFieldNumber = 7;
 private bool hasUsername;
 private string username_ = "";
 public bool HasUsername {
 get { return hasUsername; }
 }
 public string Username {
 get { return username_; }
 set { SetUsername(value); }
 }
 public void SetUsername(string value) { 
 hasUsername = true;
 username_ = value;
 }

public const int giftisonFieldNumber = 8;
 private bool hasGiftison;
 private Int32 giftison_ = 0;
 public bool HasGiftison {
 get { return hasGiftison; }
 }
 public Int32 Giftison {
 get { return giftison_; }
 set { SetGiftison(value); }
 }
 public void SetGiftison(Int32 value) { 
 hasGiftison = true;
 giftison_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasState) {
size += pb::CodedOutputStream.ComputeInt32Size(1, State);
}
 if (HasAccountId) {
size += pb::CodedOutputStream.ComputeInt64Size(2, AccountId);
}
 if (HasGuide_finished_step) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Guide_finished_step);
}
 if (HasRetCode) {
size += pb::CodedOutputStream.ComputeInt32Size(4, RetCode);
}
 if (HasType) {
size += pb::CodedOutputStream.ComputeInt32Size(5, Type);
}
 if (HasUid) {
size += pb::CodedOutputStream.ComputeStringSize(6, Uid);
}
 if (HasUsername) {
size += pb::CodedOutputStream.ComputeStringSize(7, Username);
}
 if (HasGiftison) {
size += pb::CodedOutputStream.ComputeInt32Size(8, Giftison);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasState) {
output.WriteInt32(1, State);
}
 
if (HasAccountId) {
output.WriteInt64(2, AccountId);
}
 
if (HasGuide_finished_step) {
output.WriteInt32(3, Guide_finished_step);
}
 
if (HasRetCode) {
output.WriteInt32(4, RetCode);
}
 
if (HasType) {
output.WriteInt32(5, Type);
}
 
if (HasUid) {
output.WriteString(6, Uid);
}
 
if (HasUsername) {
output.WriteString(7, Username);
}
 
if (HasGiftison) {
output.WriteInt32(8, Giftison);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCLoginRet _inst = (SCLoginRet) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.State = input.ReadInt32();
break;
}
   case  16: {
 _inst.AccountId = input.ReadInt64();
break;
}
   case  24: {
 _inst.Guide_finished_step = input.ReadInt32();
break;
}
   case  32: {
 _inst.RetCode = input.ReadInt32();
break;
}
   case  40: {
 _inst.Type = input.ReadInt32();
break;
}
   case  50: {
 _inst.Uid = input.ReadString();
break;
}
   case  58: {
 _inst.Username = input.ReadString();
break;
}
   case  64: {
 _inst.Giftison = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class SCLoginThirdPlatformRet : PacketDistributed
{

public const int stateFieldNumber = 1;
 private bool hasState;
 private Int32 state_ = 0;
 public bool HasState {
 get { return hasState; }
 }
 public Int32 State {
 get { return state_; }
 set { SetState(value); }
 }
 public void SetState(Int32 value) { 
 hasState = true;
 state_ = value;
 }

public const int ucAccountFieldNumber = 2;
 private bool hasUcAccount;
 private string ucAccount_ = "";
 public bool HasUcAccount {
 get { return hasUcAccount; }
 }
 public string UcAccount {
 get { return ucAccount_; }
 set { SetUcAccount(value); }
 }
 public void SetUcAccount(string value) { 
 hasUcAccount = true;
 ucAccount_ = value;
 }

public const int nickNameFieldNumber = 3;
 private bool hasNickName;
 private string nickName_ = "";
 public bool HasNickName {
 get { return hasNickName; }
 }
 public string NickName {
 get { return nickName_; }
 set { SetNickName(value); }
 }
 public void SetNickName(string value) { 
 hasNickName = true;
 nickName_ = value;
 }

public const int guide_finished_stepFieldNumber = 4;
 private bool hasGuide_finished_step;
 private Int32 guide_finished_step_ = 0;
 public bool HasGuide_finished_step {
 get { return hasGuide_finished_step; }
 }
 public Int32 Guide_finished_step {
 get { return guide_finished_step_; }
 set { SetGuide_finished_step(value); }
 }
 public void SetGuide_finished_step(Int32 value) { 
 hasGuide_finished_step = true;
 guide_finished_step_ = value;
 }

public const int retCodeFieldNumber = 5;
 private bool hasRetCode;
 private Int32 retCode_ = 0;
 public bool HasRetCode {
 get { return hasRetCode; }
 }
 public Int32 RetCode {
 get { return retCode_; }
 set { SetRetCode(value); }
 }
 public void SetRetCode(Int32 value) { 
 hasRetCode = true;
 retCode_ = value;
 }

public const int gameIdFieldNumber = 6;
 private bool hasGameId;
 private Int32 gameId_ = 0;
 public bool HasGameId {
 get { return hasGameId; }
 }
 public Int32 GameId {
 get { return gameId_; }
 set { SetGameId(value); }
 }
 public void SetGameId(Int32 value) { 
 hasGameId = true;
 gameId_ = value;
 }

public const int serverIdFieldNumber = 7;
 private bool hasServerId;
 private Int32 serverId_ = 0;
 public bool HasServerId {
 get { return hasServerId; }
 }
 public Int32 ServerId {
 get { return serverId_; }
 set { SetServerId(value); }
 }
 public void SetServerId(Int32 value) { 
 hasServerId = true;
 serverId_ = value;
 }

public const int appkeyFieldNumber = 8;
 private bool hasAppkey;
 private string appkey_ = "";
 public bool HasAppkey {
 get { return hasAppkey; }
 }
 public string Appkey {
 get { return appkey_; }
 set { SetAppkey(value); }
 }
 public void SetAppkey(string value) { 
 hasAppkey = true;
 appkey_ = value;
 }

public const int appsecretFieldNumber = 9;
 private bool hasAppsecret;
 private string appsecret_ = "";
 public bool HasAppsecret {
 get { return hasAppsecret; }
 }
 public string Appsecret {
 get { return appsecret_; }
 set { SetAppsecret(value); }
 }
 public void SetAppsecret(string value) { 
 hasAppsecret = true;
 appsecret_ = value;
 }

public const int cpIdFieldNumber = 10;
 private bool hasCpId;
 private Int32 cpId_ = 0;
 public bool HasCpId {
 get { return hasCpId; }
 }
 public Int32 CpId {
 get { return cpId_; }
 set { SetCpId(value); }
 }
 public void SetCpId(Int32 value) { 
 hasCpId = true;
 cpId_ = value;
 }

public const int apiKeyFieldNumber = 11;
 private bool hasApiKey;
 private string apiKey_ = "";
 public bool HasApiKey {
 get { return hasApiKey; }
 }
 public string ApiKey {
 get { return apiKey_; }
 set { SetApiKey(value); }
 }
 public void SetApiKey(string value) { 
 hasApiKey = true;
 apiKey_ = value;
 }

public const int ucchannelIdFieldNumber = 12;
 private bool hasUcchannelId;
 private Int32 ucchannelId_ = 0;
 public bool HasUcchannelId {
 get { return hasUcchannelId; }
 }
 public Int32 UcchannelId {
 get { return ucchannelId_; }
 set { SetUcchannelId(value); }
 }
 public void SetUcchannelId(Int32 value) { 
 hasUcchannelId = true;
 ucchannelId_ = value;
 }

public const int accountIdFieldNumber = 13;
 private bool hasAccountId;
 private Int64 accountId_ = 0;
 public bool HasAccountId {
 get { return hasAccountId; }
 }
 public Int64 AccountId {
 get { return accountId_; }
 set { SetAccountId(value); }
 }
 public void SetAccountId(Int64 value) { 
 hasAccountId = true;
 accountId_ = value;
 }

public const int giftisonFieldNumber = 14;
 private bool hasGiftison;
 private Int32 giftison_ = 0;
 public bool HasGiftison {
 get { return hasGiftison; }
 }
 public Int32 Giftison {
 get { return giftison_; }
 set { SetGiftison(value); }
 }
 public void SetGiftison(Int32 value) { 
 hasGiftison = true;
 giftison_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasState) {
size += pb::CodedOutputStream.ComputeInt32Size(1, State);
}
 if (HasUcAccount) {
size += pb::CodedOutputStream.ComputeStringSize(2, UcAccount);
}
 if (HasNickName) {
size += pb::CodedOutputStream.ComputeStringSize(3, NickName);
}
 if (HasGuide_finished_step) {
size += pb::CodedOutputStream.ComputeInt32Size(4, Guide_finished_step);
}
 if (HasRetCode) {
size += pb::CodedOutputStream.ComputeInt32Size(5, RetCode);
}
 if (HasGameId) {
size += pb::CodedOutputStream.ComputeInt32Size(6, GameId);
}
 if (HasServerId) {
size += pb::CodedOutputStream.ComputeInt32Size(7, ServerId);
}
 if (HasAppkey) {
size += pb::CodedOutputStream.ComputeStringSize(8, Appkey);
}
 if (HasAppsecret) {
size += pb::CodedOutputStream.ComputeStringSize(9, Appsecret);
}
 if (HasCpId) {
size += pb::CodedOutputStream.ComputeInt32Size(10, CpId);
}
 if (HasApiKey) {
size += pb::CodedOutputStream.ComputeStringSize(11, ApiKey);
}
 if (HasUcchannelId) {
size += pb::CodedOutputStream.ComputeInt32Size(12, UcchannelId);
}
 if (HasAccountId) {
size += pb::CodedOutputStream.ComputeInt64Size(13, AccountId);
}
 if (HasGiftison) {
size += pb::CodedOutputStream.ComputeInt32Size(14, Giftison);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasState) {
output.WriteInt32(1, State);
}
 
if (HasUcAccount) {
output.WriteString(2, UcAccount);
}
 
if (HasNickName) {
output.WriteString(3, NickName);
}
 
if (HasGuide_finished_step) {
output.WriteInt32(4, Guide_finished_step);
}
 
if (HasRetCode) {
output.WriteInt32(5, RetCode);
}
 
if (HasGameId) {
output.WriteInt32(6, GameId);
}
 
if (HasServerId) {
output.WriteInt32(7, ServerId);
}
 
if (HasAppkey) {
output.WriteString(8, Appkey);
}
 
if (HasAppsecret) {
output.WriteString(9, Appsecret);
}
 
if (HasCpId) {
output.WriteInt32(10, CpId);
}
 
if (HasApiKey) {
output.WriteString(11, ApiKey);
}
 
if (HasUcchannelId) {
output.WriteInt32(12, UcchannelId);
}
 
if (HasAccountId) {
output.WriteInt64(13, AccountId);
}
 
if (HasGiftison) {
output.WriteInt32(14, Giftison);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCLoginThirdPlatformRet _inst = (SCLoginThirdPlatformRet) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.State = input.ReadInt32();
break;
}
   case  18: {
 _inst.UcAccount = input.ReadString();
break;
}
   case  26: {
 _inst.NickName = input.ReadString();
break;
}
   case  32: {
 _inst.Guide_finished_step = input.ReadInt32();
break;
}
   case  40: {
 _inst.RetCode = input.ReadInt32();
break;
}
   case  48: {
 _inst.GameId = input.ReadInt32();
break;
}
   case  56: {
 _inst.ServerId = input.ReadInt32();
break;
}
   case  66: {
 _inst.Appkey = input.ReadString();
break;
}
   case  74: {
 _inst.Appsecret = input.ReadString();
break;
}
   case  80: {
 _inst.CpId = input.ReadInt32();
break;
}
   case  90: {
 _inst.ApiKey = input.ReadString();
break;
}
   case  96: {
 _inst.UcchannelId = input.ReadInt32();
break;
}
   case  104: {
 _inst.AccountId = input.ReadInt64();
break;
}
   case  112: {
 _inst.Giftison = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasUcAccount) return false;
 if (!hasNickName) return false;
 return true;
 }

	}


[Serializable]
public class SCMailDelete : PacketDistributed
{

public const int stateFieldNumber = 1;
 private bool hasState;
 private Int32 state_ = 0;
 public bool HasState {
 get { return hasState; }
 }
 public Int32 State {
 get { return state_; }
 set { SetState(value); }
 }
 public void SetState(Int32 value) { 
 hasState = true;
 state_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasState) {
size += pb::CodedOutputStream.ComputeInt32Size(1, State);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasState) {
output.WriteInt32(1, State);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCMailDelete _inst = (SCMailDelete) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.State = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasState) return false;
 return true;
 }

	}


[Serializable]
public class SCMailFriend : PacketDistributed
{

public const int stateFieldNumber = 1;
 private bool hasState;
 private Int32 state_ = 0;
 public bool HasState {
 get { return hasState; }
 }
 public Int32 State {
 get { return state_; }
 set { SetState(value); }
 }
 public void SetState(Int32 value) { 
 hasState = true;
 state_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasState) {
size += pb::CodedOutputStream.ComputeInt32Size(1, State);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasState) {
output.WriteInt32(1, State);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCMailFriend _inst = (SCMailFriend) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.State = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasState) return false;
 return true;
 }

	}


[Serializable]
public class SCMailList : PacketDistributed
{

public const int pbMailFieldNumber = 1;
 private pbc::PopsicleList<PBMail> pbMail_ = new pbc::PopsicleList<PBMail>();
 public scg::IList<PBMail> pbMailList {
 get { return pbc::Lists.AsReadOnly(pbMail_); }
 }
 
 public int pbMailCount {
 get { return pbMail_.Count; }
 }
 
public PBMail GetPbMail(int index) {
 return pbMail_[index];
 }
 public void AddPbMail(PBMail value) {
 pbMail_.Add(value);
 }

public const int isfullFieldNumber = 2;
 private bool hasIsfull;
 private Int32 isfull_ = 0;
 public bool HasIsfull {
 get { return hasIsfull; }
 }
 public Int32 Isfull {
 get { return isfull_; }
 set { SetIsfull(value); }
 }
 public void SetIsfull(Int32 value) { 
 hasIsfull = true;
 isfull_ = value;
 }

public const int friendIsfullFieldNumber = 3;
 private bool hasFriendIsfull;
 private Int32 friendIsfull_ = 0;
 public bool HasFriendIsfull {
 get { return hasFriendIsfull; }
 }
 public Int32 FriendIsfull {
 get { return friendIsfull_; }
 set { SetFriendIsfull(value); }
 }
 public void SetFriendIsfull(Int32 value) { 
 hasFriendIsfull = true;
 friendIsfull_ = value;
 }

public const int noReadMailFieldNumber = 4;
 private bool hasNoReadMail;
 private Int32 noReadMail_ = 0;
 public bool HasNoReadMail {
 get { return hasNoReadMail; }
 }
 public Int32 NoReadMail {
 get { return noReadMail_; }
 set { SetNoReadMail(value); }
 }
 public void SetNoReadMail(Int32 value) { 
 hasNoReadMail = true;
 noReadMail_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
foreach (PBMail element in pbMailList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 if (HasIsfull) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Isfull);
}
 if (HasFriendIsfull) {
size += pb::CodedOutputStream.ComputeInt32Size(3, FriendIsfull);
}
 if (HasNoReadMail) {
size += pb::CodedOutputStream.ComputeInt32Size(4, NoReadMail);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 
do{
foreach (PBMail element in pbMailList) {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 
if (HasIsfull) {
output.WriteInt32(2, Isfull);
}
 
if (HasFriendIsfull) {
output.WriteInt32(3, FriendIsfull);
}
 
if (HasNoReadMail) {
output.WriteInt32(4, NoReadMail);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCMailList _inst = (SCMailList) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
PBMail subBuilder =  new PBMail();
input.ReadMessage(subBuilder);
_inst.AddPbMail(subBuilder);
break;
}
   case  16: {
 _inst.Isfull = input.ReadInt32();
break;
}
   case  24: {
 _inst.FriendIsfull = input.ReadInt32();
break;
}
   case  32: {
 _inst.NoReadMail = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
 foreach (PBMail element in pbMailList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCMailRead : PacketDistributed
{

public const int stateFieldNumber = 1;
 private bool hasState;
 private Int32 state_ = 0;
 public bool HasState {
 get { return hasState; }
 }
 public Int32 State {
 get { return state_; }
 set { SetState(value); }
 }
 public void SetState(Int32 value) { 
 hasState = true;
 state_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasState) {
size += pb::CodedOutputStream.ComputeInt32Size(1, State);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasState) {
output.WriteInt32(1, State);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCMailRead _inst = (SCMailRead) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.State = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasState) return false;
 return true;
 }

	}


[Serializable]
public class SCMailSend : PacketDistributed
{

public const int stateFieldNumber = 1;
 private bool hasState;
 private Int32 state_ = 0;
 public bool HasState {
 get { return hasState; }
 }
 public Int32 State {
 get { return state_; }
 set { SetState(value); }
 }
 public void SetState(Int32 value) { 
 hasState = true;
 state_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasState) {
size += pb::CodedOutputStream.ComputeInt32Size(1, State);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasState) {
output.WriteInt32(1, State);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCMailSend _inst = (SCMailSend) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.State = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasState) return false;
 return true;
 }

	}


[Serializable]
public class SCMailSystem : PacketDistributed
{

public const int item_typeFieldNumber = 1;
 private bool hasItem_type;
 private Int32 item_type_ = 0;
 public bool HasItem_type {
 get { return hasItem_type; }
 }
 public Int32 Item_type {
 get { return item_type_; }
 set { SetItem_type(value); }
 }
 public void SetItem_type(Int32 value) { 
 hasItem_type = true;
 item_type_ = value;
 }

public const int baseDataFieldNumber = 2;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int bagDataFieldNumber = 3;
 private bool hasBagData;
 private PBUserBagData bagData_ =  new PBUserBagData();
 public bool HasBagData {
 get { return hasBagData; }
 }
 public PBUserBagData BagData {
 get { return bagData_; }
 set { SetBagData(value); }
 }
 public void SetBagData(PBUserBagData value) { 
 hasBagData = true;
 bagData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasItem_type) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Item_type);
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasItem_type) {
output.WriteInt32(1, Item_type);
}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
{
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BagData.SerializedSize());
BagData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCMailSystem _inst = (SCMailSystem) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Item_type = input.ReadInt32();
break;
}
    case  18: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
    case  26: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BagData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasItem_type) return false;
  if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
  if (HasBagData) {
if (!BagData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCMonthCardGetDollar : PacketDistributed
{

public const int resultFieldNumber = 1;
 private bool hasResult;
 private Int32 result_ = 0;
 public bool HasResult {
 get { return hasResult; }
 }
 public Int32 Result {
 get { return result_; }
 set { SetResult(value); }
 }
 public void SetResult(Int32 value) { 
 hasResult = true;
 result_ = value;
 }

public const int baseDataFieldNumber = 2;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int bagDataFieldNumber = 3;
 private bool hasBagData;
 private PBUserBagData bagData_ =  new PBUserBagData();
 public bool HasBagData {
 get { return hasBagData; }
 }
 public PBUserBagData BagData {
 get { return bagData_; }
 set { SetBagData(value); }
 }
 public void SetBagData(PBUserBagData value) { 
 hasBagData = true;
 bagData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasResult) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Result);
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasResult) {
output.WriteInt32(1, Result);
}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
{
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BagData.SerializedSize());
BagData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCMonthCardGetDollar _inst = (SCMonthCardGetDollar) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Result = input.ReadInt32();
break;
}
    case  18: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
    case  26: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BagData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
  if (HasBagData) {
if (!BagData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCMonthCardInfo : PacketDistributed
{

public const int yuekaInfoFieldNumber = 1;
 private bool hasYuekaInfo;
 private MonthCardInfo yuekaInfo_ =  new MonthCardInfo();
 public bool HasYuekaInfo {
 get { return hasYuekaInfo; }
 }
 public MonthCardInfo YuekaInfo {
 get { return yuekaInfo_; }
 set { SetYuekaInfo(value); }
 }
 public void SetYuekaInfo(MonthCardInfo value) { 
 hasYuekaInfo = true;
 yuekaInfo_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int subsize = YuekaInfo.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)YuekaInfo.SerializedSize());
YuekaInfo.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCMonthCardInfo _inst = (SCMonthCardInfo) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
MonthCardInfo subBuilder =  new MonthCardInfo();
 input.ReadMessage(subBuilder);
_inst.YuekaInfo = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasYuekaInfo) {
if (!YuekaInfo.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCPPProductList : PacketDistributed
{

public const int productFieldNumber = 1;
 private pbc::PopsicleList<ProductInfo> product_ = new pbc::PopsicleList<ProductInfo>();
 public scg::IList<ProductInfo> productList {
 get { return pbc::Lists.AsReadOnly(product_); }
 }
 
 public int productCount {
 get { return product_.Count; }
 }
 
public ProductInfo GetProduct(int index) {
 return product_[index];
 }
 public void AddProduct(ProductInfo value) {
 product_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
foreach (ProductInfo element in productList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 
do{
foreach (ProductInfo element in productList) {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCPPProductList _inst = (SCPPProductList) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
ProductInfo subBuilder =  new ProductInfo();
input.ReadMessage(subBuilder);
_inst.AddProduct(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
 foreach (ProductInfo element in productList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCPPVerifyCharge : PacketDistributed
{

public const int resultFieldNumber = 1;
 private bool hasResult;
 private Int32 result_ = 0;
 public bool HasResult {
 get { return hasResult; }
 }
 public Int32 Result {
 get { return result_; }
 set { SetResult(value); }
 }
 public void SetResult(Int32 value) { 
 hasResult = true;
 result_ = value;
 }

public const int goodsIdFieldNumber = 2;
 private bool hasGoodsId;
 private Int32 goodsId_ = 0;
 public bool HasGoodsId {
 get { return hasGoodsId; }
 }
 public Int32 GoodsId {
 get { return goodsId_; }
 set { SetGoodsId(value); }
 }
 public void SetGoodsId(Int32 value) { 
 hasGoodsId = true;
 goodsId_ = value;
 }

public const int playerDollarFieldNumber = 3;
 private bool hasPlayerDollar;
 private Int32 playerDollar_ = 0;
 public bool HasPlayerDollar {
 get { return hasPlayerDollar; }
 }
 public Int32 PlayerDollar {
 get { return playerDollar_; }
 set { SetPlayerDollar(value); }
 }
 public void SetPlayerDollar(Int32 value) { 
 hasPlayerDollar = true;
 playerDollar_ = value;
 }

public const int orderIdFieldNumber = 4;
 private bool hasOrderId;
 private string orderId_ = "";
 public bool HasOrderId {
 get { return hasOrderId; }
 }
 public string OrderId {
 get { return orderId_; }
 set { SetOrderId(value); }
 }
 public void SetOrderId(string value) { 
 hasOrderId = true;
 orderId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasResult) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Result);
}
 if (HasGoodsId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, GoodsId);
}
 if (HasPlayerDollar) {
size += pb::CodedOutputStream.ComputeInt32Size(3, PlayerDollar);
}
 if (HasOrderId) {
size += pb::CodedOutputStream.ComputeStringSize(4, OrderId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasResult) {
output.WriteInt32(1, Result);
}
 
if (HasGoodsId) {
output.WriteInt32(2, GoodsId);
}
 
if (HasPlayerDollar) {
output.WriteInt32(3, PlayerDollar);
}
 
if (HasOrderId) {
output.WriteString(4, OrderId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCPPVerifyCharge _inst = (SCPPVerifyCharge) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Result = input.ReadInt32();
break;
}
   case  16: {
 _inst.GoodsId = input.ReadInt32();
break;
}
   case  24: {
 _inst.PlayerDollar = input.ReadInt32();
break;
}
   case  34: {
 _inst.OrderId = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasResult) return false;
 if (!hasGoodsId) return false;
 if (!hasPlayerDollar) return false;
 if (!hasOrderId) return false;
 return true;
 }

	}


[Serializable]
public class SCPVPBattleData : PacketDistributed
{

public const int battleFieldNumber = 1;
 private bool hasBattle;
 private DataBattle battle_ =  new DataBattle();
 public bool HasBattle {
 get { return hasBattle; }
 }
 public DataBattle Battle {
 get { return battle_; }
 set { SetBattle(value); }
 }
 public void SetBattle(DataBattle value) { 
 hasBattle = true;
 battle_ = value;
 }

public const int baseDataFieldNumber = 2;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int bagDataFieldNumber = 3;
 private bool hasBagData;
 private PBUserBagData bagData_ =  new PBUserBagData();
 public bool HasBagData {
 get { return hasBagData; }
 }
 public PBUserBagData BagData {
 get { return bagData_; }
 set { SetBagData(value); }
 }
 public void SetBagData(PBUserBagData value) { 
 hasBagData = true;
 bagData_ = value;
 }

public const int copyDataFieldNumber = 4;
 private bool hasCopyData;
 private PBUserCopyData copyData_ =  new PBUserCopyData();
 public bool HasCopyData {
 get { return hasCopyData; }
 }
 public PBUserCopyData CopyData {
 get { return copyData_; }
 set { SetCopyData(value); }
 }
 public void SetCopyData(PBUserCopyData value) { 
 hasCopyData = true;
 copyData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int subsize = Battle.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = CopyData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)4) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)Battle.SerializedSize());
Battle.WriteTo(output);

}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
{
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BagData.SerializedSize());
BagData.WriteTo(output);

}
{
output.WriteTag((int)4, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)CopyData.SerializedSize());
CopyData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCPVPBattleData _inst = (SCPVPBattleData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
DataBattle subBuilder =  new DataBattle();
 input.ReadMessage(subBuilder);
_inst.Battle = subBuilder;
break;
}
    case  18: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
    case  26: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BagData = subBuilder;
break;
}
    case  34: {
PBUserCopyData subBuilder =  new PBUserCopyData();
 input.ReadMessage(subBuilder);
_inst.CopyData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasBattle) {
if (!Battle.IsInitialized()) return false;
}
  if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
  if (HasBagData) {
if (!BagData.IsInitialized()) return false;
}
  if (HasCopyData) {
if (!CopyData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCPVPShopRet : PacketDistributed
{

public const int resultFieldNumber = 1;
 private bool hasResult;
 private Int32 result_ = 0;
 public bool HasResult {
 get { return hasResult; }
 }
 public Int32 Result {
 get { return result_; }
 set { SetResult(value); }
 }
 public void SetResult(Int32 value) { 
 hasResult = true;
 result_ = value;
 }

public const int baseBagDataFieldNumber = 2;
 private bool hasBaseBagData;
 private PBUserBagData baseBagData_ =  new PBUserBagData();
 public bool HasBaseBagData {
 get { return hasBaseBagData; }
 }
 public PBUserBagData BaseBagData {
 get { return baseBagData_; }
 set { SetBaseBagData(value); }
 }
 public void SetBaseBagData(PBUserBagData value) { 
 hasBaseBagData = true;
 baseBagData_ = value;
 }

public const int nScoreFieldNumber = 3;
 private bool hasNScore;
 private Int32 nScore_ = 0;
 public bool HasNScore {
 get { return hasNScore; }
 }
 public Int32 NScore {
 get { return nScore_; }
 set { SetNScore(value); }
 }
 public void SetNScore(Int32 value) { 
 hasNScore = true;
 nScore_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasResult) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Result);
}
{
int subsize = BaseBagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 if (HasNScore) {
size += pb::CodedOutputStream.ComputeInt32Size(3, NScore);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasResult) {
output.WriteInt32(1, Result);
}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseBagData.SerializedSize());
BaseBagData.WriteTo(output);

}
 
if (HasNScore) {
output.WriteInt32(3, NScore);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCPVPShopRet _inst = (SCPVPShopRet) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Result = input.ReadInt32();
break;
}
    case  18: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BaseBagData = subBuilder;
break;
}
   case  24: {
 _inst.NScore = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasResult) return false;
  if (HasBaseBagData) {
if (!BaseBagData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCPaiTaBattleData : PacketDistributed
{

public const int battleFieldNumber = 1;
 private bool hasBattle;
 private DataBattle battle_ =  new DataBattle();
 public bool HasBattle {
 get { return hasBattle; }
 }
 public DataBattle Battle {
 get { return battle_; }
 set { SetBattle(value); }
 }
 public void SetBattle(DataBattle value) { 
 hasBattle = true;
 battle_ = value;
 }

public const int flagFieldNumber = 2;
 private bool hasFlag;
 private Int32 flag_ = 0;
 public bool HasFlag {
 get { return hasFlag; }
 }
 public Int32 Flag {
 get { return flag_; }
 set { SetFlag(value); }
 }
 public void SetFlag(Int32 value) { 
 hasFlag = true;
 flag_ = value;
 }

public const int moneynumFieldNumber = 3;
 private bool hasMoneynum;
 private Int32 moneynum_ = 0;
 public bool HasMoneynum {
 get { return hasMoneynum; }
 }
 public Int32 Moneynum {
 get { return moneynum_; }
 set { SetMoneynum(value); }
 }
 public void SetMoneynum(Int32 value) { 
 hasMoneynum = true;
 moneynum_ = value;
 }

public const int rubynumFieldNumber = 4;
 private bool hasRubynum;
 private Int32 rubynum_ = 0;
 public bool HasRubynum {
 get { return hasRubynum; }
 }
 public Int32 Rubynum {
 get { return rubynum_; }
 set { SetRubynum(value); }
 }
 public void SetRubynum(Int32 value) { 
 hasRubynum = true;
 rubynum_ = value;
 }

public const int numFieldNumber = 5;
 private bool hasNum;
 private Int32 num_ = 0;
 public bool HasNum {
 get { return hasNum; }
 }
 public Int32 Num {
 get { return num_; }
 set { SetNum(value); }
 }
 public void SetNum(Int32 value) { 
 hasNum = true;
 num_ = value;
 }

public const int baseDataFieldNumber = 6;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int bagDataFieldNumber = 7;
 private bool hasBagData;
 private PBUserBagData bagData_ =  new PBUserBagData();
 public bool HasBagData {
 get { return hasBagData; }
 }
 public PBUserBagData BagData {
 get { return bagData_; }
 set { SetBagData(value); }
 }
 public void SetBagData(PBUserBagData value) { 
 hasBagData = true;
 bagData_ = value;
 }

public const int copyDataFieldNumber = 8;
 private bool hasCopyData;
 private PBUserCopyData copyData_ =  new PBUserCopyData();
 public bool HasCopyData {
 get { return hasCopyData; }
 }
 public PBUserCopyData CopyData {
 get { return copyData_; }
 set { SetCopyData(value); }
 }
 public void SetCopyData(PBUserCopyData value) { 
 hasCopyData = true;
 copyData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int subsize = Battle.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 if (HasFlag) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Flag);
}
 if (HasMoneynum) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Moneynum);
}
 if (HasRubynum) {
size += pb::CodedOutputStream.ComputeInt32Size(4, Rubynum);
}
 if (HasNum) {
size += pb::CodedOutputStream.ComputeInt32Size(5, Num);
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)6) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)7) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = CopyData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)8) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)Battle.SerializedSize());
Battle.WriteTo(output);

}
 
if (HasFlag) {
output.WriteInt32(2, Flag);
}
 
if (HasMoneynum) {
output.WriteInt32(3, Moneynum);
}
 
if (HasRubynum) {
output.WriteInt32(4, Rubynum);
}
 
if (HasNum) {
output.WriteInt32(5, Num);
}
{
output.WriteTag((int)6, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
{
output.WriteTag((int)7, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BagData.SerializedSize());
BagData.WriteTo(output);

}
{
output.WriteTag((int)8, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)CopyData.SerializedSize());
CopyData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCPaiTaBattleData _inst = (SCPaiTaBattleData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
DataBattle subBuilder =  new DataBattle();
 input.ReadMessage(subBuilder);
_inst.Battle = subBuilder;
break;
}
   case  16: {
 _inst.Flag = input.ReadInt32();
break;
}
   case  24: {
 _inst.Moneynum = input.ReadInt32();
break;
}
   case  32: {
 _inst.Rubynum = input.ReadInt32();
break;
}
   case  40: {
 _inst.Num = input.ReadInt32();
break;
}
    case  50: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
    case  58: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BagData = subBuilder;
break;
}
    case  66: {
PBUserCopyData subBuilder =  new PBUserCopyData();
 input.ReadMessage(subBuilder);
_inst.CopyData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasBattle) {
if (!Battle.IsInitialized()) return false;
}
 if (!hasFlag) return false;
 if (!hasMoneynum) return false;
 if (!hasRubynum) return false;
 if (!hasNum) return false;
  if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
  if (HasBagData) {
if (!BagData.IsInitialized()) return false;
}
  if (HasCopyData) {
if (!CopyData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCProductList : PacketDistributed
{

public const int isFirstFieldNumber = 1;
 private bool hasIsFirst;
 private Int32 isFirst_ = 0;
 public bool HasIsFirst {
 get { return hasIsFirst; }
 }
 public Int32 IsFirst {
 get { return isFirst_; }
 set { SetIsFirst(value); }
 }
 public void SetIsFirst(Int32 value) { 
 hasIsFirst = true;
 isFirst_ = value;
 }

public const int productFieldNumber = 2;
 private pbc::PopsicleList<ProductInfo> product_ = new pbc::PopsicleList<ProductInfo>();
 public scg::IList<ProductInfo> productList {
 get { return pbc::Lists.AsReadOnly(product_); }
 }
 
 public int productCount {
 get { return product_.Count; }
 }
 
public ProductInfo GetProduct(int index) {
 return product_[index];
 }
 public void AddProduct(ProductInfo value) {
 product_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasIsFirst) {
size += pb::CodedOutputStream.ComputeInt32Size(1, IsFirst);
}
{
foreach (ProductInfo element in productList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasIsFirst) {
output.WriteInt32(1, IsFirst);
}

do{
foreach (ProductInfo element in productList) {
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCProductList _inst = (SCProductList) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.IsFirst = input.ReadInt32();
break;
}
    case  18: {
ProductInfo subBuilder =  new ProductInfo();
input.ReadMessage(subBuilder);
_inst.AddProduct(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasIsFirst) return false;
foreach (ProductInfo element in productList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCQxzbBattle : PacketDistributed
{

public const int battleFieldNumber = 1;
 private bool hasBattle;
 private DataBattle battle_ =  new DataBattle();
 public bool HasBattle {
 get { return hasBattle; }
 }
 public DataBattle Battle {
 get { return battle_; }
 set { SetBattle(value); }
 }
 public void SetBattle(DataBattle value) { 
 hasBattle = true;
 battle_ = value;
 }

public const int baseDataFieldNumber = 2;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int bagDataFieldNumber = 3;
 private bool hasBagData;
 private PBUserBagData bagData_ =  new PBUserBagData();
 public bool HasBagData {
 get { return hasBagData; }
 }
 public PBUserBagData BagData {
 get { return bagData_; }
 set { SetBagData(value); }
 }
 public void SetBagData(PBUserBagData value) { 
 hasBagData = true;
 bagData_ = value;
 }

public const int copyDataFieldNumber = 4;
 private bool hasCopyData;
 private PBUserCopyData copyData_ =  new PBUserCopyData();
 public bool HasCopyData {
 get { return hasCopyData; }
 }
 public PBUserCopyData CopyData {
 get { return copyData_; }
 set { SetCopyData(value); }
 }
 public void SetCopyData(PBUserCopyData value) { 
 hasCopyData = true;
 copyData_ = value;
 }

public const int moneyFieldNumber = 5;
 private bool hasMoney;
 private Int32 money_ = 0;
 public bool HasMoney {
 get { return hasMoney; }
 }
 public Int32 Money {
 get { return money_; }
 set { SetMoney(value); }
 }
 public void SetMoney(Int32 value) { 
 hasMoney = true;
 money_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int subsize = Battle.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = CopyData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)4) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 if (HasMoney) {
size += pb::CodedOutputStream.ComputeInt32Size(5, Money);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)Battle.SerializedSize());
Battle.WriteTo(output);

}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
{
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BagData.SerializedSize());
BagData.WriteTo(output);

}
{
output.WriteTag((int)4, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)CopyData.SerializedSize());
CopyData.WriteTo(output);

}
 
if (HasMoney) {
output.WriteInt32(5, Money);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCQxzbBattle _inst = (SCQxzbBattle) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
DataBattle subBuilder =  new DataBattle();
 input.ReadMessage(subBuilder);
_inst.Battle = subBuilder;
break;
}
    case  18: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
    case  26: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BagData = subBuilder;
break;
}
    case  34: {
PBUserCopyData subBuilder =  new PBUserCopyData();
 input.ReadMessage(subBuilder);
_inst.CopyData = subBuilder;
break;
}
   case  40: {
 _inst.Money = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasBattle) {
if (!Battle.IsInitialized()) return false;
}
  if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
  if (HasBagData) {
if (!BagData.IsInitialized()) return false;
}
  if (HasCopyData) {
if (!CopyData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCQxzbGetReward : PacketDistributed
{

public const int resultFieldNumber = 1;
 private bool hasResult;
 private Int32 result_ = 0;
 public bool HasResult {
 get { return hasResult; }
 }
 public Int32 Result {
 get { return result_; }
 set { SetResult(value); }
 }
 public void SetResult(Int32 value) { 
 hasResult = true;
 result_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasResult) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Result);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasResult) {
output.WriteInt32(1, Result);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCQxzbGetReward _inst = (SCQxzbGetReward) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Result = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasResult) return false;
 return true;
 }

	}


[Serializable]
public class SCQxzbPVPClearCD : PacketDistributed
{

public const int resultFieldNumber = 1;
 private bool hasResult;
 private Int32 result_ = 0;
 public bool HasResult {
 get { return hasResult; }
 }
 public Int32 Result {
 get { return result_; }
 set { SetResult(value); }
 }
 public void SetResult(Int32 value) { 
 hasResult = true;
 result_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasResult) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Result);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasResult) {
output.WriteInt32(1, Result);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCQxzbPVPClearCD _inst = (SCQxzbPVPClearCD) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Result = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasResult) return false;
 return true;
 }

	}


[Serializable]
public class SCQxzbPVPDataAsk : PacketDistributed
{

public const int pvpStar3FieldNumber = 1;
 private pbc::PopsicleList<PVPUserBaseData> pvpStar3_ = new pbc::PopsicleList<PVPUserBaseData>();
 public scg::IList<PVPUserBaseData> pvpStar3List {
 get { return pbc::Lists.AsReadOnly(pvpStar3_); }
 }
 
 public int pvpStar3Count {
 get { return pvpStar3_.Count; }
 }
 
public PVPUserBaseData GetPvpStar3(int index) {
 return pvpStar3_[index];
 }
 public void AddPvpStar3(PVPUserBaseData value) {
 pvpStar3_.Add(value);
 }

public const int pvpStar4FieldNumber = 2;
 private pbc::PopsicleList<PVPUserBaseData> pvpStar4_ = new pbc::PopsicleList<PVPUserBaseData>();
 public scg::IList<PVPUserBaseData> pvpStar4List {
 get { return pbc::Lists.AsReadOnly(pvpStar4_); }
 }
 
 public int pvpStar4Count {
 get { return pvpStar4_.Count; }
 }
 
public PVPUserBaseData GetPvpStar4(int index) {
 return pvpStar4_[index];
 }
 public void AddPvpStar4(PVPUserBaseData value) {
 pvpStar4_.Add(value);
 }

public const int pvpStar5FieldNumber = 3;
 private pbc::PopsicleList<PVPUserBaseData> pvpStar5_ = new pbc::PopsicleList<PVPUserBaseData>();
 public scg::IList<PVPUserBaseData> pvpStar5List {
 get { return pbc::Lists.AsReadOnly(pvpStar5_); }
 }
 
 public int pvpStar5Count {
 get { return pvpStar5_.Count; }
 }
 
public PVPUserBaseData GetPvpStar5(int index) {
 return pvpStar5_[index];
 }
 public void AddPvpStar5(PVPUserBaseData value) {
 pvpStar5_.Add(value);
 }

public const int pvpStar6FieldNumber = 4;
 private pbc::PopsicleList<PVPUserBaseData> pvpStar6_ = new pbc::PopsicleList<PVPUserBaseData>();
 public scg::IList<PVPUserBaseData> pvpStar6List {
 get { return pbc::Lists.AsReadOnly(pvpStar6_); }
 }
 
 public int pvpStar6Count {
 get { return pvpStar6_.Count; }
 }
 
public PVPUserBaseData GetPvpStar6(int index) {
 return pvpStar6_[index];
 }
 public void AddPvpStar6(PVPUserBaseData value) {
 pvpStar6_.Add(value);
 }

public const int pvpStar7FieldNumber = 5;
 private pbc::PopsicleList<PVPUserBaseData> pvpStar7_ = new pbc::PopsicleList<PVPUserBaseData>();
 public scg::IList<PVPUserBaseData> pvpStar7List {
 get { return pbc::Lists.AsReadOnly(pvpStar7_); }
 }
 
 public int pvpStar7Count {
 get { return pvpStar7_.Count; }
 }
 
public PVPUserBaseData GetPvpStar7(int index) {
 return pvpStar7_[index];
 }
 public void AddPvpStar7(PVPUserBaseData value) {
 pvpStar7_.Add(value);
 }

public const int startTimeFieldNumber = 6;
 private bool hasStartTime;
 private Int64 startTime_ = 0;
 public bool HasStartTime {
 get { return hasStartTime; }
 }
 public Int64 StartTime {
 get { return startTime_; }
 set { SetStartTime(value); }
 }
 public void SetStartTime(Int64 value) { 
 hasStartTime = true;
 startTime_ = value;
 }

public const int endTimeFieldNumber = 7;
 private bool hasEndTime;
 private Int64 endTime_ = 0;
 public bool HasEndTime {
 get { return hasEndTime; }
 }
 public Int64 EndTime {
 get { return endTime_; }
 set { SetEndTime(value); }
 }
 public void SetEndTime(Int64 value) { 
 hasEndTime = true;
 endTime_ = value;
 }

public const int ranksFieldNumber = 8;
 private pbc::PopsicleList<Int32> ranks_ = new pbc::PopsicleList<Int32>();
 public scg::IList<Int32> ranksList {
 get { return pbc::Lists.AsReadOnly(ranks_); }
 }
 
 public int ranksCount {
 get { return ranks_.Count; }
 }
 
public Int32 GetRanks(int index) {
 return ranks_[index];
 }
 public void AddRanks(Int32 value) {
 ranks_.Add(value);
 }

public const int rewardsFieldNumber = 9;
 private pbc::PopsicleList<Int32> rewards_ = new pbc::PopsicleList<Int32>();
 public scg::IList<Int32> rewardsList {
 get { return pbc::Lists.AsReadOnly(rewards_); }
 }
 
 public int rewardsCount {
 get { return rewards_.Count; }
 }
 
public Int32 GetRewards(int index) {
 return rewards_[index];
 }
 public void AddRewards(Int32 value) {
 rewards_.Add(value);
 }

public const int qxzbBeatCdTimeFieldNumber = 10;
 private bool hasQxzbBeatCdTime;
 private Int64 qxzbBeatCdTime_ = 0;
 public bool HasQxzbBeatCdTime {
 get { return hasQxzbBeatCdTime; }
 }
 public Int64 QxzbBeatCdTime {
 get { return qxzbBeatCdTime_; }
 set { SetQxzbBeatCdTime(value); }
 }
 public void SetQxzbBeatCdTime(Int64 value) { 
 hasQxzbBeatCdTime = true;
 qxzbBeatCdTime_ = value;
 }

public const int qxzbRemainingBeatCountFieldNumber = 11;
 private bool hasQxzbRemainingBeatCount;
 private Int32 qxzbRemainingBeatCount_ = 0;
 public bool HasQxzbRemainingBeatCount {
 get { return hasQxzbRemainingBeatCount; }
 }
 public Int32 QxzbRemainingBeatCount {
 get { return qxzbRemainingBeatCount_; }
 set { SetQxzbRemainingBeatCount(value); }
 }
 public void SetQxzbRemainingBeatCount(Int32 value) { 
 hasQxzbRemainingBeatCount = true;
 qxzbRemainingBeatCount_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
foreach (PVPUserBaseData element in pvpStar3List) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
{
foreach (PVPUserBaseData element in pvpStar4List) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
{
foreach (PVPUserBaseData element in pvpStar5List) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
{
foreach (PVPUserBaseData element in pvpStar6List) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)4) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
{
foreach (PVPUserBaseData element in pvpStar7List) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)5) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 if (HasStartTime) {
size += pb::CodedOutputStream.ComputeInt64Size(6, StartTime);
}
 if (HasEndTime) {
size += pb::CodedOutputStream.ComputeInt64Size(7, EndTime);
}
{
int dataSize = 0;
foreach (Int32 element in ranksList) {
dataSize += pb::CodedOutputStream.ComputeInt32SizeNoTag(element);
}
size += dataSize;
size += 1 * ranks_.Count;
}
{
int dataSize = 0;
foreach (Int32 element in rewardsList) {
dataSize += pb::CodedOutputStream.ComputeInt32SizeNoTag(element);
}
size += dataSize;
size += 1 * rewards_.Count;
}
 if (HasQxzbBeatCdTime) {
size += pb::CodedOutputStream.ComputeInt64Size(10, QxzbBeatCdTime);
}
 if (HasQxzbRemainingBeatCount) {
size += pb::CodedOutputStream.ComputeInt32Size(11, QxzbRemainingBeatCount);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 
do{
foreach (PVPUserBaseData element in pvpStar3List) {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);

do{
foreach (PVPUserBaseData element in pvpStar4List) {
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);

do{
foreach (PVPUserBaseData element in pvpStar5List) {
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);

do{
foreach (PVPUserBaseData element in pvpStar6List) {
output.WriteTag((int)4, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);

do{
foreach (PVPUserBaseData element in pvpStar7List) {
output.WriteTag((int)5, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 
if (HasStartTime) {
output.WriteInt64(6, StartTime);
}
 
if (HasEndTime) {
output.WriteInt64(7, EndTime);
}
{
if (ranks_.Count > 0) {
foreach (Int32 element in ranksList) {
output.WriteInt32(8,element);
}
}

}
{
if (rewards_.Count > 0) {
foreach (Int32 element in rewardsList) {
output.WriteInt32(9,element);
}
}

}
 
if (HasQxzbBeatCdTime) {
output.WriteInt64(10, QxzbBeatCdTime);
}
 
if (HasQxzbRemainingBeatCount) {
output.WriteInt32(11, QxzbRemainingBeatCount);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCQxzbPVPDataAsk _inst = (SCQxzbPVPDataAsk) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
PVPUserBaseData subBuilder =  new PVPUserBaseData();
input.ReadMessage(subBuilder);
_inst.AddPvpStar3(subBuilder);
break;
}
    case  18: {
PVPUserBaseData subBuilder =  new PVPUserBaseData();
input.ReadMessage(subBuilder);
_inst.AddPvpStar4(subBuilder);
break;
}
    case  26: {
PVPUserBaseData subBuilder =  new PVPUserBaseData();
input.ReadMessage(subBuilder);
_inst.AddPvpStar5(subBuilder);
break;
}
    case  34: {
PVPUserBaseData subBuilder =  new PVPUserBaseData();
input.ReadMessage(subBuilder);
_inst.AddPvpStar6(subBuilder);
break;
}
    case  42: {
PVPUserBaseData subBuilder =  new PVPUserBaseData();
input.ReadMessage(subBuilder);
_inst.AddPvpStar7(subBuilder);
break;
}
   case  48: {
 _inst.StartTime = input.ReadInt64();
break;
}
   case  56: {
 _inst.EndTime = input.ReadInt64();
break;
}
   case  64: {
 _inst.AddRanks(input.ReadInt32());
break;
}
   case  72: {
 _inst.AddRewards(input.ReadInt32());
break;
}
   case  80: {
 _inst.QxzbBeatCdTime = input.ReadInt64();
break;
}
   case  88: {
 _inst.QxzbRemainingBeatCount = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
 foreach (PVPUserBaseData element in pvpStar3List) {
if (!element.IsInitialized()) return false;
}
foreach (PVPUserBaseData element in pvpStar4List) {
if (!element.IsInitialized()) return false;
}
foreach (PVPUserBaseData element in pvpStar5List) {
if (!element.IsInitialized()) return false;
}
foreach (PVPUserBaseData element in pvpStar6List) {
if (!element.IsInitialized()) return false;
}
foreach (PVPUserBaseData element in pvpStar7List) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCRandomCardFree : PacketDistributed
{

public const int templateIDFieldNumber = 1;
 private bool hasTemplateID;
 private Int32 templateID_ = 0;
 public bool HasTemplateID {
 get { return hasTemplateID; }
 }
 public Int32 TemplateID {
 get { return templateID_; }
 set { SetTemplateID(value); }
 }
 public void SetTemplateID(Int32 value) { 
 hasTemplateID = true;
 templateID_ = value;
 }

public const int baseDataFieldNumber = 2;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int bagDataFieldNumber = 3;
 private bool hasBagData;
 private PBUserBagData bagData_ =  new PBUserBagData();
 public bool HasBagData {
 get { return hasBagData; }
 }
 public PBUserBagData BagData {
 get { return bagData_; }
 set { SetBagData(value); }
 }
 public void SetBagData(PBUserBagData value) { 
 hasBagData = true;
 bagData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasTemplateID) {
size += pb::CodedOutputStream.ComputeInt32Size(1, TemplateID);
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasTemplateID) {
output.WriteInt32(1, TemplateID);
}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
{
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BagData.SerializedSize());
BagData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCRandomCardFree _inst = (SCRandomCardFree) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.TemplateID = input.ReadInt32();
break;
}
    case  18: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
    case  26: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BagData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
  if (HasBagData) {
if (!BagData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCSDKLoginThirdPlatformRet : PacketDistributed
{

public const int stateFieldNumber = 1;
 private bool hasState;
 private Int32 state_ = 0;
 public bool HasState {
 get { return hasState; }
 }
 public Int32 State {
 get { return state_; }
 set { SetState(value); }
 }
 public void SetState(Int32 value) { 
 hasState = true;
 state_ = value;
 }

public const int ucAccountFieldNumber = 2;
 private bool hasUcAccount;
 private string ucAccount_ = "";
 public bool HasUcAccount {
 get { return hasUcAccount; }
 }
 public string UcAccount {
 get { return ucAccount_; }
 set { SetUcAccount(value); }
 }
 public void SetUcAccount(string value) { 
 hasUcAccount = true;
 ucAccount_ = value;
 }

public const int nickNameFieldNumber = 3;
 private bool hasNickName;
 private string nickName_ = "";
 public bool HasNickName {
 get { return hasNickName; }
 }
 public string NickName {
 get { return nickName_; }
 set { SetNickName(value); }
 }
 public void SetNickName(string value) { 
 hasNickName = true;
 nickName_ = value;
 }

public const int guide_finished_stepFieldNumber = 4;
 private bool hasGuide_finished_step;
 private Int32 guide_finished_step_ = 0;
 public bool HasGuide_finished_step {
 get { return hasGuide_finished_step; }
 }
 public Int32 Guide_finished_step {
 get { return guide_finished_step_; }
 set { SetGuide_finished_step(value); }
 }
 public void SetGuide_finished_step(Int32 value) { 
 hasGuide_finished_step = true;
 guide_finished_step_ = value;
 }

public const int retCodeFieldNumber = 5;
 private bool hasRetCode;
 private Int32 retCode_ = 0;
 public bool HasRetCode {
 get { return hasRetCode; }
 }
 public Int32 RetCode {
 get { return retCode_; }
 set { SetRetCode(value); }
 }
 public void SetRetCode(Int32 value) { 
 hasRetCode = true;
 retCode_ = value;
 }

public const int gameIdFieldNumber = 6;
 private bool hasGameId;
 private Int32 gameId_ = 0;
 public bool HasGameId {
 get { return hasGameId; }
 }
 public Int32 GameId {
 get { return gameId_; }
 set { SetGameId(value); }
 }
 public void SetGameId(Int32 value) { 
 hasGameId = true;
 gameId_ = value;
 }

public const int serverIdFieldNumber = 7;
 private bool hasServerId;
 private Int32 serverId_ = 0;
 public bool HasServerId {
 get { return hasServerId; }
 }
 public Int32 ServerId {
 get { return serverId_; }
 set { SetServerId(value); }
 }
 public void SetServerId(Int32 value) { 
 hasServerId = true;
 serverId_ = value;
 }

public const int appkeyFieldNumber = 8;
 private bool hasAppkey;
 private string appkey_ = "";
 public bool HasAppkey {
 get { return hasAppkey; }
 }
 public string Appkey {
 get { return appkey_; }
 set { SetAppkey(value); }
 }
 public void SetAppkey(string value) { 
 hasAppkey = true;
 appkey_ = value;
 }

public const int appsecretFieldNumber = 9;
 private bool hasAppsecret;
 private string appsecret_ = "";
 public bool HasAppsecret {
 get { return hasAppsecret; }
 }
 public string Appsecret {
 get { return appsecret_; }
 set { SetAppsecret(value); }
 }
 public void SetAppsecret(string value) { 
 hasAppsecret = true;
 appsecret_ = value;
 }

public const int cpIdFieldNumber = 10;
 private bool hasCpId;
 private Int32 cpId_ = 0;
 public bool HasCpId {
 get { return hasCpId; }
 }
 public Int32 CpId {
 get { return cpId_; }
 set { SetCpId(value); }
 }
 public void SetCpId(Int32 value) { 
 hasCpId = true;
 cpId_ = value;
 }

public const int apiKeyFieldNumber = 11;
 private bool hasApiKey;
 private string apiKey_ = "";
 public bool HasApiKey {
 get { return hasApiKey; }
 }
 public string ApiKey {
 get { return apiKey_; }
 set { SetApiKey(value); }
 }
 public void SetApiKey(string value) { 
 hasApiKey = true;
 apiKey_ = value;
 }

public const int ucchannelIdFieldNumber = 12;
 private bool hasUcchannelId;
 private Int32 ucchannelId_ = 0;
 public bool HasUcchannelId {
 get { return hasUcchannelId; }
 }
 public Int32 UcchannelId {
 get { return ucchannelId_; }
 set { SetUcchannelId(value); }
 }
 public void SetUcchannelId(Int32 value) { 
 hasUcchannelId = true;
 ucchannelId_ = value;
 }

public const int accountIdFieldNumber = 13;
 private bool hasAccountId;
 private Int64 accountId_ = 0;
 public bool HasAccountId {
 get { return hasAccountId; }
 }
 public Int64 AccountId {
 get { return accountId_; }
 set { SetAccountId(value); }
 }
 public void SetAccountId(Int64 value) { 
 hasAccountId = true;
 accountId_ = value;
 }

public const int giftisonFieldNumber = 14;
 private bool hasGiftison;
 private Int32 giftison_ = 0;
 public bool HasGiftison {
 get { return hasGiftison; }
 }
 public Int32 Giftison {
 get { return giftison_; }
 set { SetGiftison(value); }
 }
 public void SetGiftison(Int32 value) { 
 hasGiftison = true;
 giftison_ = value;
 }

public const int jsonDataFieldNumber = 15;
 private bool hasJsonData;
 private string jsonData_ = "";
 public bool HasJsonData {
 get { return hasJsonData; }
 }
 public string JsonData {
 get { return jsonData_; }
 set { SetJsonData(value); }
 }
 public void SetJsonData(string value) { 
 hasJsonData = true;
 jsonData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasState) {
size += pb::CodedOutputStream.ComputeInt32Size(1, State);
}
 if (HasUcAccount) {
size += pb::CodedOutputStream.ComputeStringSize(2, UcAccount);
}
 if (HasNickName) {
size += pb::CodedOutputStream.ComputeStringSize(3, NickName);
}
 if (HasGuide_finished_step) {
size += pb::CodedOutputStream.ComputeInt32Size(4, Guide_finished_step);
}
 if (HasRetCode) {
size += pb::CodedOutputStream.ComputeInt32Size(5, RetCode);
}
 if (HasGameId) {
size += pb::CodedOutputStream.ComputeInt32Size(6, GameId);
}
 if (HasServerId) {
size += pb::CodedOutputStream.ComputeInt32Size(7, ServerId);
}
 if (HasAppkey) {
size += pb::CodedOutputStream.ComputeStringSize(8, Appkey);
}
 if (HasAppsecret) {
size += pb::CodedOutputStream.ComputeStringSize(9, Appsecret);
}
 if (HasCpId) {
size += pb::CodedOutputStream.ComputeInt32Size(10, CpId);
}
 if (HasApiKey) {
size += pb::CodedOutputStream.ComputeStringSize(11, ApiKey);
}
 if (HasUcchannelId) {
size += pb::CodedOutputStream.ComputeInt32Size(12, UcchannelId);
}
 if (HasAccountId) {
size += pb::CodedOutputStream.ComputeInt64Size(13, AccountId);
}
 if (HasGiftison) {
size += pb::CodedOutputStream.ComputeInt32Size(14, Giftison);
}
 if (HasJsonData) {
size += pb::CodedOutputStream.ComputeStringSize(15, JsonData);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasState) {
output.WriteInt32(1, State);
}
 
if (HasUcAccount) {
output.WriteString(2, UcAccount);
}
 
if (HasNickName) {
output.WriteString(3, NickName);
}
 
if (HasGuide_finished_step) {
output.WriteInt32(4, Guide_finished_step);
}
 
if (HasRetCode) {
output.WriteInt32(5, RetCode);
}
 
if (HasGameId) {
output.WriteInt32(6, GameId);
}
 
if (HasServerId) {
output.WriteInt32(7, ServerId);
}
 
if (HasAppkey) {
output.WriteString(8, Appkey);
}
 
if (HasAppsecret) {
output.WriteString(9, Appsecret);
}
 
if (HasCpId) {
output.WriteInt32(10, CpId);
}
 
if (HasApiKey) {
output.WriteString(11, ApiKey);
}
 
if (HasUcchannelId) {
output.WriteInt32(12, UcchannelId);
}
 
if (HasAccountId) {
output.WriteInt64(13, AccountId);
}
 
if (HasGiftison) {
output.WriteInt32(14, Giftison);
}
 
if (HasJsonData) {
output.WriteString(15, JsonData);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCSDKLoginThirdPlatformRet _inst = (SCSDKLoginThirdPlatformRet) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.State = input.ReadInt32();
break;
}
   case  18: {
 _inst.UcAccount = input.ReadString();
break;
}
   case  26: {
 _inst.NickName = input.ReadString();
break;
}
   case  32: {
 _inst.Guide_finished_step = input.ReadInt32();
break;
}
   case  40: {
 _inst.RetCode = input.ReadInt32();
break;
}
   case  48: {
 _inst.GameId = input.ReadInt32();
break;
}
   case  56: {
 _inst.ServerId = input.ReadInt32();
break;
}
   case  66: {
 _inst.Appkey = input.ReadString();
break;
}
   case  74: {
 _inst.Appsecret = input.ReadString();
break;
}
   case  80: {
 _inst.CpId = input.ReadInt32();
break;
}
   case  90: {
 _inst.ApiKey = input.ReadString();
break;
}
   case  96: {
 _inst.UcchannelId = input.ReadInt32();
break;
}
   case  104: {
 _inst.AccountId = input.ReadInt64();
break;
}
   case  112: {
 _inst.Giftison = input.ReadInt32();
break;
}
   case  122: {
 _inst.JsonData = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasUcAccount) return false;
 if (!hasNickName) return false;
 return true;
 }

	}


[Serializable]
public class SCSDKRefreshRet : PacketDistributed
{

public const int jsonDataFieldNumber = 1;
 private bool hasJsonData;
 private string jsonData_ = "";
 public bool HasJsonData {
 get { return hasJsonData; }
 }
 public string JsonData {
 get { return jsonData_; }
 set { SetJsonData(value); }
 }
 public void SetJsonData(string value) { 
 hasJsonData = true;
 jsonData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasJsonData) {
size += pb::CodedOutputStream.ComputeStringSize(1, JsonData);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasJsonData) {
output.WriteString(1, JsonData);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCSDKRefreshRet _inst = (SCSDKRefreshRet) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  10: {
 _inst.JsonData = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


[Serializable]
public class SCSearchFriend : PacketDistributed
{

public const int friend_infoFieldNumber = 1;
 private bool hasFriend_info;
 private PBFriend friend_info_ =  new PBFriend();
 public bool HasFriend_info {
 get { return hasFriend_info; }
 }
 public PBFriend Friend_info {
 get { return friend_info_; }
 set { SetFriend_info(value); }
 }
 public void SetFriend_info(PBFriend value) { 
 hasFriend_info = true;
 friend_info_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int subsize = Friend_info.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)Friend_info.SerializedSize());
Friend_info.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCSearchFriend _inst = (SCSearchFriend) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
PBFriend subBuilder =  new PBFriend();
 input.ReadMessage(subBuilder);
_inst.Friend_info = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasFriend_info) {
if (!Friend_info.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCSellCard : PacketDistributed
{

public const int card_guidFieldNumber = 1;
 private pbc::PopsicleList<Int64> card_guid_ = new pbc::PopsicleList<Int64>();
 public scg::IList<Int64> card_guidList {
 get { return pbc::Lists.AsReadOnly(card_guid_); }
 }
 
 public int card_guidCount {
 get { return card_guid_.Count; }
 }
 
public Int64 GetCard_guid(int index) {
 return card_guid_[index];
 }
 public void AddCard_guid(Int64 value) {
 card_guid_.Add(value);
 }

public const int new_moneyFieldNumber = 2;
 private bool hasNew_money;
 private Int32 new_money_ = 0;
 public bool HasNew_money {
 get { return hasNew_money; }
 }
 public Int32 New_money {
 get { return new_money_; }
 set { SetNew_money(value); }
 }
 public void SetNew_money(Int32 value) { 
 hasNew_money = true;
 new_money_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int dataSize = 0;
foreach (Int64 element in card_guidList) {
dataSize += pb::CodedOutputStream.ComputeInt64SizeNoTag(element);
}
size += dataSize;
size += 1 * card_guid_.Count;
}
 if (HasNew_money) {
size += pb::CodedOutputStream.ComputeInt32Size(2, New_money);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
if (card_guid_.Count > 0) {
foreach (Int64 element in card_guidList) {
output.WriteInt64(1,element);
}
}

}
 
if (HasNew_money) {
output.WriteInt32(2, New_money);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCSellCard _inst = (SCSellCard) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.AddCard_guid(input.ReadInt64());
break;
}
   case  16: {
 _inst.New_money = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasNew_money) return false;
 return true;
 }

	}


[Serializable]
public class SCShopRet : PacketDistributed
{

public const int resultFieldNumber = 1;
 private bool hasResult;
 private Int32 result_ = 0;
 public bool HasResult {
 get { return hasResult; }
 }
 public Int32 Result {
 get { return result_; }
 set { SetResult(value); }
 }
 public void SetResult(Int32 value) { 
 hasResult = true;
 result_ = value;
 }

public const int baseDataFieldNumber = 2;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasResult) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Result);
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasResult) {
output.WriteInt32(1, Result);
}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCShopRet _inst = (SCShopRet) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Result = input.ReadInt32();
break;
}
    case  18: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasResult) return false;
  if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCStudySkill : PacketDistributed
{

public const int combine_resultFieldNumber = 1;
 private bool hasCombine_result;
 private Int32 combine_result_ = 0;
 public bool HasCombine_result {
 get { return hasCombine_result; }
 }
 public Int32 Combine_result {
 get { return combine_result_; }
 set { SetCombine_result(value); }
 }
 public void SetCombine_result(Int32 value) { 
 hasCombine_result = true;
 combine_result_ = value;
 }

public const int baseDataFieldNumber = 2;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int bagDataFieldNumber = 3;
 private bool hasBagData;
 private PBUserBagData bagData_ =  new PBUserBagData();
 public bool HasBagData {
 get { return hasBagData; }
 }
 public PBUserBagData BagData {
 get { return bagData_; }
 set { SetBagData(value); }
 }
 public void SetBagData(PBUserBagData value) { 
 hasBagData = true;
 bagData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasCombine_result) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Combine_result);
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasCombine_result) {
output.WriteInt32(1, Combine_result);
}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
{
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BagData.SerializedSize());
BagData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCStudySkill _inst = (SCStudySkill) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Combine_result = input.ReadInt32();
break;
}
    case  18: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
    case  26: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BagData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasCombine_result) return false;
  if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
  if (HasBagData) {
if (!BagData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCStudySkillUpdate : PacketDistributed
{

public const int combine_resultFieldNumber = 1;
 private bool hasCombine_result;
 private Int32 combine_result_ = 0;
 public bool HasCombine_result {
 get { return hasCombine_result; }
 }
 public Int32 Combine_result {
 get { return combine_result_; }
 set { SetCombine_result(value); }
 }
 public void SetCombine_result(Int32 value) { 
 hasCombine_result = true;
 combine_result_ = value;
 }

public const int baseDataFieldNumber = 2;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int bagDataFieldNumber = 3;
 private bool hasBagData;
 private PBUserBagData bagData_ =  new PBUserBagData();
 public bool HasBagData {
 get { return hasBagData; }
 }
 public PBUserBagData BagData {
 get { return bagData_; }
 set { SetBagData(value); }
 }
 public void SetBagData(PBUserBagData value) { 
 hasBagData = true;
 bagData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasCombine_result) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Combine_result);
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasCombine_result) {
output.WriteInt32(1, Combine_result);
}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
{
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BagData.SerializedSize());
BagData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCStudySkillUpdate _inst = (SCStudySkillUpdate) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Combine_result = input.ReadInt32();
break;
}
    case  18: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
    case  26: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BagData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasCombine_result) return false;
  if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
  if (HasBagData) {
if (!BagData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCTaskList : PacketDistributed
{

public const int pbTaskFieldNumber = 1;
 private pbc::PopsicleList<PBTask> pbTask_ = new pbc::PopsicleList<PBTask>();
 public scg::IList<PBTask> pbTaskList {
 get { return pbc::Lists.AsReadOnly(pbTask_); }
 }
 
 public int pbTaskCount {
 get { return pbTask_.Count; }
 }
 
public PBTask GetPbTask(int index) {
 return pbTask_[index];
 }
 public void AddPbTask(PBTask value) {
 pbTask_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
foreach (PBTask element in pbTaskList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 
do{
foreach (PBTask element in pbTaskList) {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCTaskList _inst = (SCTaskList) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
PBTask subBuilder =  new PBTask();
input.ReadMessage(subBuilder);
_inst.AddPbTask(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
 foreach (PBTask element in pbTaskList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCTaskOver : PacketDistributed
{

public const int taskOverFieldNumber = 1;
 private bool hasTaskOver;
 private Int32 taskOver_ = 0;
 public bool HasTaskOver {
 get { return hasTaskOver; }
 }
 public Int32 TaskOver {
 get { return taskOver_; }
 set { SetTaskOver(value); }
 }
 public void SetTaskOver(Int32 value) { 
 hasTaskOver = true;
 taskOver_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasTaskOver) {
size += pb::CodedOutputStream.ComputeInt32Size(1, TaskOver);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasTaskOver) {
output.WriteInt32(1, TaskOver);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCTaskOver _inst = (SCTaskOver) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.TaskOver = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasTaskOver) return false;
 return true;
 }

	}


[Serializable]
public class SCThirdPlatformVerifyCharge : PacketDistributed
{

public const int resultFieldNumber = 1;
 private bool hasResult;
 private Int32 result_ = 0;
 public bool HasResult {
 get { return hasResult; }
 }
 public Int32 Result {
 get { return result_; }
 set { SetResult(value); }
 }
 public void SetResult(Int32 value) { 
 hasResult = true;
 result_ = value;
 }

public const int goodsIdFieldNumber = 2;
 private bool hasGoodsId;
 private Int32 goodsId_ = 0;
 public bool HasGoodsId {
 get { return hasGoodsId; }
 }
 public Int32 GoodsId {
 get { return goodsId_; }
 set { SetGoodsId(value); }
 }
 public void SetGoodsId(Int32 value) { 
 hasGoodsId = true;
 goodsId_ = value;
 }

public const int playerDollarFieldNumber = 3;
 private bool hasPlayerDollar;
 private Int32 playerDollar_ = 0;
 public bool HasPlayerDollar {
 get { return hasPlayerDollar; }
 }
 public Int32 PlayerDollar {
 get { return playerDollar_; }
 set { SetPlayerDollar(value); }
 }
 public void SetPlayerDollar(Int32 value) { 
 hasPlayerDollar = true;
 playerDollar_ = value;
 }

public const int orderIdFieldNumber = 4;
 private bool hasOrderId;
 private string orderId_ = "";
 public bool HasOrderId {
 get { return hasOrderId; }
 }
 public string OrderId {
 get { return orderId_; }
 set { SetOrderId(value); }
 }
 public void SetOrderId(string value) { 
 hasOrderId = true;
 orderId_ = value;
 }

public const int goodsPriceFieldNumber = 5;
 private bool hasGoodsPrice;
 private Int32 goodsPrice_ = 0;
 public bool HasGoodsPrice {
 get { return hasGoodsPrice; }
 }
 public Int32 GoodsPrice {
 get { return goodsPrice_; }
 set { SetGoodsPrice(value); }
 }
 public void SetGoodsPrice(Int32 value) { 
 hasGoodsPrice = true;
 goodsPrice_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasResult) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Result);
}
 if (HasGoodsId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, GoodsId);
}
 if (HasPlayerDollar) {
size += pb::CodedOutputStream.ComputeInt32Size(3, PlayerDollar);
}
 if (HasOrderId) {
size += pb::CodedOutputStream.ComputeStringSize(4, OrderId);
}
 if (HasGoodsPrice) {
size += pb::CodedOutputStream.ComputeInt32Size(5, GoodsPrice);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasResult) {
output.WriteInt32(1, Result);
}
 
if (HasGoodsId) {
output.WriteInt32(2, GoodsId);
}
 
if (HasPlayerDollar) {
output.WriteInt32(3, PlayerDollar);
}
 
if (HasOrderId) {
output.WriteString(4, OrderId);
}
 
if (HasGoodsPrice) {
output.WriteInt32(5, GoodsPrice);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCThirdPlatformVerifyCharge _inst = (SCThirdPlatformVerifyCharge) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Result = input.ReadInt32();
break;
}
   case  16: {
 _inst.GoodsId = input.ReadInt32();
break;
}
   case  24: {
 _inst.PlayerDollar = input.ReadInt32();
break;
}
   case  34: {
 _inst.OrderId = input.ReadString();
break;
}
   case  40: {
 _inst.GoodsPrice = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasResult) return false;
 if (!hasGoodsId) return false;
 if (!hasPlayerDollar) return false;
 if (!hasOrderId) return false;
 if (!hasGoodsPrice) return false;
 return true;
 }

	}


[Serializable]
public class SCWorldBossAddZhufu : PacketDistributed
{

public const int baseDataFieldNumber = 1;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int currentAttInfoFieldNumber = 2;
 private bool hasCurrentAttInfo;
 private WorldBossAttInfo currentAttInfo_ =  new WorldBossAttInfo();
 public bool HasCurrentAttInfo {
 get { return hasCurrentAttInfo; }
 }
 public WorldBossAttInfo CurrentAttInfo {
 get { return currentAttInfo_; }
 set { SetCurrentAttInfo(value); }
 }
 public void SetCurrentAttInfo(WorldBossAttInfo value) { 
 hasCurrentAttInfo = true;
 currentAttInfo_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = CurrentAttInfo.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)CurrentAttInfo.SerializedSize());
CurrentAttInfo.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCWorldBossAddZhufu _inst = (SCWorldBossAddZhufu) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
    case  18: {
WorldBossAttInfo subBuilder =  new WorldBossAttInfo();
 input.ReadMessage(subBuilder);
_inst.CurrentAttInfo = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
  if (HasCurrentAttInfo) {
if (!CurrentAttInfo.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCWorldBossResurgence : PacketDistributed
{

public const int baseDataFieldNumber = 1;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int currentAttInfoFieldNumber = 2;
 private bool hasCurrentAttInfo;
 private WorldBossAttInfo currentAttInfo_ =  new WorldBossAttInfo();
 public bool HasCurrentAttInfo {
 get { return hasCurrentAttInfo; }
 }
 public WorldBossAttInfo CurrentAttInfo {
 get { return currentAttInfo_; }
 set { SetCurrentAttInfo(value); }
 }
 public void SetCurrentAttInfo(WorldBossAttInfo value) { 
 hasCurrentAttInfo = true;
 currentAttInfo_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = CurrentAttInfo.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)CurrentAttInfo.SerializedSize());
CurrentAttInfo.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCWorldBossResurgence _inst = (SCWorldBossResurgence) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
    case  18: {
WorldBossAttInfo subBuilder =  new WorldBossAttInfo();
 input.ReadMessage(subBuilder);
_inst.CurrentAttInfo = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
  if (HasCurrentAttInfo) {
if (!CurrentAttInfo.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCWorldBossWeekRank : PacketDistributed
{

public const int playerRankInfoFieldNumber = 1;
 private bool hasPlayerRankInfo;
 private WorldBossDamageRankInfo playerRankInfo_ =  new WorldBossDamageRankInfo();
 public bool HasPlayerRankInfo {
 get { return hasPlayerRankInfo; }
 }
 public WorldBossDamageRankInfo PlayerRankInfo {
 get { return playerRankInfo_; }
 set { SetPlayerRankInfo(value); }
 }
 public void SetPlayerRankInfo(WorldBossDamageRankInfo value) { 
 hasPlayerRankInfo = true;
 playerRankInfo_ = value;
 }

public const int hasRewardFieldNumber = 2;
 private bool hasHasReward;
 private Int32 hasReward_ = 0;
 public bool HasHasReward {
 get { return hasHasReward; }
 }
 public Int32 HasReward {
 get { return hasReward_; }
 set { SetHasReward(value); }
 }
 public void SetHasReward(Int32 value) { 
 hasHasReward = true;
 hasReward_ = value;
 }

public const int top10RankInfoFieldNumber = 3;
 private pbc::PopsicleList<WorldBossDamageRankInfo> top10RankInfo_ = new pbc::PopsicleList<WorldBossDamageRankInfo>();
 public scg::IList<WorldBossDamageRankInfo> top10RankInfoList {
 get { return pbc::Lists.AsReadOnly(top10RankInfo_); }
 }
 
 public int top10RankInfoCount {
 get { return top10RankInfo_.Count; }
 }
 
public WorldBossDamageRankInfo GetTop10RankInfo(int index) {
 return top10RankInfo_[index];
 }
 public void AddTop10RankInfo(WorldBossDamageRankInfo value) {
 top10RankInfo_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int subsize = PlayerRankInfo.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 if (HasHasReward) {
size += pb::CodedOutputStream.ComputeInt32Size(2, HasReward);
}
{
foreach (WorldBossDamageRankInfo element in top10RankInfoList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)PlayerRankInfo.SerializedSize());
PlayerRankInfo.WriteTo(output);

}
 
if (HasHasReward) {
output.WriteInt32(2, HasReward);
}

do{
foreach (WorldBossDamageRankInfo element in top10RankInfoList) {
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCWorldBossWeekRank _inst = (SCWorldBossWeekRank) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
WorldBossDamageRankInfo subBuilder =  new WorldBossDamageRankInfo();
 input.ReadMessage(subBuilder);
_inst.PlayerRankInfo = subBuilder;
break;
}
   case  16: {
 _inst.HasReward = input.ReadInt32();
break;
}
    case  26: {
WorldBossDamageRankInfo subBuilder =  new WorldBossDamageRankInfo();
input.ReadMessage(subBuilder);
_inst.AddTop10RankInfo(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasPlayerRankInfo) {
if (!PlayerRankInfo.IsInitialized()) return false;
}
foreach (WorldBossDamageRankInfo element in top10RankInfoList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCWorldBossWeekReward : PacketDistributed
{

public const int playerRankFieldNumber = 1;
 private bool hasPlayerRank;
 private Int32 playerRank_ = 0;
 public bool HasPlayerRank {
 get { return hasPlayerRank; }
 }
 public Int32 PlayerRank {
 get { return playerRank_; }
 set { SetPlayerRank(value); }
 }
 public void SetPlayerRank(Int32 value) { 
 hasPlayerRank = true;
 playerRank_ = value;
 }

public const int baseDataFieldNumber = 2;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int rewardLevFieldNumber = 3;
 private bool hasRewardLev;
 private Int32 rewardLev_ = 0;
 public bool HasRewardLev {
 get { return hasRewardLev; }
 }
 public Int32 RewardLev {
 get { return rewardLev_; }
 set { SetRewardLev(value); }
 }
 public void SetRewardLev(Int32 value) { 
 hasRewardLev = true;
 rewardLev_ = value;
 }

public const int hasRewardFieldNumber = 4;
 private bool hasHasReward;
 private Int32 hasReward_ = 0;
 public bool HasHasReward {
 get { return hasHasReward; }
 }
 public Int32 HasReward {
 get { return hasReward_; }
 set { SetHasReward(value); }
 }
 public void SetHasReward(Int32 value) { 
 hasHasReward = true;
 hasReward_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasPlayerRank) {
size += pb::CodedOutputStream.ComputeInt32Size(1, PlayerRank);
}
{
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 if (HasRewardLev) {
size += pb::CodedOutputStream.ComputeInt32Size(3, RewardLev);
}
 if (HasHasReward) {
size += pb::CodedOutputStream.ComputeInt32Size(4, HasReward);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasPlayerRank) {
output.WriteInt32(1, PlayerRank);
}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
 
if (HasRewardLev) {
output.WriteInt32(3, RewardLev);
}
 
if (HasHasReward) {
output.WriteInt32(4, HasReward);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCWorldBossWeekReward _inst = (SCWorldBossWeekReward) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.PlayerRank = input.ReadInt32();
break;
}
    case  18: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
   case  24: {
 _inst.RewardLev = input.ReadInt32();
break;
}
   case  32: {
 _inst.HasReward = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCWuxingActivation : PacketDistributed
{

public const int fengshuiFieldNumber = 1;
 private bool hasFengshui;
 private FengshuiInfo fengshui_ =  new FengshuiInfo();
 public bool HasFengshui {
 get { return hasFengshui; }
 }
 public FengshuiInfo Fengshui {
 get { return fengshui_; }
 set { SetFengshui(value); }
 }
 public void SetFengshui(FengshuiInfo value) { 
 hasFengshui = true;
 fengshui_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int subsize = Fengshui.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)Fengshui.SerializedSize());
Fengshui.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCWuxingActivation _inst = (SCWuxingActivation) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
FengshuiInfo subBuilder =  new FengshuiInfo();
 input.ReadMessage(subBuilder);
_inst.Fengshui = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasFengshui) {
if (!Fengshui.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCWuxingLevelup : PacketDistributed
{

public const int fengshuiFieldNumber = 1;
 private bool hasFengshui;
 private FengshuiInfo fengshui_ =  new FengshuiInfo();
 public bool HasFengshui {
 get { return hasFengshui; }
 }
 public FengshuiInfo Fengshui {
 get { return fengshui_; }
 set { SetFengshui(value); }
 }
 public void SetFengshui(FengshuiInfo value) { 
 hasFengshui = true;
 fengshui_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int subsize = Fengshui.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)Fengshui.SerializedSize());
Fengshui.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCWuxingLevelup _inst = (SCWuxingLevelup) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
FengshuiInfo subBuilder =  new FengshuiInfo();
 input.ReadMessage(subBuilder);
_inst.Fengshui = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasFengshui) {
if (!Fengshui.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCWuxingReset : PacketDistributed
{

public const int fengshuiFieldNumber = 1;
 private bool hasFengshui;
 private FengshuiInfo fengshui_ =  new FengshuiInfo();
 public bool HasFengshui {
 get { return hasFengshui; }
 }
 public FengshuiInfo Fengshui {
 get { return fengshui_; }
 set { SetFengshui(value); }
 }
 public void SetFengshui(FengshuiInfo value) { 
 hasFengshui = true;
 fengshui_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int subsize = Fengshui.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)Fengshui.SerializedSize());
Fengshui.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCWuxingReset _inst = (SCWuxingReset) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
FengshuiInfo subBuilder =  new FengshuiInfo();
 input.ReadMessage(subBuilder);
_inst.Fengshui = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasFengshui) {
if (!Fengshui.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCYunyingHuodong : PacketDistributed
{

public const int HasHuodongFieldNumber = 1;
 private bool hasHasHuodong;
 private Int32 HasHuodong_ = 0;
 public bool HasHasHuodong {
 get { return hasHasHuodong; }
 }
 public Int32 HasHuodong {
 get { return HasHuodong_; }
 set { SetHasHuodong(value); }
 }
 public void SetHasHuodong(Int32 value) { 
 hasHasHuodong = true;
 HasHuodong_ = value;
 }

public const int HuodongMiaoshuFieldNumber = 2;
 private bool hasHuodongMiaoshu;
 private string HuodongMiaoshu_ = "";
 public bool HasHuodongMiaoshu {
 get { return hasHuodongMiaoshu; }
 }
 public string HuodongMiaoshu {
 get { return HuodongMiaoshu_; }
 set { SetHuodongMiaoshu(value); }
 }
 public void SetHuodongMiaoshu(string value) { 
 hasHuodongMiaoshu = true;
 HuodongMiaoshu_ = value;
 }

public const int HuodongArrayFieldNumber = 3;
 private pbc::PopsicleList<PBYunyingHuodong> HuodongArray_ = new pbc::PopsicleList<PBYunyingHuodong>();
 public scg::IList<PBYunyingHuodong> HuodongArrayList {
 get { return pbc::Lists.AsReadOnly(HuodongArray_); }
 }
 
 public int HuodongArrayCount {
 get { return HuodongArray_.Count; }
 }
 
public PBYunyingHuodong GetHuodongArray(int index) {
 return HuodongArray_[index];
 }
 public void AddHuodongArray(PBYunyingHuodong value) {
 HuodongArray_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasHasHuodong) {
size += pb::CodedOutputStream.ComputeInt32Size(1, HasHuodong);
}
 if (HasHuodongMiaoshu) {
size += pb::CodedOutputStream.ComputeStringSize(2, HuodongMiaoshu);
}
{
foreach (PBYunyingHuodong element in HuodongArrayList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasHasHuodong) {
output.WriteInt32(1, HasHuodong);
}
 
if (HasHuodongMiaoshu) {
output.WriteString(2, HuodongMiaoshu);
}

do{
foreach (PBYunyingHuodong element in HuodongArrayList) {
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCYunyingHuodong _inst = (SCYunyingHuodong) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.HasHuodong = input.ReadInt32();
break;
}
   case  18: {
 _inst.HuodongMiaoshu = input.ReadString();
break;
}
    case  26: {
PBYunyingHuodong subBuilder =  new PBYunyingHuodong();
input.ReadMessage(subBuilder);
_inst.AddHuodongArray(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasHasHuodong) return false;
foreach (PBYunyingHuodong element in HuodongArrayList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SCscode : PacketDistributed
{

public const int baseDataFieldNumber = 1;
 private bool hasBaseData;
 private PBUserBaseData baseData_ =  new PBUserBaseData();
 public bool HasBaseData {
 get { return hasBaseData; }
 }
 public PBUserBaseData BaseData {
 get { return baseData_; }
 set { SetBaseData(value); }
 }
 public void SetBaseData(PBUserBaseData value) { 
 hasBaseData = true;
 baseData_ = value;
 }

public const int bagDataFieldNumber = 2;
 private bool hasBagData;
 private PBUserBagData bagData_ =  new PBUserBagData();
 public bool HasBagData {
 get { return hasBagData; }
 }
 public PBUserBagData BagData {
 get { return bagData_; }
 set { SetBagData(value); }
 }
 public void SetBagData(PBUserBagData value) { 
 hasBagData = true;
 bagData_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int subsize = BaseData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = BagData.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BaseData.SerializedSize());
BaseData.WriteTo(output);

}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)BagData.SerializedSize());
BagData.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SCscode _inst = (SCscode) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
PBUserBaseData subBuilder =  new PBUserBaseData();
 input.ReadMessage(subBuilder);
_inst.BaseData = subBuilder;
break;
}
    case  18: {
PBUserBagData subBuilder =  new PBUserBagData();
 input.ReadMessage(subBuilder);
_inst.BagData = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasBaseData) {
if (!BaseData.IsInitialized()) return false;
}
  if (HasBagData) {
if (!BagData.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class SuipianInfo : PacketDistributed
{

public const int idFieldNumber = 1;
 private bool hasId;
 private Int32 id_ = 0;
 public bool HasId {
 get { return hasId; }
 }
 public Int32 Id {
 get { return id_; }
 set { SetId(value); }
 }
 public void SetId(Int32 value) { 
 hasId = true;
 id_ = value;
 }

public const int numFieldNumber = 2;
 private bool hasNum;
 private Int32 num_ = 0;
 public bool HasNum {
 get { return hasNum; }
 }
 public Int32 Num {
 get { return num_; }
 set { SetNum(value); }
 }
 public void SetNum(Int32 value) { 
 hasNum = true;
 num_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Id);
}
 if (HasNum) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Num);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasId) {
output.WriteInt32(1, Id);
}
 
if (HasNum) {
output.WriteInt32(2, Num);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 SuipianInfo _inst = (SuipianInfo) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Id = input.ReadInt32();
break;
}
   case  16: {
 _inst.Num = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasId) return false;
 if (!hasNum) return false;
 return true;
 }

	}


[Serializable]
public class WorldBoss : PacketDistributed
{

public const int idFieldNumber = 1;
 private bool hasId;
 private Int32 id_ = 0;
 public bool HasId {
 get { return hasId; }
 }
 public Int32 Id {
 get { return id_; }
 set { SetId(value); }
 }
 public void SetId(Int32 value) { 
 hasId = true;
 id_ = value;
 }

public const int nameFieldNumber = 2;
 private bool hasName;
 private string name_ = "";
 public bool HasName {
 get { return hasName; }
 }
 public string Name {
 get { return name_; }
 set { SetName(value); }
 }
 public void SetName(string value) { 
 hasName = true;
 name_ = value;
 }

public const int cardidFieldNumber = 3;
 private bool hasCardid;
 private Int32 cardid_ = 0;
 public bool HasCardid {
 get { return hasCardid; }
 }
 public Int32 Cardid {
 get { return cardid_; }
 set { SetCardid(value); }
 }
 public void SetCardid(Int32 value) { 
 hasCardid = true;
 cardid_ = value;
 }

public const int currentHpFieldNumber = 4;
 private bool hasCurrentHp;
 private Int64 currentHp_ = 0;
 public bool HasCurrentHp {
 get { return hasCurrentHp; }
 }
 public Int64 CurrentHp {
 get { return currentHp_; }
 set { SetCurrentHp(value); }
 }
 public void SetCurrentHp(Int64 value) { 
 hasCurrentHp = true;
 currentHp_ = value;
 }

public const int totalHpFieldNumber = 5;
 private bool hasTotalHp;
 private Int64 totalHp_ = 0;
 public bool HasTotalHp {
 get { return hasTotalHp; }
 }
 public Int64 TotalHp {
 get { return totalHp_; }
 set { SetTotalHp(value); }
 }
 public void SetTotalHp(Int64 value) { 
 hasTotalHp = true;
 totalHp_ = value;
 }

public const int activeFlagFieldNumber = 6;
 private bool hasActiveFlag;
 private Int32 activeFlag_ = 0;
 public bool HasActiveFlag {
 get { return hasActiveFlag; }
 }
 public Int32 ActiveFlag {
 get { return activeFlag_; }
 set { SetActiveFlag(value); }
 }
 public void SetActiveFlag(Int32 value) { 
 hasActiveFlag = true;
 activeFlag_ = value;
 }

public const int remainOpenTimeFieldNumber = 7;
 private bool hasRemainOpenTime;
 private Int32 remainOpenTime_ = 0;
 public bool HasRemainOpenTime {
 get { return hasRemainOpenTime; }
 }
 public Int32 RemainOpenTime {
 get { return remainOpenTime_; }
 set { SetRemainOpenTime(value); }
 }
 public void SetRemainOpenTime(Int32 value) { 
 hasRemainOpenTime = true;
 remainOpenTime_ = value;
 }

public const int aliveTimeFieldNumber = 8;
 private bool hasAliveTime;
 private Int32 aliveTime_ = 0;
 public bool HasAliveTime {
 get { return hasAliveTime; }
 }
 public Int32 AliveTime {
 get { return aliveTime_; }
 set { SetAliveTime(value); }
 }
 public void SetAliveTime(Int32 value) { 
 hasAliveTime = true;
 aliveTime_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Id);
}
 if (HasName) {
size += pb::CodedOutputStream.ComputeStringSize(2, Name);
}
 if (HasCardid) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Cardid);
}
 if (HasCurrentHp) {
size += pb::CodedOutputStream.ComputeInt64Size(4, CurrentHp);
}
 if (HasTotalHp) {
size += pb::CodedOutputStream.ComputeInt64Size(5, TotalHp);
}
 if (HasActiveFlag) {
size += pb::CodedOutputStream.ComputeInt32Size(6, ActiveFlag);
}
 if (HasRemainOpenTime) {
size += pb::CodedOutputStream.ComputeInt32Size(7, RemainOpenTime);
}
 if (HasAliveTime) {
size += pb::CodedOutputStream.ComputeInt32Size(8, AliveTime);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasId) {
output.WriteInt32(1, Id);
}
 
if (HasName) {
output.WriteString(2, Name);
}
 
if (HasCardid) {
output.WriteInt32(3, Cardid);
}
 
if (HasCurrentHp) {
output.WriteInt64(4, CurrentHp);
}
 
if (HasTotalHp) {
output.WriteInt64(5, TotalHp);
}
 
if (HasActiveFlag) {
output.WriteInt32(6, ActiveFlag);
}
 
if (HasRemainOpenTime) {
output.WriteInt32(7, RemainOpenTime);
}
 
if (HasAliveTime) {
output.WriteInt32(8, AliveTime);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 WorldBoss _inst = (WorldBoss) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Id = input.ReadInt32();
break;
}
   case  18: {
 _inst.Name = input.ReadString();
break;
}
   case  24: {
 _inst.Cardid = input.ReadInt32();
break;
}
   case  32: {
 _inst.CurrentHp = input.ReadInt64();
break;
}
   case  40: {
 _inst.TotalHp = input.ReadInt64();
break;
}
   case  48: {
 _inst.ActiveFlag = input.ReadInt32();
break;
}
   case  56: {
 _inst.RemainOpenTime = input.ReadInt32();
break;
}
   case  64: {
 _inst.AliveTime = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasId) return false;
 if (!hasCardid) return false;
 return true;
 }

	}


[Serializable]
public class WorldBossAttInfo : PacketDistributed
{

public const int attTimesFieldNumber = 1;
 private bool hasAttTimes;
 private Int32 attTimes_ = 0;
 public bool HasAttTimes {
 get { return hasAttTimes; }
 }
 public Int32 AttTimes {
 get { return attTimes_; }
 set { SetAttTimes(value); }
 }
 public void SetAttTimes(Int32 value) { 
 hasAttTimes = true;
 attTimes_ = value;
 }

public const int currentDamageFieldNumber = 2;
 private bool hasCurrentDamage;
 private Int64 currentDamage_ = 0;
 public bool HasCurrentDamage {
 get { return hasCurrentDamage; }
 }
 public Int64 CurrentDamage {
 get { return currentDamage_; }
 set { SetCurrentDamage(value); }
 }
 public void SetCurrentDamage(Int64 value) { 
 hasCurrentDamage = true;
 currentDamage_ = value;
 }

public const int totalDamageFieldNumber = 3;
 private bool hasTotalDamage;
 private Int64 totalDamage_ = 0;
 public bool HasTotalDamage {
 get { return hasTotalDamage; }
 }
 public Int64 TotalDamage {
 get { return totalDamage_; }
 set { SetTotalDamage(value); }
 }
 public void SetTotalDamage(Int64 value) { 
 hasTotalDamage = true;
 totalDamage_ = value;
 }

public const int attRankFieldNumber = 4;
 private bool hasAttRank;
 private Int32 attRank_ = 0;
 public bool HasAttRank {
 get { return hasAttRank; }
 }
 public Int32 AttRank {
 get { return attRank_; }
 set { SetAttRank(value); }
 }
 public void SetAttRank(Int32 value) { 
 hasAttRank = true;
 attRank_ = value;
 }

public const int rankInfoFieldNumber = 5;
 private pbc::PopsicleList<WorldBossDamageRankInfo> rankInfo_ = new pbc::PopsicleList<WorldBossDamageRankInfo>();
 public scg::IList<WorldBossDamageRankInfo> rankInfoList {
 get { return pbc::Lists.AsReadOnly(rankInfo_); }
 }
 
 public int rankInfoCount {
 get { return rankInfo_.Count; }
 }
 
public WorldBossDamageRankInfo GetRankInfo(int index) {
 return rankInfo_[index];
 }
 public void AddRankInfo(WorldBossDamageRankInfo value) {
 rankInfo_.Add(value);
 }

public const int zhufuTimesFieldNumber = 6;
 private bool hasZhufuTimes;
 private Int32 zhufuTimes_ = 0;
 public bool HasZhufuTimes {
 get { return hasZhufuTimes; }
 }
 public Int32 ZhufuTimes {
 get { return zhufuTimes_; }
 set { SetZhufuTimes(value); }
 }
 public void SetZhufuTimes(Int32 value) { 
 hasZhufuTimes = true;
 zhufuTimes_ = value;
 }

public const int fuhuoTimeFieldNumber = 7;
 private bool hasFuhuoTime;
 private Int32 fuhuoTime_ = 0;
 public bool HasFuhuoTime {
 get { return hasFuhuoTime; }
 }
 public Int32 FuhuoTime {
 get { return fuhuoTime_; }
 set { SetFuhuoTime(value); }
 }
 public void SetFuhuoTime(Int32 value) { 
 hasFuhuoTime = true;
 fuhuoTime_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasAttTimes) {
size += pb::CodedOutputStream.ComputeInt32Size(1, AttTimes);
}
 if (HasCurrentDamage) {
size += pb::CodedOutputStream.ComputeInt64Size(2, CurrentDamage);
}
 if (HasTotalDamage) {
size += pb::CodedOutputStream.ComputeInt64Size(3, TotalDamage);
}
 if (HasAttRank) {
size += pb::CodedOutputStream.ComputeInt32Size(4, AttRank);
}
{
foreach (WorldBossDamageRankInfo element in rankInfoList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)5) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 if (HasZhufuTimes) {
size += pb::CodedOutputStream.ComputeInt32Size(6, ZhufuTimes);
}
 if (HasFuhuoTime) {
size += pb::CodedOutputStream.ComputeInt32Size(7, FuhuoTime);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasAttTimes) {
output.WriteInt32(1, AttTimes);
}
 
if (HasCurrentDamage) {
output.WriteInt64(2, CurrentDamage);
}
 
if (HasTotalDamage) {
output.WriteInt64(3, TotalDamage);
}
 
if (HasAttRank) {
output.WriteInt32(4, AttRank);
}

do{
foreach (WorldBossDamageRankInfo element in rankInfoList) {
output.WriteTag((int)5, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 
if (HasZhufuTimes) {
output.WriteInt32(6, ZhufuTimes);
}
 
if (HasFuhuoTime) {
output.WriteInt32(7, FuhuoTime);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 WorldBossAttInfo _inst = (WorldBossAttInfo) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.AttTimes = input.ReadInt32();
break;
}
   case  16: {
 _inst.CurrentDamage = input.ReadInt64();
break;
}
   case  24: {
 _inst.TotalDamage = input.ReadInt64();
break;
}
   case  32: {
 _inst.AttRank = input.ReadInt32();
break;
}
    case  42: {
WorldBossDamageRankInfo subBuilder =  new WorldBossDamageRankInfo();
input.ReadMessage(subBuilder);
_inst.AddRankInfo(subBuilder);
break;
}
   case  48: {
 _inst.ZhufuTimes = input.ReadInt32();
break;
}
   case  56: {
 _inst.FuhuoTime = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
 foreach (WorldBossDamageRankInfo element in rankInfoList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class WorldBossDamageRankInfo : PacketDistributed
{

public const int rankFieldNumber = 1;
 private bool hasRank;
 private Int32 rank_ = 0;
 public bool HasRank {
 get { return hasRank; }
 }
 public Int32 Rank {
 get { return rank_; }
 set { SetRank(value); }
 }
 public void SetRank(Int32 value) { 
 hasRank = true;
 rank_ = value;
 }

public const int pidFieldNumber = 2;
 private bool hasPid;
 private Int64 pid_ = 0;
 public bool HasPid {
 get { return hasPid; }
 }
 public Int64 Pid {
 get { return pid_; }
 set { SetPid(value); }
 }
 public void SetPid(Int64 value) { 
 hasPid = true;
 pid_ = value;
 }

public const int nameFieldNumber = 3;
 private bool hasName;
 private string name_ = "";
 public bool HasName {
 get { return hasName; }
 }
 public string Name {
 get { return name_; }
 set { SetName(value); }
 }
 public void SetName(string value) { 
 hasName = true;
 name_ = value;
 }

public const int totalDamageFieldNumber = 4;
 private bool hasTotalDamage;
 private Int64 totalDamage_ = 0;
 public bool HasTotalDamage {
 get { return hasTotalDamage; }
 }
 public Int64 TotalDamage {
 get { return totalDamage_; }
 set { SetTotalDamage(value); }
 }
 public void SetTotalDamage(Int64 value) { 
 hasTotalDamage = true;
 totalDamage_ = value;
 }

public const int cardidFieldNumber = 5;
 private bool hasCardid;
 private Int32 cardid_ = 0;
 public bool HasCardid {
 get { return hasCardid; }
 }
 public Int32 Cardid {
 get { return cardid_; }
 set { SetCardid(value); }
 }
 public void SetCardid(Int32 value) { 
 hasCardid = true;
 cardid_ = value;
 }

public const int powerFieldNumber = 6;
 private bool hasPower;
 private Int32 power_ = 0;
 public bool HasPower {
 get { return hasPower; }
 }
 public Int32 Power {
 get { return power_; }
 set { SetPower(value); }
 }
 public void SetPower(Int32 value) { 
 hasPower = true;
 power_ = value;
 }

public const int playerLevFieldNumber = 7;
 private bool hasPlayerLev;
 private Int32 playerLev_ = 0;
 public bool HasPlayerLev {
 get { return hasPlayerLev; }
 }
 public Int32 PlayerLev {
 get { return playerLev_; }
 set { SetPlayerLev(value); }
 }
 public void SetPlayerLev(Int32 value) { 
 hasPlayerLev = true;
 playerLev_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasRank) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Rank);
}
 if (HasPid) {
size += pb::CodedOutputStream.ComputeInt64Size(2, Pid);
}
 if (HasName) {
size += pb::CodedOutputStream.ComputeStringSize(3, Name);
}
 if (HasTotalDamage) {
size += pb::CodedOutputStream.ComputeInt64Size(4, TotalDamage);
}
 if (HasCardid) {
size += pb::CodedOutputStream.ComputeInt32Size(5, Cardid);
}
 if (HasPower) {
size += pb::CodedOutputStream.ComputeInt32Size(6, Power);
}
 if (HasPlayerLev) {
size += pb::CodedOutputStream.ComputeInt32Size(7, PlayerLev);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasRank) {
output.WriteInt32(1, Rank);
}
 
if (HasPid) {
output.WriteInt64(2, Pid);
}
 
if (HasName) {
output.WriteString(3, Name);
}
 
if (HasTotalDamage) {
output.WriteInt64(4, TotalDamage);
}
 
if (HasCardid) {
output.WriteInt32(5, Cardid);
}
 
if (HasPower) {
output.WriteInt32(6, Power);
}
 
if (HasPlayerLev) {
output.WriteInt32(7, PlayerLev);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 WorldBossDamageRankInfo _inst = (WorldBossDamageRankInfo) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Rank = input.ReadInt32();
break;
}
   case  16: {
 _inst.Pid = input.ReadInt64();
break;
}
   case  26: {
 _inst.Name = input.ReadString();
break;
}
   case  32: {
 _inst.TotalDamage = input.ReadInt64();
break;
}
   case  40: {
 _inst.Cardid = input.ReadInt32();
break;
}
   case  48: {
 _inst.Power = input.ReadInt32();
break;
}
   case  56: {
 _inst.PlayerLev = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasRank) return false;
 if (!hasPid) return false;
 return true;
 }

	}


[Serializable]
public class WorldBossKillInfo : PacketDistributed
{

public const int lastBossInfoFieldNumber = 1;
 private bool hasLastBossInfo;
 private WorldBoss lastBossInfo_ =  new WorldBoss();
 public bool HasLastBossInfo {
 get { return hasLastBossInfo; }
 }
 public WorldBoss LastBossInfo {
 get { return lastBossInfo_; }
 set { SetLastBossInfo(value); }
 }
 public void SetLastBossInfo(WorldBoss value) { 
 hasLastBossInfo = true;
 lastBossInfo_ = value;
 }

public const int lastKillerFieldNumber = 2;
 private bool hasLastKiller;
 private WorldBossDamageRankInfo lastKiller_ =  new WorldBossDamageRankInfo();
 public bool HasLastKiller {
 get { return hasLastKiller; }
 }
 public WorldBossDamageRankInfo LastKiller {
 get { return lastKiller_; }
 set { SetLastKiller(value); }
 }
 public void SetLastKiller(WorldBossDamageRankInfo value) { 
 hasLastKiller = true;
 lastKiller_ = value;
 }

public const int top3KillerFieldNumber = 3;
 private pbc::PopsicleList<WorldBossDamageRankInfo> top3Killer_ = new pbc::PopsicleList<WorldBossDamageRankInfo>();
 public scg::IList<WorldBossDamageRankInfo> top3KillerList {
 get { return pbc::Lists.AsReadOnly(top3Killer_); }
 }
 
 public int top3KillerCount {
 get { return top3Killer_.Count; }
 }
 
public WorldBossDamageRankInfo GetTop3Killer(int index) {
 return top3Killer_[index];
 }
 public void AddTop3Killer(WorldBossDamageRankInfo value) {
 top3Killer_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int subsize = LastBossInfo.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = LastKiller.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
foreach (WorldBossDamageRankInfo element in top3KillerList) {
int subsize = element.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)3) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)LastBossInfo.SerializedSize());
LastBossInfo.WriteTo(output);

}
{
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)LastKiller.SerializedSize());
LastKiller.WriteTo(output);

}

do{
foreach (WorldBossDamageRankInfo element in top3KillerList) {
output.WriteTag((int)3, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)element.SerializedSize());
element.WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 WorldBossKillInfo _inst = (WorldBossKillInfo) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
WorldBoss subBuilder =  new WorldBoss();
 input.ReadMessage(subBuilder);
_inst.LastBossInfo = subBuilder;
break;
}
    case  18: {
WorldBossDamageRankInfo subBuilder =  new WorldBossDamageRankInfo();
 input.ReadMessage(subBuilder);
_inst.LastKiller = subBuilder;
break;
}
    case  26: {
WorldBossDamageRankInfo subBuilder =  new WorldBossDamageRankInfo();
input.ReadMessage(subBuilder);
_inst.AddTop3Killer(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
   if (HasLastBossInfo) {
if (!LastBossInfo.IsInitialized()) return false;
}
  if (HasLastKiller) {
if (!LastKiller.IsInitialized()) return false;
}
foreach (WorldBossDamageRankInfo element in top3KillerList) {
if (!element.IsInitialized()) return false;
}
 return true;
 }

	}


[Serializable]
public class WuxingInfo : PacketDistributed
{

public const int idFieldNumber = 1;
 private bool hasId;
 private Int32 id_ = 0;
 public bool HasId {
 get { return hasId; }
 }
 public Int32 Id {
 get { return id_; }
 set { SetId(value); }
 }
 public void SetId(Int32 value) { 
 hasId = true;
 id_ = value;
 }

public const int levelFieldNumber = 2;
 private bool hasLevel;
 private Int32 level_ = 0;
 public bool HasLevel {
 get { return hasLevel; }
 }
 public Int32 Level {
 get { return level_; }
 set { SetLevel(value); }
 }
 public void SetLevel(Int32 value) { 
 hasLevel = true;
 level_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Id);
}
 if (HasLevel) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Level);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasId) {
output.WriteInt32(1, Id);
}
 
if (HasLevel) {
output.WriteInt32(2, Level);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 WuxingInfo _inst = (WuxingInfo) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Id = input.ReadInt32();
break;
}
   case  16: {
 _inst.Level = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasId) return false;
 if (!hasLevel) return false;
 return true;
 }

	}


[Serializable]
public class cardGuidAndTempleID : PacketDistributed
{

public const int cardGuidFieldNumber = 1;
 private bool hasCardGuid;
 private Int64 cardGuid_ = 0;
 public bool HasCardGuid {
 get { return hasCardGuid; }
 }
 public Int64 CardGuid {
 get { return cardGuid_; }
 set { SetCardGuid(value); }
 }
 public void SetCardGuid(Int64 value) { 
 hasCardGuid = true;
 cardGuid_ = value;
 }

public const int cardTempleIDFieldNumber = 2;
 private bool hasCardTempleID;
 private Int32 cardTempleID_ = 0;
 public bool HasCardTempleID {
 get { return hasCardTempleID; }
 }
 public Int32 CardTempleID {
 get { return cardTempleID_; }
 set { SetCardTempleID(value); }
 }
 public void SetCardTempleID(Int32 value) { 
 hasCardTempleID = true;
 cardTempleID_ = value;
 }

public const int whereInFieldNumber = 3;
 private bool hasWhereIn;
 private Int32 whereIn_ = 0;
 public bool HasWhereIn {
 get { return hasWhereIn; }
 }
 public Int32 WhereIn {
 get { return whereIn_; }
 set { SetWhereIn(value); }
 }
 public void SetWhereIn(Int32 value) { 
 hasWhereIn = true;
 whereIn_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasCardGuid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, CardGuid);
}
 if (HasCardTempleID) {
size += pb::CodedOutputStream.ComputeInt32Size(2, CardTempleID);
}
 if (HasWhereIn) {
size += pb::CodedOutputStream.ComputeInt32Size(3, WhereIn);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasCardGuid) {
output.WriteInt64(1, CardGuid);
}
 
if (HasCardTempleID) {
output.WriteInt32(2, CardTempleID);
}
 
if (HasWhereIn) {
output.WriteInt32(3, WhereIn);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 cardGuidAndTempleID _inst = (cardGuidAndTempleID) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.CardGuid = input.ReadInt64();
break;
}
   case  16: {
 _inst.CardTempleID = input.ReadInt32();
break;
}
   case  24: {
 _inst.WhereIn = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  return true;
 }

	}


}
