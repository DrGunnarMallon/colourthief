using UnityEngine;

public class ColorThief : MonoBehaviour
{
    [Header("Transfer Cooldown")]
    [SerializeField] private float transferCooldown = 3f;
    private float lastTransferTime = -999f;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private bool CanTransfer()
    {
        return (Time.time - lastTransferTime >= transferCooldown);
    }

    private void RecordTransfer()
    {
        lastTransferTime = Time.time;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!CanTransfer()) return;

        if (other.gameObject.CompareTag("Container"))
        {
            PlayerController.Instance.Stop();

            ContainerController containerController = other.gameObject.GetComponent<ContainerController>();
            if (containerController == null) return;

            if (PlayerController.Instance.HasColor())
            {
                containerController.ReceiveColor(PlayerController.Instance.DepositColor());
            }
            else
            {
                ColorData tempColor = containerController.ReturnColor();
                if (tempColor != null)
                {
                    PlayerController.Instance.BorrowColor(tempColor);
                }
            }

            RecordTransfer();
        }

        PlayerController.Instance.Stop();
    }
}