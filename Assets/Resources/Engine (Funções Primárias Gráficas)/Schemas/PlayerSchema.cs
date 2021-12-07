// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.21
// 

using Colyseus.Schema;

public partial class PlayerSchema : Schema {
	[Type(0, "string")]
	public string characterAttached = default(string);

	[Type(1, "boolean")]
	public bool online = default(bool);

	[Type(2, "boolean")]
	public bool ready = default(bool);

	[Type(3, "string")]
	public string id = default(string);

	[Type(4, "string")]
	public string nickName = default(string);

	[Type(5, "float64")]
	public double gold = default(double);

	[Type(6, "string")]
	public string team = default(string);
}

