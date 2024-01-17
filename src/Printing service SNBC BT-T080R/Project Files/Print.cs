using System.Runtime.InteropServices;

namespace Project_Files;

public class Print
{
    //Port type
    private const int PORT_TYPE_COM = 0x00;
    private const int PORT_TYPE_LPT = 0x01;
    private const int PORT_TYPE_USB = 0x02;
    private const int PORT_TYPE_DRV = 0x03;

    //Print mode
    private const int PRINT_MODE_STANDRAD = 0x00;
    private const int PRINT_MODE_PAGE = 0x01;

    //Page width
    private const int PAGE_WDITH_56 = 0x00;
    private const int PAGE_WDITH_80 = 0x01;
    private const int PAGE_WDITH_210 = 0x02;

    //Printer resolution
    private const int RESOLUTION_203_DPI = 0x00;
    private const int RESOLUTION_300_DPI = 0x01;

    //Presenter or bundler
    private const int PAPER_OUT_PRESENTER = 0x00;
    private const int PAPER_OUT_BUNDLER = 0x01;

    //Presenter mode
    private const int PRESENTER_RETRACT = 0x00;
    private const int PRESENTER_FORWARD = 0x01;
    private const int PRESENTER_HOLD = 0x02;

    //Bundler mode
    private const int BUNDLER_RETRACT = 0x00;
    private const int BUNDLER_PRESENT = 0x01;

    //Init variable
    public int nPortType = PORT_TYPE_USB;
    public int nMode = PRINT_MODE_STANDRAD;
    public int nPageWidth = PAGE_WDITH_80;
    public int nResolution = RESOLUTION_203_DPI;
    public int nPaperOut = PAPER_OUT_PRESENTER;
    public int nPresenter = PRESENTER_RETRACT;
    public int nBundler = BUNDLER_RETRACT;

    public static bool bIsFirst = true;

    public int hPort = -1;
    public bool IsPrinter = false;
    public bool bSaveToTxt = false;
    public bool bThreadRunning = false;

    #region KIOSKDLL const declare

    //return value
    //Success
    private const int KIOSK_SUCCESS = 1001;

    //Fail
    private const int KIOSK_FAIL = 1002;

    //Invalid handle
    private const int KIOSK_ERROR_INVALID_HANDLE = 1101;

    //Invalid parameter
    private const int KIOSK_ERROR_INVALID_PARAMETER = 1102;

    //Invalid path
    private const int KIOSK_ERROR_INVALID_PATH = 1201;

    //File is not bitmap
    private const int KIOSK_ERROR_NOT_BITMAP = 1202;

    //Bitmap is not mono
    private const int KIOSK_ERROR_NOT_MONO_BITMAP = 1203;

    //Bitmap is too large
    private const int KIOSK_ERROR_BEYOND_AREA = 1204;

    //Operate file error
    private const int KIOSK_ERROR_FILE = 1301;

    //Com stopbits
    private const int KIOSK_COM_ONESTOPBIT = 0x00;
    private const int KIOSK_COM_TWOSTOPBITS = 0x01;

    //Com parity
    private const int KIOSK_COM_NOPARITY = 0x00;
    private const int KIOSK_COM_ODDPARITY = 0x01;
    private const int KIOSK_COM_EVENPARITY = 0x02;
    private const int KIOSK_COM_MARKPARITY = 0x03;
    private const int KIOSK_COM_SPACEPARITY = 0x04;

    //Com flow control
    private const int KIOSK_COM_DTR_DSR = 0x00;
    private const int KIOSK_COM_RTS_CTS = 0x01;
    private const int KIOSK_COM_XON_XOFF = 0x02;
    private const int KIOSK_COM_NO_HANDSHAKE = 0x03;

    //Print mode
    private const int KIOSK_PRINT_MODE_STANDARD = 0x00;
    private const int KIOSK_PRINT_MODE_PAGE = 0x01;

