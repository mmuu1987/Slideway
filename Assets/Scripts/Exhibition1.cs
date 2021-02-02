using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Exhibition1 : ExhibitionItem
{
    // Token: 0x06000006 RID: 6 RVA: 0x00002124 File Offset: 0x00000324
    private void OnEnable()
    {
        if(_videoPlayer==null)
        this._videoPlayer = base.GetComponent<VideoPlayer>();

        if (this._videoPlayer == null)
        {
            throw new UnityException("没有找到播放器");
        }
        this._videoPlayer.url = "file:///" + Application.streamingAssetsPath + "/alibaba.mp4";
    }

    // Token: 0x06000007 RID: 7 RVA: 0x00002175 File Offset: 0x00000375
    protected override void PointArrive(int obj, bool isRight)
    {
        if (obj == this.ID)
        {
            this._isStay = true;
        }
        else
        {
            this._isStay = false;
        }

        if (isRight)
        {
            base.gameObject.SetActive(false);
        }
        else
        {
            if (obj == ID)
                base.gameObject.SetActive(true);
            else 
                base.gameObject.SetActive(false);
        }
          
    }

    // Token: 0x06000008 RID: 8 RVA: 0x0000219C File Offset: 0x0000039C
    protected override void Message(string obj)
    {
        Debug.LogError(obj);
        if (_videoPlayer == null)
            this._videoPlayer = base.GetComponent<VideoPlayer>();
        if (obj == "pause")
        {
            this._videoPlayer.Pause();
            return;
        }
        if (obj == "stop")
        {
            this._videoPlayer.time = 0.0;
            this._videoPlayer.Play();
            return;
        }
      
      


        if (obj == "play")
        {
            if (this._isStay)
            {
                base.gameObject.SetActive(true);
            }
            this._videoPlayer.Play();
          
        }

    }

    // Token: 0x06000009 RID: 9 RVA: 0x00002050 File Offset: 0x00000250
    private void Update()
    {
    }

    // Token: 0x04000002 RID: 2
    private bool _isStay;

    // Token: 0x04000003 RID: 3
    private VideoPlayer _videoPlayer;
}
