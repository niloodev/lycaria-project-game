// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.21
// 

using Colyseus.Schema;

public partial class Character : Schema {
	[Type(0, "string")]
	public string id = default(string);

	[Type(1, "string")]
	public string entityId = default(string);

	[Type(2, "string")]
	public string mainClass = default(string);

	[Type(3, "string")]
	public string subClass = default(string);

	[Type(4, "array", typeof(ArraySchema<string>), "string")]
	public ArraySchema<string> skins = new ArraySchema<string>();

	[Type(5, "int16")]
	public short level = default(short);

	[Type(6, "string")]
	public string skin = default(string);

	[Type(7, "string")]
	public string playerId = default(string);

	[Type(8, "string")]
	public string team = default(string);

	[Type(9, "array", typeof(ArraySchema<Skill>))]
	public ArraySchema<Skill> skills = new ArraySchema<Skill>();

	[Type(10, "ref", typeof(Vector2_))]
	public Vector2_ vector = new Vector2_();

	[Type(11, "ref", typeof(Vector2_))]
	public Vector2_ root = new Vector2_();

	[Type(12, "ref", typeof(Vector2_))]
	public Vector2_ goal = new Vector2_();

	[Type(13, "ref", typeof(CharStatus_), "CharStatus")]
	public CharStatus_ CharStatus = new CharStatus_();
}

