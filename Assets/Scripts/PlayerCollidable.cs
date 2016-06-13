using UnityEngine;
using System.Collections;

public class PlayerCollidable : MonoBehaviour {

    public bool collisionResult;
    public int collisionReturn;

    public int HitByPlayer()
    {
        this.gameObject.SetActive(collisionResult);
        return this.collisionReturn;
    }
}
