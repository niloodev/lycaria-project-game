// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.21
// 

using Colyseus.Schema;

public partial class State : Schema {
	[Type(0, "boolean")]
	public bool loading = default(bool);

	[Type(1, "map", typeof(MapSchema<PlayerSchema>))]
	public MapSchema<PlayerSchema> player = new MapSchema<PlayerSchema>();

	[Type(2, "map", typeof(MapSchema<Character>))]
	public MapSchema<Character> entities = new MapSchema<Character>();

	[Type(3, "map", typeof(MapSchema<Element_>))]
	public MapSchema<Element_> elements = new MapSchema<Element_>();

	[Type(4, "int32")]
	public int roundCount = default(int);

	[Type(5, "array", typeof(ArraySchema<string>), "string")]
	public ArraySchema<string> roundPriorityList = new ArraySchema<string>();

	[Type(6, "string")]
	public string turnPriority = default(string);

	[Type(7, "array", typeof(ArraySchema<string>), "string")]
	public ArraySchema<string> turnPriorityHistory = new ArraySchema<string>();

	[Type(8, "int32")]
	public int turnCount = default(int);

	
}

