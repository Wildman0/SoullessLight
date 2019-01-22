using UnityEngine;
using NDA.FloatUtil;

/// <summary>
/// Contains the methods for determining the state of the passive bools used in the CharacterController
/// </summary>
public static class PassiveBools
{
    /// <summary>
    /// Returns true if the character is grounded, else returns false
    /// </summary>
    /// <param name="playerController">The current PlayerController</param>
    /// <returns></returns>
    public static bool IsGrounded(PlayerController playerController)
    {
        Ray ray = new Ray(playerController.gameObject.transform.position, Vector3.down);
        RaycastHit hit = new RaycastHit();

        return Physics.Raycast(ray, out hit, 0.51f);
    }

    /// <summary>
    /// Returns true if the player has moved since the last frame, else returns false
    /// </summary>
    /// <param name="playerController">The current PlayerController</param>
    /// <returns></returns>
    public static bool IsMoving(PlayerController playerController)
    {
        return !FloatMath.IsZero(playerController.totalMoveSpeedX) ||
               !FloatMath.IsZero(playerController.totalMoveSpeedZ);
    }

    //TODO: REWORK WITHOUT POSITIONLASTFRAME
    /// <summary>
    /// Returns true if the character is higher this frame than they were the last,
    /// Else returns false
    /// </summary>
    /// <param name="playerController">The current PlayerController</param>
    /// <param name="positionLastFrame">The position of the PlayerController last frame</param>
    /// <returns></returns>
    public static bool IsAscending(PlayerController playerController, 
                                   Vector3 positionLastFrame)
    {
        Vector3 positionThisFrame = playerController.gameObject.transform.position;

        return (positionThisFrame.y > positionLastFrame.y) ? true : false;
    }

    //TODO: REWORK WITHOUT POSITIONLASTFRAME
    /// <summary>
    /// Returns true if the character is lower this frame than they were the last,
    /// Else returns false
    /// </summary>
    /// <param name="playerController">The current PlayerController</param>
    /// <param name="positionLastFrame">The position of the PlayerController last frame</param>
    /// <returns></returns>
    public static bool IsDescending(PlayerController playerController, 
                                    Vector3 positionLastFrame)
    {
        Vector3 positionThisFrame = playerController.gameObject.transform.position;
        return (positionLastFrame.y > positionThisFrame.y) ? true : false;
    }

    //TODO: THIS WILL NEED TO BE REWORKED IN THE FUTURE IN ORDER TO ALLOW FOR HEAVY ATTACKING TO
    //TODO: ALSO HAVE THE CORRECT EFFECT
    //Returns whether or not the player is currently attacking
    public static bool IsAttacking(PlayerController playerController)
    {
        return (playerController.playerAnim.anim.GetBool("IsWeakAtt"));
    }
}