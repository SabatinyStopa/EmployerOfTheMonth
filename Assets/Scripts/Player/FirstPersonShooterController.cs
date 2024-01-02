using UnityEngine;

namespace EmployerOfTheMonth.Player
{
    public class FirstPersonShooterController : MonoBehaviour
    {
        [SerializeField] private Transform hitVfx;
        [SerializeField] private Transform gunHolder;

        public bool HasGun => gunHolder.childCount > 0;

        private void Update()
        {
            if (HasGun && Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                var hitted = Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity);

                if (hitted)
                {
                    Destroy(Instantiate(hitVfx, hit.point, Quaternion.LookRotation(hit.normal)).gameObject, 1f);

                    if (hit.transform.CompareTag("Enemy")) Destroy(hit.transform.gameObject);
                }
            }
        }

        public void Grab(Transform gun)
        {
            gun.SetParent(gunHolder);
            gun.localPosition = Vector3.zero;
            gun.localRotation = Quaternion.identity;
            gun.localScale = Vector3.one;
        }
    }
}