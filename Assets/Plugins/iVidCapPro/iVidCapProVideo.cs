// iVidCapPro Copyright (c) 2012-2013 James Allen and eccentric Orbits entertainment (eOe)
//
/*
Permission is hereby granted, free of charge, to any person or organization obtaining a copy of 
the software and accompanying documentation covered by this license (the "Software") to use
and prepare derivative works of the Software, for commercial or other purposes, excepting that the Software
may not be repackaged for sale as a Unity asset.

The copyright notices in the Software and this entire statement, including the above license grant, 
this restriction and the following disclaimer, must be included in all copies of the Software, 
in whole or in part.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE, TITLE AND NON-INFRINGEMENT. IN NO EVENT SHALL 
THE COPYRIGHT HOLDERS OR ANYONE DISTRIBUTING THE SOFTWARE BE LIABLE FOR ANY DAMAGES OR OTHER LIABILITY, WHETHER 
IN CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
IN THE SOFTWARE.
*/

/*------------------------------------------------------------------------------*/
/**
	@file 		iVidCapProVideo.cs
	@brief 		Place this script on a camera from which video will be captured.
	
--------------------------------------------------------------------------------*/

using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;


/* ---------------------------------------------------------------------
   Change History:
   
   - 17 Feb 13 - Created.
   --------------------------------------------------------------------- */

/// <summary>
/// This class provides the video frame capture capability of the iVidCapPro plugin.
/// 
/// Place it on a Unity Camera from which you want video to be recorded.  
/// The camera image effect/filter chain method OnRenderImage() is used to capture
/// frames from the camera, so be sure to place the iVidCapPro script component
/// after any other image effects/filter components on the camera.
/// 
/// You may use multiple components of this type. Place one on each camera from
/// which video should be captured. For example, if you want to capture footage
/// of a demo of your app, place one on the main camera and one on the GUI camera.
/// The resultant video will include the output from both cameras.
/// </summary>
public class iVidCapProVideo : MonoBehaviour {
	
	/* ------------------------------------------------------------------------
	   -- Member variables --
	   ------------------------------------------------------------------------ */
	
	/// <summary>
	/// Specifies whether or not the camera being used to capture video is dedicated 
	/// solely to video capture. When a dedicated camera is used,
	/// the camera's aspect ratio will automatically be set to the specified frame size.
	/// If a non-dedicated camera is specified it is assumed the camera will also be used
	/// to render to the screen, and so the camera's aspect ratio will not be adjusted.
	/// Use a dedicated camera to capture video at resolutions that have a different aspect
	/// ratio than the device screen.
	/// </summary>
	public bool isDedicated = false;
	
	// The camera that resides on the same game object as this script.
	// It will be used for capturing video.
	private Camera videoCam;
	
	// A local reference to the target rendertexture.
	private RenderTexture rt = null;
	
	// The rectangle that defines the viewport to be captured to the rendertexture.
	private Rect captureRect;
	
	// Whether or not recording from this camera is currently in progress.
	private bool isRecording = false;
	
	public void Awake () {
	
		videoCam = GetComponent<Camera>();
		if (videoCam == null) {
			// This game object has no camera component.
			Debug.LogWarning("iVidCapProVideo: Game object " + this.gameObject.name + 
				" needs a camera component to capture video.");
		} 
	}
	
	/// <summary>
	/// Set the capture viewport of the camera on the rendertexture.
	/// Ordinarily you don't need to call this, as it is set automatically
	/// at the start of each recording session.  If, however, you change
	/// the viewport of the camera during the recording session, you need
	/// to call this function each time the camera viewport is updated.
	/// </summary>
	public void SetCaptureViewport() {
		
		
		Rect cameraRect = videoCam.rect;
		
		captureRect.x = cameraRect.x * rt.width;
		captureRect.y = cameraRect.y * rt.height;
		captureRect.width = cameraRect.width * rt.width;
		captureRect.height = cameraRect.height * rt.height;
		
		if (isDedicated) {
			// Set the aspect ratio of the camera to match the render texture.
			videoCam.aspect = ((float)rt.width)/((float)rt.height);
		}	
	}
	
	/* ------------------------------------------------------------------------
	   -- SetRenderTexture --
	   
	   This function is called by the controller to set the rendertexture that
	   will be used for video capture. 
	   ------------------------------------------------------------------------ */
	public void SetRenderTexture(RenderTexture rt) {
		this.rt = rt;
	}
	
	/* ------------------------------------------------------------------------
	   -- SetIsRecording --
	   
	   This function is called by the controller to set whether or not recording
	   is currently in progress.
	   ------------------------------------------------------------------------ */
	public void SetIsRecording(bool isRecording) {
		this.isRecording = isRecording;
	}
	
	/* ------------------------------------------------------------------------
	   -- OnRenderImage --
	   
	   This function is called at the end of rendering for the camera to 
	   which this script is attached.  Here we blit the camera output into
	   the render texture that is serving as a source of frames for the 
	   video we're recording in the plugin. 
	   
	   Note that we have to blit the source to the destination so that the 
	   camera render is passed along to the next stage (possibly an image
	   effect we don't want included in the video or to the screen itself).
	   	   		
	   ------------------------------------------------------------------------ */
	private void OnRenderImage (RenderTexture source,  RenderTexture destination) {	
		
		//print ("OnRenderImage called...");
		if (rt != null && isRecording) {
			
			RenderTexture.active = rt;
			
			// We want to honor the size and location on the screen of the camera rendering
			// rectangle.  These GL routines allow us to restrict the rendering viewport to
			// be that of the camera when we do the blit.
			GL.PushMatrix();
    		GL.LoadPixelMatrix();
			GL.Viewport(captureRect);
			
			Graphics.Blit (source, rt);
			
			// Restore the modelview and projection matrices.
			GL.PopMatrix();
			
			RenderTexture.active = null;
			
			//RenderTextureToPNG(rt, "frame2_capture.png");
			
		}
		
		// If the camera is dedicated to video recording we don't need to pass the 
		// image any further along the rendering chain.
		if (!isDedicated) {
			// Pass the image to the next stage of rendering.
			Graphics.Blit (source, destination);
		}
	}
	
	/* ------------------------------------------------------------------------
	   -- RenderTextureToPNG --
	   
	   Capture a rendertexture to a PNG file.
	   ------------------------------------------------------------------------ */
	private void RenderTextureToPNG(RenderTexture rtex, string fileName) {
		string filePath = GetDocumentsPath() + "/" + fileName;
		byte[] imageBytes; 
		
		Texture2D captureTex = new Texture2D(rtex.width, rtex.height, TextureFormat.RGB24, false);
		RenderTexture.active = rtex;
        captureTex.ReadPixels(new Rect(0, 0, rtex.width, rtex.height), 0, 0);
        RenderTexture.active = null;
        imageBytes = captureTex.EncodeToPNG();
        System.IO.File.WriteAllBytes(filePath, imageBytes);
	}
	
	/* ------------------------------------------------------------------------
	   -- GetDocumentsPath --
	   
	   Return the path to the Documents directory for our application.
	   Note that it is platform dependent.  The Documents directory is
	   assumed to exist.
	   
	   ------------------------------------------------------------------------ */
	private string GetDocumentsPath() {
		
		string documentsPath = "";
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			documentsPath = Application.dataPath + "/../../Documents";
		} else if (Application.platform == RuntimePlatform.OSXEditor) {
			documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
		}
		
		return documentsPath;
	}
}
