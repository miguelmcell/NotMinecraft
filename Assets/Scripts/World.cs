using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

	public static World currentWorld;
	public int chunkWidth = 16, chunkHeight = 50, seed = 0;
	public float viewRange = 65;
    public Transform playerPos;
    public Chunk chunkFab;
    
	void Awake() {
        //Cursor.lockState = CursorLockMode.Locked;
		currentWorld = this;
		if(seed == 0)
			seed = Random.Range(0, int.MaxValue);
    }

	void Update() {
		for(float x = playerPos.position.x - viewRange; x < playerPos.position.x + viewRange; x += chunkWidth) {
			for(float z = playerPos.position.z - viewRange; z < playerPos.position.z + viewRange; z += chunkWidth) {
				Vector3 pos = new Vector3(x, 0, z);
				pos.x = Mathf.Floor(pos.x / (float)chunkWidth) * chunkWidth;
				pos.z = Mathf.Floor(pos.z / (float)chunkWidth) * chunkWidth;
				Chunk chunk = Chunk.FindChunk(pos);
				if(chunk != null)
					continue;
				if(Vector3.Distance(pos, playerPos.position) < viewRange) {
					chunk = (Chunk)Instantiate(chunkFab, pos, Quaternion.identity);
				}
			}
		}
        for (int a = 0; a < Chunk.chunks.Count; a++){
            if (Vector3.Distance(playerPos.position - Vector3.up * playerPos.position.y, Chunk.chunks[a].transform.position) > viewRange){
                DestroyImmediate((Object)Chunk.chunks[a].GetComponent<MeshFilter>().sharedMesh);
                Destroy(Chunk.chunks[a].GetComponent<MeshRenderer>().material, .1f);
                Destroy(Chunk.chunks[a].gameObject, .2f);
                Chunk.chunks.Remove(Chunk.chunks[a]);
            }
        }
    }
}
