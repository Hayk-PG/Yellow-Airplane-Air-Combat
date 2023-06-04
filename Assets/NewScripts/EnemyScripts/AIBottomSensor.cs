﻿
public class AIBottomSensor : BaseAISensorManager 
{
    protected override void DetectCollision()
    {
        // No AI movement manager is assigned, so we cannot proceed.
        if (IsAIMovementManagerNull)
        {
            return;
        }

        // Notify the AI movement manager about the top collision.
        _aiMovementManager.DetectBottomCollision(_aiMovementManager.CurrentPosition);
    }
} 