    //Paper type
    private const int KIOSK_PAPER_SERIAL = 0x00;
    private const int KIOSK_PAPER_SIGN = 0x01;

    //Font type
    private const int KIOSK_FONT_TYPE_STANDARD = 0x00;
    private const int KIOSK_FONT_TYPE_COMPRESSED = 0x01;
    private const int KIOSK_FONT_TYPE_UDC = 0x02;
    private const int KIOSK_FONT_TYPE_CHINESE = 0x03;

    //Font style
    private const int KIOSK_FONT_STYLE_NORMAL = 0x00;
    private const int KIOSK_FONT_STYLE_BOLD = 0x08;
    private const int KIOSK_FONT_STYLE_THIN_UNDERLINE = 0x80;
    private const int KIOSK_FONT_STYLE_THICK_UNDERLINE = 0x100;
    private const int KIOSK_FONT_STYLE_UPSIDEDOWN = 0x200;
    private const int KIOSK_FONT_STYLE_REVERSE = 0x400;
    private const int KIOSK_FONT_STYLE_CLOCKWISE_90 = 0x1000;

    //Bitmap mode
    private const int KIOSK_BITMAP_MODE_8SINGLE_DENSITY = 0x00;
    private const int KIOSK_BITMAP_MODE_8DOUBLE_DENSITY = 0x01;
    private const int KIOSK_BITMAP_MODE_24SINGLE_DENSITY = 0x20;
    private const int KIOSK_BITMAP_MODE_24DOUBLE_DENSITY = 0x21;

    //Bitmap print mode
    private const int KIOSK_BITMAP_PRINT_NORMAL = 0x00;
    private const int KIOSK_BITMAP_PRINT_DOUBLE_WIDTH = 0x01;
    private const int KIOSK_BITMAP_PRINT_DOUBLE_HEIGHT = 0x02;
    private const int KIOSK_BITMAP_PRINT_QUADRUPLE = 0x03;

    //Bar code type
    private const int KIOSK_BARCODE_TYPE_UPC_A = 0x41;
    private const int KIOSK_BARCODE_TYPE_UPC_E = 0x42;
    private const int KIOSK_BARCODE_TYPE_JAN13 = 0x43;
    private const int KIOSK_BARCODE_TYPE_JAN8 = 0x44;
    private const int KIOSK_BARCODE_TYPE_CODE39 = 0x45;
    private const int KIOSK_BARCODE_TYPE_ITF = 0x46;
    private const int KIOSK_BARCODE_TYPE_CODEBAR = 0x47;
    private const int KIOSK_BARCODE_TYPE_CODE93 = 0x48;
    private const int KIOSK_BARCODE_TYPE_CODE128 = 0x49;

    //HRI position
    private const int KIOSK_HRI_POSITION_NONE = 0x00;
    private const int KIOSK_HRI_POSITION_ABOVE = 0x01;
    private const int KIOSK_HRI_POSITION_BELOW = 0x02;
    private const int KIOSK_HRI_POSITION_BOTH = 0x03;

    //Print direction in page mode
    private const int KIOSK_AREA_LEFT_TO_RIGHT = 0x00;
    private const int KIOSK_AREA_BOTTOM_TO_TOP = 0x01;
    private const int KIOSK_AREA_RIGHT_TO_LEFT = 0x02;
    private const int KIOSK_AREA_TOP_TO_BOTTOM = 0x03;

    //Presenter mode
    private const int KIOSK_PRESENTER_Retraction_on = 0x00;
    private const int KIOSK_PRESENTER_Paper_Forward = 0x01;
    private const int KIOSK_PRESENTER_Paper_Hold = 0x02;
    private const int KIOSK_PRESENTER_Close = 0x03;

    //Bundler mode
    private const int KIOSK_BUNDLER_Petract = 0x00;
    private const int KIOSK_BUNDLER_Present = 0x01;

    #endregion

    #region KIOSKDLL function declare

    #region Communication function

