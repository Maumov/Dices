using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DiceController;

public class DicePhysics : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [Header( "Roll Force" )]
    [SerializeField] float forceAmount;
    [SerializeField] ForceMode forceMode;
    [SerializeField] float torqueAmount;
    [SerializeField] ForceMode torqueMode;

    [Header( "Unstuck ")]
    [SerializeField] float forceAmountUnstuck;
    [SerializeField] ForceMode forceModeUnstuck;
    [SerializeField] float torqueAmountUnstuck;
    [SerializeField] ForceMode torqueModeUnstuck;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void EnablePhysics()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    public void DisablePhysics()
    {
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    public void SendRolling()
    {
        timeNotRolling = 0f;
        rb.isKinematic = false;
        rb.useGravity = true;
        Vector3 force = Vector3.forward * forceAmount;
        Vector3 torque = Vector3.one * torqueAmount;
        rb.AddForce( force, forceMode );
        rb.AddTorque( torque, torqueMode );
    }
    public void Unstuck()
    {
        timeNotRolling = 0f;
        Vector3 force = Vector3.up * forceAmountUnstuck;
        Vector3 torque = Vector3.one * torqueAmountUnstuck;
        rb.AddForce( force, forceModeUnstuck );
        rb.AddTorque( torque, torqueModeUnstuck );
    }
    [Header( "Roll" )]
    [SerializeField] float RollingMinimumRotation = 0f;
    [SerializeField] float timeNotRolling = 0f;
    [SerializeField] float timePassToFinishRolling = 0.5f;

    public bool IsStillRolling()
    {
        if ( rb.angularVelocity.magnitude <= RollingMinimumRotation )
        {
            timeNotRolling += Time.deltaTime;
            if ( timeNotRolling >= timePassToFinishRolling )
            {
                rb.angularVelocity = Vector3.zero;
                return false;
            }
        }
        else
        {
            timeNotRolling = 0f;
        }
        return true;
    }

}
