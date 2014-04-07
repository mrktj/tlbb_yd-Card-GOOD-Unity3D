// UnityPlayerActivity and WebView integration

package com.cyou.mrd.kdtl.web;

import java.util.concurrent.SynchronousQueue;

import org.json.JSONObject;

import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.Color;
import android.os.Bundle;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup.LayoutParams;
import android.view.Window;
import android.view.WindowManager;
import android.webkit.WebChromeClient;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.ProgressBar;


import com.cyou.tlyd.android.tw.R;
import com.unity3d.player.UnityPlayer;

public class KDTLWebView extends Activity{
	public static final String URL = "url";
	public static Activity activity;
    // JavaScript interface class for embedded WebView.
    private class JSInterface {
        public SynchronousQueue<String> mMessageQueue;

        JSInterface() {
            mMessageQueue = new SynchronousQueue<String>();
        }

        public void pushMessage(String message) {
            Log.d("WebView", message);
            try {
                mMessageQueue.put(message);
            } catch (java.lang.InterruptedException e) {
                Log.d("WebView", "Queueing error - " + e.getMessage());
            }
        }
    }

    private JSInterface mJSInterface = null;   // JavaScript interface (message receiver)
    private WebView mWebView = null;           // WebView object
    private ProgressBar mProgress = null;      // Progress bar
    private int mLeftMargin;            // Margins around the WebView
    private int mTopMargin;
    private int mRightMargin;
    private int mBottomMargin;
    private boolean mInitialLoad;       // Initial load flag
	private Button closeButton = null;

    protected void onCreate(Bundle savedInstanceState) {
		requestWindowFeature(Window.FEATURE_NO_TITLE);
		super.onCreate(savedInstanceState);
		activity = this;
		//保持屏幕打开状态
		getWindow().setFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON,
				WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
		setTheme(android.R.style.Theme_NoTitleBar_Fullscreen);
		getWindow ().setFlags (WindowManager.LayoutParams.FLAG_FULLSCREEN,
			                       WindowManager.LayoutParams.FLAG_FULLSCREEN);
    	init();
    }

    public void init() {
		setContentView(R.layout.kdtlwebview);
		mWebView = (WebView) findViewById(R.id.kdtlwebview);
		closeButton = (Button) findViewById(R.id.backbutton);
		mProgress = (ProgressBar)findViewById(R.id.kdtlprogressBar);
		closeButton.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				UnityPlayer.UnitySendMessage("WebMediator", "OnCloseWebView", "");
				Log.d("WebView", "CloseWebView ");				
				
				KDTLWebView.this.finish();
			}
		});
		WebSettings webSettings = mWebView.getSettings();
        webSettings.setSupportZoom(true);
        webSettings.setJavaScriptEnabled(true);
       //webSettings.setPluginsEnabled(true);
     
        //webSettings.setCacheMode(WebSettings.LOAD_NO_CACHE);
        // Set a dummy WebViewClient (which enables loading a new page in own WebView).
        mWebView.setWebViewClient(new WebViewClient(){
//       	public boolean shouldOverrideUrlLoading(WebView view, String url) {
//    			//这里实现的目标是在网页中继续点开一个新链接，还是停留在当前程序中
//       		view.loadUrl(url);
//        		return super.shouldOverrideUrlLoading(view, url);
//    		}

       		public void onPageStarted(WebView view, String url, Bitmap favicon) {
       			//Log.d("WebView","onPageStarted");
       			//super.onPageStarted(view, url, favicon);
       			if(!url.contains("member.changyou.com")){
       				return;
       			}
       			String [] temp = url.split("closeView?");
       			String userName = null;
				if (temp != null && temp.length > 1) {
					userName = temp[1];
					if(userName.startsWith("?")){
						userName = userName.substring(1);
					}
				}
				if (userName != null) {
					KDTLWebView.this.finish();
					try {
						JSONObject jobj = new JSONObject();
	       	            jobj.put("data", userName);
	       				UnityPlayer.UnitySendMessage("AccountManager", "OnCyouRegCallback", jobj.toString());
					} catch (Throwable e) {
						e.printStackTrace();
					}
				}
       		}
        });
		
        mProgress.setMax(100);
        mProgress.setVisibility(View.GONE);
        mWebView.setWebChromeClient(new WebChromeClient() {
            public void onProgressChanged(WebView view, int progress) {
                if (progress < 100) {
                    mProgress.setVisibility(View.VISIBLE);
                    mProgress.setProgress(progress);
                } else {
                    mProgress.setVisibility(View.GONE);
                }
            }
        });
    	
        // Create a WebView and make layout.
//    	LinearLayout layout = new LinearLayout(this);
//    	layout.setOrientation(LinearLayout.VERTICAL);
//        addContentView(layout, new LayoutParams(LayoutParams.FILL_PARENT, LayoutParams.FILL_PARENT));
//		closeButton = new Button(this);
//		closeButton.setText(R.string.back);
		
		//设置按钮居中
