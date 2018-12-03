using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AutoTech;
using Basler.Pylon;


namespace GxMultiCam
{
    public class CCamerInfo
    {
        public bool                  m_bIsColorFilter             = false;                                          ///<判断是否为彩色相机
        public bool                  m_bIsOpen                    = false;	                                        ///<相机已打开标志
        public bool                  m_bIsSnap                    = false;		                                    ///<相机正在采集标志
        public bool                  m_bWhiteAuto                 = false;                                          ///<标识是否支持白平衡
        public bool                  m_bAcqSpeedLevel             = false;                                          ///<采集速度级别是否支持
        public bool                  m_bWhiteAutoSelectedIndex    = true;                                           ///<白平衡列表框转换标志
        public double                m_dFps                       = 0.0;			                                ///<帧率
        public string                m_strBalanceWhiteAutoValue   = "Off";                                          ///<自动白平衡当前的值
        public string                m_strDisplayName             = "";                                             ///<设备显示名称
        public string                m_strSN                      = "";                                             ///<序列号  
        public frmShowImage          m_objImageShowFrom           = null;                                           ///<用于图像的显示
        public CCBasler              m_objBasler                  = null;
        public ICameraInfo           m_objCameraInfo              = null;             
        public int                   m_Flag                       = -1;                                              ///<用于图像的显示窗口ID

        public int                   m_iImageWidth     = 0;
        public int                   m_iImageHigth     = 0;

    }
}
