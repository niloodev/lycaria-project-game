// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.21
// 

using Colyseus.Schema;

public partial class Element_ : Schema
{
	[Type(0, "string")]
	public string id = default(string);

	[Type(1, "string")]
	public string elementId = default(string);

	[Type(2, "ref", typeof(Vector2_))]
	public Vector2_ vector = new Vector2_();

	[Type(3, "ref", typeof(Vector2_))]
	public Vector2_ root = new Vector2_();

	[Type(4, "ref", typeof(Vector2_))]
	public Vector2_ goal = new Vector2_();
}

