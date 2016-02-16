﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BC2;

public class BC2Instance : MonoBehaviour {
	public int id;
	public Inst instance;
    public List<Partition> partitions;
	public MapLoad mapLoad;
    string matrixstring;


    void Start() {
		string name = null;
		if(Util.GetField ("Name", instance) != null) {
			name = Util.GetField ("Name", instance).value;
			if (name != null) {
				transform.name = name;
			}
		}

		string type = instance.type;
		if(type == "GameSharedResources.TerrainEntityData") {
           transform.gameObject.AddComponent<TerrainEntityData>();
		}
        if(type == "Physics.HavokAsset")
        {
           transform.gameObject.AddComponent<HavokAsset>();
        }
		if (type == "Terrain.TerrainSplinePlaneData") {
			TerrainSplinePlaneData tspd = transform.gameObject.AddComponent<TerrainSplinePlaneData>();
		}
		if (type == "Terrain.TerrainSplineData") {
			transform.gameObject.AddComponent<TerrainSplineData> ();
		}
		if (type == "Terrain.TerrainSplinePointData") {
			transform.gameObject.AddComponent<TerrainSplinePointData> ();
		}	

		if (type == "GameSharedResources.MissionObjectiveEntityData") {
			transform.gameObject.AddComponent<MissionObjectiveEntityData> ();
		}
	}

    public void SetPosRot() {
        if (Util.GetComplex("Transform", instance) != null) {
            Vector3 pos = transform.position;
            Quaternion rot = transform.rotation;
            Matrix4x4 matrix = new Matrix4x4(); 
            matrix.SetTRS(pos,rot,Vector3.one);
            Matrix4x4 m = matrix;

            // m1 might be wrong. dunno
            matrixstring =
                m.m02 + "/" + m.m01 + "/" + m.m00*-1 + "/*nonzero*/" +
                m.m12 + "/" + m.m11 + "/" + m.m10 + "/*nonzero*/" +
                m.m22 + "/" + m.m21 + "/" + m.m20 * -1 + "/*nonzero*/" +
                pos.z + "/" + pos.y + "/" + pos.x + "/*nonzero*/";


            Util.GetComplex("Transform", instance).value = matrixstring;

        }
    }
}