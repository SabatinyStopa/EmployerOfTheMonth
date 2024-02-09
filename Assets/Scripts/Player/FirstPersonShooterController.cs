using EmployerOfTheMonth.Common;
using TMPro;
using UnityEngine;

namespace EmployerOfTheMonth.Player
{
    public class FirstPersonShooterController : MonoBehaviour
    {
        [SerializeField] private Transform hitVfx;
        [SerializeField] private Transform gunHolder;
        [SerializeField] private TextMeshProUGUI gunInstructionText;

        public bool HasGun => gunHolder.childCount > 0;

        public bool IsShowingGun { get => isShowingGun; }

        private bool isShowingGun;

        private string gunHolsterInstruction = "1 to holster gun";
        private string gunUnholsterInstruction = "1 to pick gun";


        private void Start() => gunInstructionText.enabled = false;


        private void Update()
        {
            if (HasGun && Input.GetKeyDown(KeyCode.Alpha1))
            {
                isShowingGun = !isShowingGun;

                gunHolder.gameObject.SetActive(isShowingGun);

                gunInstructionText.text = isShowingGun ? gunHolsterInstruction : gunUnholsterInstruction;
            }

            if (isShowingGun && Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                var hitted = Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity);

                if (hitted)
                {
                    Destroy(Instantiate(hitVfx, hit.point, Quaternion.LookRotation(Camera.main.transform.position - hit.point)).gameObject, 1f);

                    if (hit.transform.CompareTag("Enemy"))
                    {
                        hit.transform.GetComponent<Life>().TakeDamage(50);
                    }
                }
            }
        }

        public void Grab(Transform gun)
        {
            gun.SetParent(gunHolder);
            gun.localPosition = Vector3.zero;
            gun.localRotation = Quaternion.identity;
            gun.localScale = Vector3.one;

            isShowingGun = true;
            gunInstructionText.enabled = true;
            gunInstructionText.text = isShowingGun ? gunHolsterInstruction : gunUnholsterInstruction;
        }
    }
}