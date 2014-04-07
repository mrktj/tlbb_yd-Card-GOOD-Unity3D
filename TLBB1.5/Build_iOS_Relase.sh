#! /bin/bash

################ Path Configure ################
SVN_PATH='svn://10.6.35.101/WUXIAN/Code/TLCard/Client'
PROJ_PATH='/Users/cymrd/Desktop/CI/workspace/KDTLClient/client'
UNITY_PATH='/Applications/Unity/Unity.app/Contents/MacOS/Unity'
OUT_PUT_DIR=$HOME/Desktop/Product/KDTL
XCODE_PROJECT_PATH=$PROJ_PATH/TLCard

################ Your Command ################
echo ''
echo "****************** Your Command: *************************"
echo "Build KDTL iOS $*"
echo "*********************************************************"
echo ''
mkdir -p $OUT_PUT_DIR/$1/
LOG_FILE='$OUT_PUT_DIR/$1/log.txt'

################ Delete Old ################
echo "Start Clean…"
rm -rf $PROJ_PATH
rm -rf $LOG_FILE
echo "Clean Done."

################ Check Out From  SVN ################
echo "Start Check Out…"
svn checkout --username lijiuchao --password 741456 $SVN_PATH  $PROJ_PATH
echo "Check Out Done."

################ Build UNITY Project ################
echo "Start Build Unity Project…"
$UNITY_PATH -batchmode -quit -projectPath $PROJ_PATH -logFile $LOG_FILE -executeMethod CommandBuild.BuildiOS
echo "Build Unity Project Done."

################ Copy Xcode Porject to Output Folder ################ 
echo copy $XCODE_PROJECT_PATH to  $OUT_PUT_DIR/$1

cp -r $XCODE_PROJECT_PATH $OUT_PUT_DIR/$1

cp -r $OUT_PUT_DIR/$1 /Users/cymrd/MRDVersion/KDTL
echo $1>/Users/cymrd/MRDVersion/KDTL/NewVersion.txt

################ Build Xcode PROJECT ################
###echo "Start Build Xcode Project"
###echo "Build Xcode Project Done."

echo "Version Auto build succeed !  `date`"

exit 0

