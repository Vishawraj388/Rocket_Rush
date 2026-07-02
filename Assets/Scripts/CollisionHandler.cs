using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float Leveldelay = 2f;
    [SerializeField] AudioClip Crash;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem succesparticles;
    [SerializeField] ParticleSystem crashparticles;
    [SerializeField] GameObject winCanvas;

    AudioSource audioSource;

    bool iscontrollable = true;
    bool iscollidable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }
    private void Update()
    {
        //RespondToDebugKey();   
    }
    //void RespondToDebugKey()
    //{
    //    if (Keyboard.current.lKey.wasPressedThisFrame)
    //    {
    //        LoadNextScence();
    //    }
    //    else if (Keyboard.current.cKey.wasPressedThisFrame)
    //    {
    //        iscollidable = !iscollidable;   
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (!iscontrollable || !iscollidable) { return; }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Everything is good !");
                break;
            case "Fuel":
                Debug.Log("Fuel loaded");
                break;
            case "Finish":
                StartSucessScence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }
    
    void StartSucessScence()
    {
        iscontrollable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        succesparticles.Play();
        GetComponent<Movement>().enabled = false;
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        Invoke("LoadNextScence",Leveldelay);
    }
    void StartCrashSequence()
    { 
        iscontrollable=false;
        audioSource.Stop();
        audioSource.PlayOneShot(Crash);
        crashparticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", Leveldelay);

    }
    void LoadNextScence()
    {
        int currScence = SceneManager.GetActiveScene().buildIndex;

        if (currScence == 2)
        {
            winCanvas.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(currScence + 1);
        }
    }
    void ReloadLevel()
    {
        int currScence = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currScence);
    }
}
