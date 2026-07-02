using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] float thrustStrength=100f  ;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] InputAction rotation;
    [SerializeField] AudioClip MainEngine;
    [SerializeField] ParticleSystem mainEngineparticles;
    [SerializeField] ParticleSystem LeftEngineparticles;
    [SerializeField] ParticleSystem RightEngineparticles;



    AudioSource audioSource;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource=rb.GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }
    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            startThrusting();
        }
        else
        {
            stopThrusting();
        }
    }
    void startThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(MainEngine);
        }
        if (!mainEngineparticles.isPlaying)
        {
            mainEngineparticles.Play();
        }
    }
    void stopThrusting()
    {
        audioSource.Stop();
        mainEngineparticles.Stop();
    }
    private void ProcessRotation()
    {
        float RotationInput = rotation.ReadValue<float>();
        if(RotationInput < 0)
        {
            RotateRight();
        }
        else if (RotationInput > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotate();
        }
    }

    private void StopRotate()
    {
        LeftEngineparticles.Stop();
        RightEngineparticles.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(-rotationStrength);
        if (!LeftEngineparticles.isPlaying)
        {
            RightEngineparticles.Stop();
            LeftEngineparticles.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(rotationStrength);
        if (!RightEngineparticles.isPlaying)
        {
            LeftEngineparticles.Stop();
            RightEngineparticles.Play();

        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;  
    }

}
