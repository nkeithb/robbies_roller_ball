using UnityEngine;
using System.Collections;

public class AnitPlayerController : MonoBehaviour {

    [Tooltip("AntiPlayer will follow this target")]
    public Transform target;
    [Tooltip("AntiPlayer movement speed")]
    public float moveSpeed = 4f;
    [Tooltip("AntiPlayer rotation speed")]
    public float rotationSpeed = 3f;
    
    void Start()
    {
        //if (Application.loadedLevelName != "Developer_Test_Zone.unity")
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            dir.z = 0.0f;
            if (dir != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.FromToRotation(Vector3.right, dir),
                    rotationSpeed * Time.deltaTime);
            transform.position += (target.position - transform.position).normalized
           * moveSpeed * Time.deltaTime;
        }
    }
    void OnTriggerEnter(Collider other)
    {
            switch (other.tag)
            {
                case "Hammer":
                    // test if hit with this print(string.Format("hit detected"));
                    int signOne = (Random.Range(0, 2) * 2) - 1;
                    int signTwo = (Random.Range(0, 2) * 2) - 1;
                    float dirX = Random.Range(10000f, 25000f) * signOne;
                    float dirZ = Random.Range(10000f, 25000f) * signTwo;
                    GetComponent<Rigidbody>().AddForce(new Vector3(dirX, 3000.0f, dirZ));
                    SoundManager.instance.RandomizeSfx(PlayerController.instance.hammerSounds);
<<<<<<< HEAD

                    PlayerController.instance.HammerAPScore(); 
                    PlayerController.instance.HammerAPScore();
                    Invoke("DestroyThisAntiPlayer", 3.0f);
=======
                    PlayerController.instance.HammerAPScore();
                    Invoke("DestroyThisAntiPlayer", 3.0f);
                    break;
                case "DeathZone":
                    DestroyThisAntiPlayer();
>>>>>>> caa6dba872fec467d6ff2c214aa74a44438e4939
                    break;
            }
        }

        private void DestroyThisAntiPlayer()
    {
        Destroy (gameObject);
    }
}
