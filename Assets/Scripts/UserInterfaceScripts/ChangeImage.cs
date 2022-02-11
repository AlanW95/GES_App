using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public Image[] coachSelectionImage;
    public Image coachEmotionImage;

    private int image = 0;

    public void CalmClarice() { image = 1; ImageSelection(); }
    public void FunnyFabio() { image = 2; ImageSelection(); }
    public void OptimisticAlen() { image = 3; ImageSelection(); }
    public void EnthusiasticEmily() { image = 4; ImageSelection(); }
    public void AmbitiousFred() { image = 5; ImageSelection(); }
    public void LogicalSam() { image = 6; ImageSelection(); }


    public void ImageSelection()
    {
        switch (image)
        {
            case 1:
                //Calm Clarice replaced
                coachEmotionImage.sprite = coachSelectionImage[0].sprite;
                break;

            case 2:
                //Funny Fabio replaced
                coachEmotionImage.sprite = coachSelectionImage[1].sprite;
                break;
            case 3:
                //Optimistic Alen replaced
                coachEmotionImage.sprite = coachSelectionImage[2].sprite;
                break;
            case 4:
                //Enthusiastic Emily replaced
                coachEmotionImage.sprite = coachSelectionImage[3].sprite;
                break;
            case 5:
                //Ambitious Fred replaced
                coachEmotionImage.sprite = coachSelectionImage[4].sprite;
                break;
            case 6:
                //Logical Sam replaced
                coachEmotionImage.sprite = coachSelectionImage[5].sprite;
                break;
            default:
                //do nothing
                coachEmotionImage.sprite = coachSelectionImage[1].sprite;
                break;
        }
    }
}
