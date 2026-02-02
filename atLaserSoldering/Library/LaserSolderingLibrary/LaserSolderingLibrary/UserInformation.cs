using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserSoldering
{
    public class UserInformation
    {
        public UserInformation(string UserID, string Password, string MD5Hash, int UserLevel = 0)
        {
            mUserID = UserID;
            mPassword = Password;
            mUserLevel = UserLevel;
            mMD5Hash = MD5Hash;
        }
        public UserInformation()
        {
        }
        private string mUserID = string.Empty;
        public string UserID
        {
            get { return mUserID; }
            set { mUserID = value; }
        }

        private string mPassword = string.Empty;
        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }
        private string mMD5Hash = string.Empty;
        public string strMD5Hash
        {
            get { return mMD5Hash; }
            set { mMD5Hash = value; }
        }
        private int mUserLevel = 0;
        public int UserLevel
        {
            get { return mUserLevel; }
            set { mUserLevel = value; }
        }
    }
}