    //Communication function
    //COM
    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_OpenCom(string lpName,
        int nComBaudrate,
        int nComDataBits,
        int nComStopBits,
        int nComParity,
        int nFlowControl);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_SetComTimeOuts(int hPort,
        int nWriteTimeoutMul,
        int nWriteTimeoutCon,
        int nReadTimeoutMul,
        int nReadTimeoutCon);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_CloseCom(int hPort);


    //Driver LPT
    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_OpenLptByDrv(ushort LPTAddress);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_CloseDrvLPT(int nPortType);

    //USB
    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_OpenUsb();

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_OpenUsbByID(int nID);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_CloseUsb(int hPort);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_SetUsbTimeOuts(int hPort, ushort wReadTimeouts, ushort wWriteTimeouts);

    //Driver
    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_OpenDrv(char[] drivername);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_CloseDrv(int hPort);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_StartDoc(int hPort);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_EndDoc(int hPort);


    //Write Read Port
    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_WriteData(int hPort, int nPortType, char[] pszData, int nBytesToWrite);

    [DllImport("KIOSKDLL.dll")]
    private static extern unsafe int KIOSK_ReadData(int hPort, int nPortType, int nStatus, byte* pszBuffer,
        int nBytesToRead);

    #endregion

    #region Assistant function

    //Assistant function
    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_SendFile(int hPort, int nPortType, string filename);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_BeginSaveToFile(int hPort, string lpFileName, bool bToPrinter);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_EndSaveToFile(int hPort);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_QueryStatus(int hPort, int nPortType, string pszStatus, int nTimeouts);

    [DllImport("KIOSKDLL.dll")]
    private static extern unsafe int KIOSK_RTQueryStatus(int hPort, int nPortType, char* pszStatus);

    [DllImport("KIOSKDLL.dll")]
    private static extern unsafe int KIOSK_RTQueryStatusForT681(int hPort, int nPortType, char* pszStatus);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_QueryASB(int hPort, int nPortType, int Enable);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_QueryStatus_T(int hPort, int nPortType, string pszStatus, int nTimeouts);

    [DllImport("KIOSKDLL.dll")]
    private static extern unsafe int KIOSK_RTQueryStatus_T(int hPort, int nPortType, char* pszStatus);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_QueryASB_T(int hPort, int nPortType, int Enable);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_GetVersionInfo(IntPtr pnMajor, IntPtr pnMinor);

    #endregion

    #region Printer command function

