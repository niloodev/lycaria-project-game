// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.21
// 

using Colyseus.Schema;

public partial class Skill : Schema {
	[Type(0, "array", typeof(ArraySchema<bool>), "boolean")]
	public ArraySchema<bool> _booleans = new ArraySchema<bool>();

	[Type(1, "array", typeof(ArraySchema<float>), "float32")]
	public ArraySchema<float> _numbers = new ArraySchema<float>();

	[Type(2, "array", typeof(ArraySchema<string>), "string")]
	public ArraySchema<string> _strings = new ArraySchema<string>();

	[Type(3, "map", typeof(MapSchema<bool>), "boolean")]
	public MapSchema<bool> blocked = new MapSchema<bool>();

	[Type(4, "boolean")]
	public bool enabled = default(bool);

	[Type(5, "number")]
	public float cdr = default(float);

	[Type(6, "string")]
	public string _icon = default(string);

	[Type(7, "string")]
	public string _name = default(string);

	[Type(8, "string")]
	public string _desc = default(string);

	[Type(9, "string")]
	public string id = default(string);
}

