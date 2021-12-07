// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.21
// 

using Colyseus.Schema;

public partial class CharStatus_ : Schema {
	[Type(0, "number")]
	public float mvp = default(float);

	[Type(1, "map", typeof(MapSchema<float>), "number")]
	public MapSchema<float> mvpModifiers = new MapSchema<float>();

	[Type(2, "map", typeof(MapSchema<double>), "float64")]
	public MapSchema<double> mvpModifiersX = new MapSchema<double>();

	[Type(3, "number")]
	public float maxHp = default(float);

	[Type(4, "number")]
	public float hp = default(float);

	[Type(5, "map", typeof(MapSchema<float>), "number")]
	public MapSchema<float> maxHpModifiers = new MapSchema<float>();

	[Type(6, "map", typeof(MapSchema<double>), "float64")]
	public MapSchema<double> maxHpModifiersX = new MapSchema<double>();

	[Type(7, "number")]
	public float mana = default(float);

	[Type(8, "number")]
	public float maxMana = default(float);

	[Type(9, "map", typeof(MapSchema<float>), "number")]
	public MapSchema<float> maxManaModifiers = new MapSchema<float>();

	[Type(10, "map", typeof(MapSchema<double>), "float64")]
	public MapSchema<double> maxManaModifiersX = new MapSchema<double>();

	[Type(11, "number")]
	public float ap = default(float);

	[Type(12, "map", typeof(MapSchema<float>), "number")]
	public MapSchema<float> apModifiers = new MapSchema<float>();

	[Type(13, "map", typeof(MapSchema<double>), "float64")]
	public MapSchema<double> apModifiersX = new MapSchema<double>();

	[Type(14, "number")]
	public float ad = default(float);

	[Type(15, "map", typeof(MapSchema<float>), "number")]
	public MapSchema<float> adModifiers = new MapSchema<float>();

	[Type(16, "map", typeof(MapSchema<double>), "float64")]
	public MapSchema<double> adModifiersX = new MapSchema<double>();

	[Type(17, "number")]
	public float mr = default(float);

	[Type(18, "map", typeof(MapSchema<float>), "number")]
	public MapSchema<float> mrModifiers = new MapSchema<float>();

	[Type(19, "map", typeof(MapSchema<double>), "float64")]
	public MapSchema<double> mrModifiersX = new MapSchema<double>();

	[Type(20, "number")]
	public float mrPainPlane = default(float);

	[Type(21, "map", typeof(MapSchema<float>), "number")]
	public MapSchema<float> mrPainPlaneModifiers = new MapSchema<float>();

	[Type(22, "map", typeof(MapSchema<double>), "float64")]
	public MapSchema<double> mrPainPlaneModifiersX = new MapSchema<double>();

	[Type(23, "number")]
	public float mrPainPorc = default(float);

	[Type(24, "map", typeof(MapSchema<float>), "number")]
	public MapSchema<float> mrPainPorcModifiers = new MapSchema<float>();

	[Type(25, "map", typeof(MapSchema<double>), "float64")]
	public MapSchema<double> mrPainPorcModifiersX = new MapSchema<double>();

	[Type(26, "number")]
	public float ar = default(float);

	[Type(27, "map", typeof(MapSchema<float>), "number")]
	public MapSchema<float> arModifiers = new MapSchema<float>();

	[Type(28, "map", typeof(MapSchema<double>), "float64")]
	public MapSchema<double> arModifiersX = new MapSchema<double>();

	[Type(29, "number")]
	public float arPainPlane = default(float);

	[Type(30, "map", typeof(MapSchema<float>), "number")]
	public MapSchema<float> arPainPlaneModifiers = new MapSchema<float>();

	[Type(31, "map", typeof(MapSchema<double>), "float64")]
	public MapSchema<double> arPainPlaneModifiersX = new MapSchema<double>();

	[Type(32, "number")]
	public float arPainPorc = default(float);

	[Type(33, "map", typeof(MapSchema<float>), "number")]
	public MapSchema<float> arPainPorcModifiers = new MapSchema<float>();

	[Type(34, "map", typeof(MapSchema<double>), "float64")]
	public MapSchema<double> arPainPorcModifiersX = new MapSchema<double>();

	[Type(35, "number")]
	public float vamp = default(float);

	[Type(36, "map", typeof(MapSchema<float>), "number")]
	public MapSchema<float> vampModifiers = new MapSchema<float>();

	[Type(37, "map", typeof(MapSchema<double>), "float64")]
	public MapSchema<double> vampModifiersX = new MapSchema<double>();

	[Type(38, "number")]
	public float vitality = default(float);

	[Type(39, "map", typeof(MapSchema<float>), "number")]
	public MapSchema<float> vitalityModifiers = new MapSchema<float>();

	[Type(40, "map", typeof(MapSchema<double>), "float64")]
	public MapSchema<double> vitalityModifiersX = new MapSchema<double>();
}

