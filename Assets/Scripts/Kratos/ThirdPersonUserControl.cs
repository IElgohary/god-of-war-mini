using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        public Slider expSlider;
        public Slider rageSlider;
        public TMPro.TextMeshProUGUI lvlUI;
        public TMPro.TextMeshProUGUI spUI;
        public TMPro.TextMeshProUGUI spMenu;

        public Button upgradeUI;
        public Button moveUpgrade;
        public Button attackUpgrade;
        public Button healthUpgrade;



        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        public LayerMask layerMask;
        Vector3 currLooktarget = Vector3.zero;
        private Animator anim;
        private float damageFactor = 1.0f;
        private float moveFactor = 1.0f;
        private PlayerHealth healthScript;
        public int health;
        public int XP = 0;
        public int newXP;
        public int PrevXP = 250;
        public int level = 1;
        public int rageMeter = 0;
        public int skillPoints = 0;
        public bool rageBool = false;
        public bool Shield = false;
        public int jumpState;
        private BoxCollider[] weaponColliders;
        public bool lightAttack;
        public bool heavyAttack;

        public int GetDamage(){
            float damage = 0;
            if (heavyAttack) damage = 30;
            if (lightAttack) damage = 10;
            damage *= damageFactor;
            if (rageBool) damage *= 2;
            return (int) damage;
        }

        private void Start()
        {
            healthScript = gameObject.GetComponent<PlayerHealth>();
            health = healthScript.currentHealth;
            weaponColliders = GetComponentsInChildren<BoxCollider>();
            anim = GetComponent<Animator>();
            skillPoints = 5;
            rageSlider.value = rageMeter;
            if (upgradeUI != null){
                upgradeUI.enabled = false;
            }
            if(lvlUI != null)
                lvlUI.text = "Lvl   " + level;
            if(spUI != null)
                spUI.text = "SP   " + skillPoints;
            if(spMenu != null)
                spMenu.text = "SP   " + skillPoints;

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


        public void updateRage()
        {
            if (!rageBool)
            {
                rageMeter += 10;
                if (rageMeter >= 100)
                {
                    rageMeter = 100;
                }
                rageSlider.value = rageMeter;
            }
        }


        public void updateXP()
        {
            XP = XP + 50;
            if (XP >= 2 * PrevXP)
            {     
                level++;
                skillPoints++;
                PrevXP *= 2;
                expSlider.value = (int) ((XP - PrevXP) / (PrevXP * 2));
                lvlUI.text = "Lvl   " + level;
                spUI.text = "SP   " + skillPoints;
                spMenu.text = "SP   " + skillPoints;
                enableUpgrades();
            }
        }

        private void enableUpgrades() {
            upgradeUI.gameObject.SetActive(true);
            moveUpgrade.gameObject.SetActive(true);
            healthUpgrade.gameObject.SetActive(true);
            attackUpgrade.gameObject.SetActive(true);

        }

        private void disableUpgrades() {
            upgradeUI.gameObject.SetActive(false);
            moveUpgrade.gameObject.SetActive(false);
            healthUpgrade.gameObject.SetActive(false);
            attackUpgrade.gameObject.SetActive(false);
        }

        public void PickSkills(int x)
        {
            switch (x)
            {
                case 1: moveFactor += 0.1f; break;
                case 2: damageFactor += 0.1f; break;
                case 3: healthScript.maxHealth += 10; break;
            }

            skillPoints -= 1;
            spUI.text = "SP     " + skillPoints;
            spMenu.text = "SP     " + skillPoints;
            if (skillPoints == 0)
                disableUpgrades();
        }

        private void LightAttackBegin()
        {
            foreach (var weapon in weaponColliders)
            {
                weapon.enabled = true;
                lightAttack = true;
            }
        }

        private void LightAttackEnd()
        {
            foreach (var weapon in weaponColliders)
            {
                weapon.enabled = false;
                lightAttack = false;
            }
        }

        private void PlayerBeginAttack()
        {
            foreach (var weapon in weaponColliders)
            {
                weapon.enabled = true;
                heavyAttack = true;

            }
        }
        private void PlayerEndAttack()
        {
            foreach (var weapon in weaponColliders)
            {
                weapon.enabled = false;
                heavyAttack = false;

            }
        }
        private void Update()
        {
            if (!GameManager.instance.gameOver)
            {   
                if ((Input.GetKeyDown(KeyCode.W)|Input.GetKeyDown(KeyCode.A)|
                Input.GetKeyDown(KeyCode.D)|Input.GetKeyDown(KeyCode.S))){
                GetComponents<AudioSource>()[4].Play();

                }  
                if ((Input.GetKeyUp(KeyCode.W)|Input.GetKeyUp(KeyCode.A)|
                Input.GetKeyUp(KeyCode.D)|Input.GetKeyUp(KeyCode.S))){
                GetComponents<AudioSource>()[4].Stop();

                }   



                rageSlider.value = ((float)rageMeter) / 100;
                expSlider.value = ((float)XP) / (2*PrevXP);
                if (!m_Jump)
                {
                    m_Jump = Input.GetButtonDown("Jump");
                }

                if (m_Jump)
                    jumpState++;

                if (m_Character.m_IsGrounded)
                    jumpState = 0;



                if (Input.GetMouseButtonDown(0))
                {
                   anim.Play("Attack1");
                }

                if (Input.GetMouseButtonDown(1))
                {
                    anim.Play("Dual Weapon Combo");
                }

                if (Input.GetKeyDown(KeyCode.R))
                {

                    //Debug.Log("Activated rage");
                    GetComponents<AudioSource>()[2].Play();
                    if (rageMeter == 100)
                    {
                        rageBool = true; 
                        StartCoroutine(reduceRage());
                        
                    }
                }

            }
        }

        IEnumerator reduceRage()
        {
            while (rageMeter > 0)
            {
                
            rageMeter -= 1;
            Debug.Log(rageMeter);
            rageSlider.value = rageMeter;
            yield return new WaitForSeconds(0.05f);

            }
            rageBool = false;

        }
        

        private void FixedUpdate()
        {
            //CAMERA ACCORDING TO MOUSE
            if (!GameManager.instance.gameOver)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 300, Color.blue);
                if (Physics.Raycast(ray, out hit, 300, layerMask, QueryTriggerInteraction.Ignore))
                {

                    if (hit.point != currLooktarget)
                    {
                        currLooktarget = hit.point;
                    }

                    Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                    Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime);

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
                    m_Move = v * m_CamForward + h * m_Cam.right;
                }
                else
                {
                    // we use world-relative directions in the case of no main camera
                    m_Move = v * Vector3.forward + h * Vector3.right;
                }
#if !MOBILE_INPUT
                // walk speed multiplier
                if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 3.0f;
#endif

                // pass all parameters to the character control script
                m_Character.Move(0.5f * m_Move * moveFactor, crouch, m_Jump, jumpState);
                m_Jump = false;
            }
        }
    }
}
