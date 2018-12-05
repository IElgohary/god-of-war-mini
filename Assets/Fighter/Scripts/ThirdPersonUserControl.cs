using System;
using UnityEngine;


namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        public LayerMask layerMask;
        Vector3 currLooktarget = Vector3.zero;
        private Animator anim;
        private float damage = 1.0f;
        private float moveFactor = 1.0f;
        private PlayerHealth healthScript;
        private int health;
        private int XP = 0;
        private int newXP;
        private int PrevXP = 250;
        private int level = 1;
        private int rageMeter = 0;
        public int skillPoints = 0;
        private bool rageBool = false;
        private bool Shield = false;



        private void Start()
        {
            healthScript = gameObject.GetComponent<PlayerHealth>();
            health = healthScript.currentHealth;
            anim = GetComponent<Animator>();
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }
        private void updateRage()
        {
            rageMeter += 10;
            if (rageMeter >= 100)
            {
                rageMeter = 100;
                rageBool = true;

            }

        }
        private void updateXP()
        {
            XP = XP + 50;
            if (XP >= 2 * PrevXP)
            {
                level++;
                skillPoints++;
                PrevXP *= 2;
            }

        }
        private void PickSkills(int x)
        {
            switch (x)
            {
                case 1: moveFactor += 0.1f; break;
                case 2: damage += 0.1f; break;
                case 3: health += 10; healthScript.currentHealth = health; break;
            }
        }
        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = Input.GetButtonDown("Jump");
            }
            if (Input.GetMouseButtonDown(0)) {
                anim.Play("double_chop");


            }
            if (Input.GetMouseButtonDown(1)) {
                anim.Play("Standing Melee Kick");

            }

            Shield = Input.GetKeyDown(KeyCode.LeftControl);
            anim.SetBool("Shield", Shield);



            if (Input.GetKeyDown(KeyCode.R)){
                if (rageBool)
                {
                    damage *= 2f;
                    rageMeter = 0;
                    rageBool = false;
                }
            }

        }
        private void FixedUpdate()
        {
            //CAMERA ACCORDING TO MOUSE

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin,ray.direction *300,Color.blue); 
            if(Physics.Raycast(ray,out hit,300,layerMask,QueryTriggerInteraction.Ignore))
            {

			if(hit.point != currLooktarget){
				currLooktarget= hit.point;
			}

			Vector3 targetPosition = new Vector3(hit.point.x,transform.position.y,hit.point.z);
			Quaternion rotation = Quaternion.LookRotation(targetPosition-transform.position);
			transform.rotation = Quaternion.Lerp(transform.rotation,rotation,Time.deltaTime);

		}

            // read inputs

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 3.0f;
#endif
            
            // pass all parameters to the character control script
            m_Character.Move(0.5f * m_Move*moveFactor, crouch, m_Jump);
            m_Jump = false;
        }
    }
}