//		LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(LayoutParams.WRAP_CONTENT,LayoutParams.WRAP_CONTENT); 
//		lp.gravity = Gravity.CENTER; 
//		closeButton.setLayoutParams(lp);
//		closeButton.setBackgroundResource(R.drawable.back_button);
//		closeButton.setOnClickListener(new OnClickListener() {
//			@Override
//			public void onClick(View v) {
//				KDTLWebView.this.finish();
//
//			}
//		});
//		layout.addView(closeButton, new FrameLayout.LayoutParams(LayoutParams.WRAP_CONTENT, LayoutParams.WRAP_CONTENT, Gravity.LEFT|Gravity.TOP));
//        mWebView = new WebView(this);
//        layout.addView(mWebView, new FrameLayout.LayoutParams(LayoutParams.FILL_PARENT, LayoutParams.FILL_PARENT, Gravity.CENTER));
        // Basic settings of WebView.
//        WebSettings webSettings = mWebView.getSettings();
//        webSettings.setSupportZoom(false);
//        webSettings.setJavaScriptEnabled(true);
//        webSettings.setPluginsEnabled(true);
//        //webSettings.setCacheMode(WebSettings.LOAD_NO_CACHE);
//        // Set a dummy WebViewClient (which enables loading a new page in own WebView).
//        mWebView.setWebViewClient(new WebViewClient(){
////       		public boolean shouldOverrideUrlLoading(WebView view, String url) {
////    			//这里实现的目标是在网页中继续点开一个新链接，还是停留在当前程序中
////    			view.loadUrl(url);
////    			return super.shouldOverrideUrlLoading(view, url);
////    		}
//        });
        // Add a progress bar.
//        mProgress = new ProgressBar(this, null, android.R.attr.progressBarStyleHorizontal);
//        layout.addView(mProgress, new FrameLayout.LayoutParams(LayoutParams.FILL_PARENT, 5));
//        mProgress.setMax(100);
//        mProgress.setVisibility(View.GONE);
//        mWebView.setWebChromeClient(new WebChromeClient() {
//            public void onProgressChanged(WebView view, int progress) {
//                if (progress < 100) {
//                    mProgress.setVisibility(View.VISIBLE);
//                    mProgress.setProgress(progress);
//                } else {
//                    mProgress.setVisibility(View.GONE);
//                }
//            }
//        });

        // Create a JavaScript interface and bind the WebView to it.
        mJSInterface = new JSInterface();
        mWebView.addJavascriptInterface(mJSInterface, "UnityInterface");
        // 打开页面//
    	mWebView.loadUrl(getIntent().getStringExtra(URL));
        mWebView.requestFocus();
    }

    public void updateWebView(final String lastRequestedUrl, final boolean loadRequest, final boolean visibility, final int leftMargin, final int topMargin, final int rightMargin, final int bottomMargin) {
        // Process load requests.
        if (lastRequestedUrl != null && (loadRequest || !mInitialLoad)) {
        	runOnUiThread(new Runnable() {
                public void run() {
                    mWebView.loadUrl(lastRequestedUrl);
                }
            });
            mInitialLoad = true;
        }
        // Process changes in margin amounts.
        if (leftMargin != mLeftMargin || topMargin != mTopMargin || rightMargin != mRightMargin || bottomMargin != mBottomMargin) {
            mLeftMargin = leftMargin;
            mTopMargin = topMargin;
            mRightMargin = rightMargin;
            mBottomMargin = bottomMargin;
            runOnUiThread(new Runnable() {
                public void run() {
                    // Apply a new layout to the WebView.
                    FrameLayout.LayoutParams params = new FrameLayout.LayoutParams(LayoutParams.FILL_PARENT, LayoutParams.FILL_PARENT, Gravity.NO_GRAVITY);
                    params.setMargins(mLeftMargin, mTopMargin, mRightMargin, mBottomMargin);
                    mWebView.setLayoutParams(params);
                }
            });
        }
        // Process changes in visibility.
        if (visibility != (mWebView.getVisibility() == View.VISIBLE)) {
        	runOnUiThread(new Runnable() {
                public void run() {
                    if (visibility) {
                        // Show and set focus.
                        mWebView.setVisibility(View.VISIBLE);
                        mWebView.requestFocus();
                    } else {
                        // Hide.
                        mWebView.setVisibility(View.GONE);
                    }
                }
            });
        }
    }

    public String pollWebViewMessage() {
        return mJSInterface.mMessageQueue.poll();
    }
    
    // Transparent background
    public void makeTransparentWebViewBackground() {
        mWebView.setBackgroundColor(Color.TRANSPARENT);
    }
    
    public void onDestroy(){
    	super.onDestroy();
		try {
	    	mJSInterface.mMessageQueue.clear();
	    	mProgress.destroyDrawingCache();
	    	closeButton.destroyDrawingCache();
	    	mWebView.destroy();
	    	 
	    	/*如果再遇到退出webvie白屏，解开此段代码
	    	RelativeLayout rel = (RelativeLayout) findViewById(R.layout.kdtlwebview);
	    	rel.removeView(mWebView);
	    	mWebView.removeAllViews();
	    	mWebView.destroy();*/
		} catch (Throwable t) {
			t.printStackTrace();
		}
    }
} 