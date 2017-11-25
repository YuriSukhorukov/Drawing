using UnityEngine;

public class UIScrollViewCustom : UIScrollView
{	
	public Vector3 Relative;

	public bool IsLeftScrollLocked = false;
	public bool IsRightScrollLocked = false;
	public bool IsDownScrollLocked = false;
	public bool IsUpScrollLocked = false;

	public void LockLeftScroll()
	{
		IsLeftScrollLocked = true;
		mScroll = 0f;
		mMomentum = Vector3.zero;
		Press(false);
	}

	public void UnlockLeftScroll()
	{
		IsLeftScrollLocked = false;
	}

	public void LockRightScroll()
	{
		IsRightScrollLocked = true;
		mScroll = 0f;
		mMomentum = Vector3.zero;
		Press(false);
	}

	public void UnlockRightScroll()
	{
		IsRightScrollLocked = false;
	}

	public void LockDownScroll()
	{
		IsDownScrollLocked = true;
		mScroll = 0f;
		mMomentum = Vector3.zero;
		Press(false);
	}
	
	public void UnlockDownScroll()
	{
		IsDownScrollLocked = false;
	}

	public void LockUpScroll()
	{
		IsUpScrollLocked = true;
		mScroll = 0f;
		mMomentum = Vector3.zero;
		Press(false);
	}

	public void UnlockUpScroll()
	{
		IsUpScrollLocked = false;
	}

	public override void MoveRelative (Vector3 relative)
	{
		Relative = relative;
		
		if (movement == Movement.Horizontal)
		{
			if (relative.x < -0.75 && IsRightScrollLocked)
			{
				relative.x = -0.75f;
				relative = Vector3.Lerp(Relative, Vector3.zero, 0.5f * Time.deltaTime);
			}
			if (relative.x > 0.75 && IsLeftScrollLocked)
			{
				relative.x = 0.75f;
				relative = Vector3.Lerp(relative, Vector3.zero, 0.5f * Time.deltaTime);
			}
			
			if (IsRightScrollLocked)
			{
				if (relative.x < 0)
				{
					mPressed = false;
				}
			}
			if (IsLeftScrollLocked)
			{
				if (relative.x > 0)
				{
					mPressed = false;
				}
			}
		}

		if (movement == Movement.Vertical)
		{
			if (relative.y > 0.75 && IsDownScrollLocked)
			{
				relative.y = 0.75f;
				relative = Vector3.Lerp(relative, Vector3.zero, 0.5f * Time.deltaTime);
			}
			if (relative.y < -0.75 && IsUpScrollLocked)
			{
				relative.y = -0.75f;
				relative = Vector3.Lerp(relative, Vector3.zero, 0.5f * Time.deltaTime);
			}
			
			if (IsDownScrollLocked)
			{
				if (relative.y > 0)
				{
					mPressed = false;
				}
			}
			if (IsUpScrollLocked)
			{
				if (relative.y < 0)
				{
					mPressed = false;
				}
			}
		}

		mTrans.localPosition += relative;
		Vector2 co = mPanel.clipOffset;
		co.x -= relative.x;
		co.y -= relative.y;
		mPanel.clipOffset = co;

		// Update the scroll bars
		UpdateScrollbars(false);
	}
}
