using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUIDisplay : UIDisplay
{
    public Text DateText;

    public override void SetInteractive(bool interactive)
    {
    }

    public override void SetVisible(bool visible)
    {
        // TODO: hide self
    }

    protected override void initSliderUI()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(SystemSlider != null)
        {
            // set date text to days and weeks
            int totDays = Mathf.RoundToInt(SystemSlider.Value);

            // number of weeks that have elapsed
            int weeks = totDays / 7;

            // number of days that have elapsed in the week
            int weekDays = totDays % 7 + 1;

            // display weeks and days
            DateText.text = weeks + "w" + weekDays + "d";
        }
    }
}