    //Printer command function
    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_EnableMacro(int hPort, int nPortType, int nEnable);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_Reset(int hPort, int nPortType);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_SetPaperMode(int hPort, int nPortType, int nMode);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_SetMode(int hPort, int nPortType, int nPrintMode);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_SetMotionUnit(int hPort, int nPortType, int nHorizontalMU, int nVerticalMU);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_SetLineSpacing(int hPort, int nPortType, int nDistance);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_SetRightSpacing(int hPort, int nPortType, int nDistance);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_SetOppositePosition(int hPort,
        int nPortType,
        int nPrintMode,
        int nHorizontalDist,
        int nVerticalDist);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_SetTabs(int hPort, int nPortType, string pszPosition, int nCount);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_ExecuteTabs(int hPort, int nPortType);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_PreDownloadBmpToRAM(int hPort, int nPortType, string pszPaths, int nTranslateMode,
        int nID);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_PreDownloadBmpToFlash(int hPort,
        int nPortType,
        string[] pszPaths,
        int nTranslateMode,
        int nCount);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_SetCharSetAndCodePage(int hPort, int nPortType, int nCharSet, int nCodePage);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_FontUDChar(int hPort,
        int nPortType,
        int nEnable,
        int DPI,
        int nChar,
        int nCharCode,
        char[] pCharBmpBuffer,
        int nDotsOfWidth,
        int nBytesOfHeight);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_FeedLine(int hPort, int nPortType);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_FeedLines(int hPort, int nPortType, int nLines);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_MarkerFeedPaper(int hPort, int nPortType);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_CutPaper(int hPort, int nPortType, int nMode, int nDistance);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_MarkerCutPaper(int hPort, int nPortType, int nDistance);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_S_SetLeftMarginAndAreaWidth(int hPort,
        int nPortType,
        int nDistance,
        int nWidth);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_S_SetAlignMode(int hPort, int nPortType, int nMode);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_S_Textout(int hPort,
        int nPortType,
        string pszData,
        int nOrgx,
        int nWidthTimes,
        int nHeightTimes,
        int nFontType,
        int nFontStyle);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_S_DownloadPrintBmp(int hPort,
        int nPortType,
        string pszPath,
        int nTranslateMode,
        int nOrgx,
        int nMode);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_S_PrintBmpInRAM(int hPort,
        int nPortType,
        int nID,
        int nOrgx,
        int nMode);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_S_PrintBmpInFlash(int hPort,
        int nPortType,
        int nID,
        int nOrgx,
        int nMode);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_S_PrintBarcode(int hPort,
        int nPortType,
        char[] pszBuffer,
        int nOrgx,
        int nType,
        int nWidth,
        int nHeight,
        int nHriFontType,
        int nHriFontPosition,
        int nBytesOfBuffer);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_P_SetAreaAndDirection(int hPort,
        int nPortType,
        int nOrgx,
        int nOrgy,
        int nWidth,
        int nHeight,
        int nDirection);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_P_Textout(int hPort,
        int nPortType,
        string pszData,
        int nOrgx,
        int nOrgy,
        int nWidthTimes,
        int nHeightTimes,
        int nFontType,
        int nFontStyle);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_P_DownloadPrintBmp(int hPort,
        int nPortType,
        string pszPath,
        int nTranslateMode,
        int nOrgx,
        int nOrgy,
        int nMode);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_P_PrintBmpInRAM(int hPort,
        int nPortType,
        int nID,
        int nOrgx,
        int nOrgy,
        int nMode);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_P_PrintBmpInFlash(int hPort,
        int nPortType,
        int nID,
        int nOrgx,
        int nOrgy,
        int nMode);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_P_PrintBarcode(int hPort,
        int nPortType,
        char[] pszBuffer,
        int nOrgx,
        int nOrgy,
        int nType,
        int nWidth,
        int nHeight,
        int nHriFontType,
        int nHriFontPosition,
        int nBytesOfBuffer);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_P_Print(int hPort, int nPortType);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_P_Clear(int hPort, int nPortType);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_S_TestPrint(int hPort, int nPortType);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_CountMode(int hPort,
        int nPortType,
        int nBits,
        int nOrder,
        int nSBound,
        int nEBound,
        int nTimeSpace,
        int nCycTime,
        int nCount);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_SetPrintFromStart(int hPort, int nPortType, int nMode);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_SetRaster(int hPort, int nPortType, string pszBmpPath, int nTranslateMode,
        int nMode);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_P_Lineation(int hPort,
        int nPortType,
        int nWidth,
        int nSHCoordinate,
        int nSVCoordinate,
        int nEHCoordinate,
        int nEVCoordinate);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_BarcodeSetPDF417(int hPort,
        int nPortType,
        string content,
        int nBytesOfBuffer,
        int nWidth,
        int nHeight,
        int nColumns,
        int nLines,
        int nScaleH,
        int nScaleV,
        int nCorrectGrade);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_SetChineseFont(int hPort,
        int nPortType,
        string pszBuffer,
        int nEnable,
        int nBigger,
        int nLSpacing,
        int nRSpacing,
        int nUnderLine);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_SetPresenter(int hPort, int nPortType, int nMode, int nTime);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_SetBundler(int hPort, int nPortType, int nMode, int nTime);

    [DllImport("KIOSKDLL.dll")]
    private static extern int KIOSK_SetBundlerInfo(int hPort, int nPortType, int nMode);

    #endregion

    #endregion
}