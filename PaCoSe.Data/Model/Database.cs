// <auto-generated />
// This file was automatically generated by the PetePocoGenerator
// 
// The following connection settings were used to generate this file
// 
//     Connection String: `Data Source=WIN-MDA7DA5H0OM;database=PaCoSe;user id=sa;password=**zapped**;`
//     Provider:               `SqlServer`

// <auto-generated />
namespace PaCoSe.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using PetaPoco;

    public partial class Record<T> where T : new()
    {
        private Dictionary<string, bool> modifiedColumns = null;
        [Ignore]
        public Dictionary<string, bool> ModifiedColumns { get { return modifiedColumns; } }
        protected void MarkColumnModified(string column_name)
        {
            if (ModifiedColumns != null)
            {
                ModifiedColumns[column_name] = true;
            }
        }

        private void OnLoaded()
        {
            modifiedColumns = new Dictionary<string,bool>();
        }
    }

    [TableName("[dbo].[Device]")]
    [PrimaryKey("Id")]
    [ExplicitColumns]
    public partial class Device
        : Record<Device>
    {
        [Column]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                _CreatedBy = value;
                MarkColumnModified("CreatedBy");
            }
        }

        private string _CreatedBy;

        [Column]
        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set
            {
                _DateCreated = value;
                MarkColumnModified("DateCreated");
            }
        }

        private DateTime _DateCreated;

        [Column]
        public DateTime DateModified
        {
            get { return _DateModified; }
            set
            {
                _DateModified = value;
                MarkColumnModified("DateModified");
            }
        }

        private DateTime _DateModified;

        [Column]
        public long Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                MarkColumnModified("Id");
            }
        }

        private long _Id;

        [Column]
        public string IdentifierHash
        {
            get { return _IdentifierHash; }
            set
            {
                _IdentifierHash = value;
                MarkColumnModified("IdentifierHash");
            }
        }

        private string _IdentifierHash;

        [Column]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                _ModifiedBy = value;
                MarkColumnModified("ModifiedBy");
            }
        }

        private string _ModifiedBy;

        [Column]
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                MarkColumnModified("Name");
            }
        }

        private string _Name;

    }

    [TableName("[dbo].[DeviceOwner]")]
    [PrimaryKey("Id")]
    [ExplicitColumns]
    public partial class DeviceOwner
        : Record<DeviceOwner>
    {
        [Column]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                _CreatedBy = value;
                MarkColumnModified("CreatedBy");
            }
        }

        private string _CreatedBy;

        [Column]
        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set
            {
                _DateCreated = value;
                MarkColumnModified("DateCreated");
            }
        }

        private DateTime _DateCreated;

        [Column]
        public DateTime DateModified
        {
            get { return _DateModified; }
            set
            {
                _DateModified = value;
                MarkColumnModified("DateModified");
            }
        }

        private DateTime _DateModified;

        [Column]
        public long DeviceId
        {
            get { return _DeviceId; }
            set
            {
                _DeviceId = value;
                MarkColumnModified("DeviceId");
            }
        }

        private long _DeviceId;

        [Column]
        public long Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                MarkColumnModified("Id");
            }
        }

        private long _Id;

        [Column]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                _ModifiedBy = value;
                MarkColumnModified("ModifiedBy");
            }
        }

        private string _ModifiedBy;

        [Column]
        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set
            {
                _IsDeleted = value;
                MarkColumnModified("IsDeleted");
            }
        }

        private bool _IsDeleted;

        [Column]
        public long OwnerId
        {
            get { return _OwnerId; }
            set
            {
                _OwnerId = value;
                MarkColumnModified("OwnerId");
            }
        }

        private long _OwnerId;

    }

    [TableName("[dbo].[DeviceOwnerView]")]
    [PrimaryKey("Id")]
    [ExplicitColumns]
    public partial class DeviceOwnerView
        : Record<DeviceOwnerView>
    {
        [Column]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                _CreatedBy = value;
                MarkColumnModified("CreatedBy");
            }
        }

        private string _CreatedBy;

        [Column]
        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set
            {
                _DateCreated = value;
                MarkColumnModified("DateCreated");
            }
        }

        private DateTime _DateCreated;

        [Column]
        public DateTime DateModified
        {
            get { return _DateModified; }
            set
            {
                _DateModified = value;
                MarkColumnModified("DateModified");
            }
        }

        private DateTime _DateModified;

        [Column]
        public long Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                MarkColumnModified("Id");
            }
        }

        private long _Id;

        [Column]
        public string IdentifierHash
        {
            get { return _IdentifierHash; }
            set
            {
                _IdentifierHash = value;
                MarkColumnModified("IdentifierHash");
            }
        }

        private string _IdentifierHash;

        [Column]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                _ModifiedBy = value;
                MarkColumnModified("ModifiedBy");
            }
        }

        private string _ModifiedBy;

        [Column]
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                MarkColumnModified("Name");
            }
        }

        private string _Name;

        [Column]
        public long OwnerId
        {
            get { return _OwnerId; }
            set
            {
                _OwnerId = value;
                MarkColumnModified("OwnerId");
            }
        }

        private long _OwnerId;

    }

    [TableName("[dbo].[ChildDeviceView]")]
    [PrimaryKey("Id")]
    [ExplicitColumns]
    public partial class ChildDeviceView
        : Record<ChildDeviceView>
    {
        [Column]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                _CreatedBy = value;
                MarkColumnModified("CreatedBy");
            }
        }

        private string _CreatedBy;

        [Column]
        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set
            {
                _DateCreated = value;
                MarkColumnModified("DateCreated");
            }
        }

        private DateTime _DateCreated;

        [Column]
        public DateTime DateModified
        {
            get { return _DateModified; }
            set
            {
                _DateModified = value;
                MarkColumnModified("DateModified");
            }
        }

        private DateTime _DateModified;

        [Column]
        public string DeviceLimits
        {
            get { return _DeviceLimits; }
            set
            {
                _DeviceLimits = value;
                MarkColumnModified("DeviceLimits");
            }
        }

        private string _DeviceLimits;

        [Column]
        public long Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                MarkColumnModified("Id");
            }
        }

        private long _Id;

        [Column]
        public long ChildId
        {
            get { return _ChildId; }
            set
            {
                _ChildId = value;
                MarkColumnModified("ChildId");
            }
        }

        private long _ChildId;

        [Column]
        public long DeviceId
        {
            get { return _DeviceId; }
            set
            {
                _DeviceId = value;
                MarkColumnModified("DeviceId");
            }
        }

        private long _DeviceId;

        [Column]
        public string IdentifierHash
        {
            get { return _IdentifierHash; }
            set
            {
                _IdentifierHash = value;
                MarkColumnModified("IdentifierHash");
            }
        }

        private string _IdentifierHash;

        [Column]
        public bool IsScreenTimeEnabled
        {
            get { return _IsScreenTimeEnabled; }
            set
            {
                _IsScreenTimeEnabled = value;
                MarkColumnModified("IsScreenTimeEnabled");
            }
        }

        private bool _IsScreenTimeEnabled;

        [Column]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                _ModifiedBy = value;
                MarkColumnModified("ModifiedBy");
            }
        }

        private string _ModifiedBy;

        [Column]
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                MarkColumnModified("Name");
            }
        }

        private string _Name;

    }

    [TableName("[dbo].[DeviceToken]")]
    [PrimaryKey("Id")]
    [ExplicitColumns]
    public partial class DeviceToken
        : Record<DeviceToken>
    {
        [Column]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                _CreatedBy = value;
                MarkColumnModified("CreatedBy");
            }
        }

        private string _CreatedBy;

        [Column]
        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set
            {
                _DateCreated = value;
                MarkColumnModified("DateCreated");
            }
        }

        private DateTime _DateCreated;

        [Column]
        public DateTime DateModified
        {
            get { return _DateModified; }
            set
            {
                _DateModified = value;
                MarkColumnModified("DateModified");
            }
        }

        private DateTime _DateModified;

        [Column]
        public long DeviceId
        {
            get { return _DeviceId; }
            set
            {
                _DeviceId = value;
                MarkColumnModified("DeviceId");
            }
        }

        private long _DeviceId;

        [Column]
        public long Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                MarkColumnModified("Id");
            }
        }

        private long _Id;

        [Column]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                _ModifiedBy = value;
                MarkColumnModified("ModifiedBy");
            }
        }

        private string _ModifiedBy;

        [Column]
        public string TokenString
        {
            get { return _TokenString; }
            set
            {
                _TokenString = value;
                MarkColumnModified("TokenString");
            }
        }

        private string _TokenString;

        [Column]
        public DateTime ValidTill
        {
            get { return _ValidTill; }
            set
            {
                _ValidTill = value;
                MarkColumnModified("ValidTill");
            }
        }

        private DateTime _ValidTill;

        [Column]
        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set
            {
                _IsDeleted = value;
                MarkColumnModified("IsDeleted");
            }
        }

        private bool _IsDeleted;

    }

    [TableName("[dbo].[User]")]
    [PrimaryKey("Id")]
    [ExplicitColumns]
    public partial class User
        : Record<User>
    {
        [Column]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                _CreatedBy = value;
                MarkColumnModified("CreatedBy");
            }
        }

        private string _CreatedBy;

        [Column]
        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set
            {
                _DateCreated = value;
                MarkColumnModified("DateCreated");
            }
        }

        private DateTime _DateCreated;

        [Column]
        public DateTime DateModified
        {
            get { return _DateModified; }
            set
            {
                _DateModified = value;
                MarkColumnModified("DateModified");
            }
        }

        private DateTime _DateModified;

        [Column]
        public long Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                MarkColumnModified("Id");
            }
        }

        private long _Id;

        [Column]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                _ModifiedBy = value;
                MarkColumnModified("ModifiedBy");
            }
        }

        private string _ModifiedBy;

        [Column]
        public string Sub
        {
            get { return _Sub; }
            set
            {
                _Sub = value;
                MarkColumnModified("Sub");
            }
        }

        private string _Sub;

        [Column]
        public string Username
        {
            get { return _Username; }
            set
            {
                _Username = value;
                MarkColumnModified("Username");
            }
        }

        private string _Username;

        [Column]
        public bool IsActivated
        {
            get { return _IsActivated; }
            set
            {
                _IsActivated = value;
                MarkColumnModified("IsActivated");
            }
        }

        private bool _IsActivated;

        [Column]
        public bool IsInvited
        {
            get { return _IsInvited; }
            set
            {
                _IsInvited = value;
                MarkColumnModified("IsInvited");
            }
        }

        private bool _IsInvited;

    }

    [TableName("[dbo].[UserProfile]")]
    [PrimaryKey("Id")]
    [ExplicitColumns]
    public partial class UserProfile
        : Record<UserProfile>
    {
        [Column]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                _CreatedBy = value;
                MarkColumnModified("CreatedBy");
            }
        }

        private string _CreatedBy;

        [Column]
        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set
            {
                _DateCreated = value;
                MarkColumnModified("DateCreated");
            }
        }

        private DateTime _DateCreated;

        [Column]
        public DateTime DateModified
        {
            get { return _DateModified; }
            set
            {
                _DateModified = value;
                MarkColumnModified("DateModified");
            }
        }

        private DateTime _DateModified;

        [Column]
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                MarkColumnModified("Email");
            }
        }

        private string _Email;

        [Column]
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
                MarkColumnModified("FirstName");
            }
        }

        private string _FirstName;

        [Column]
        public long Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                MarkColumnModified("Id");
            }
        }

        private long _Id;

        [Column]
        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
                MarkColumnModified("LastName");
            }
        }

        private string _LastName;

        [Column]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                _ModifiedBy = value;
                MarkColumnModified("ModifiedBy");
            }
        }

        private string _ModifiedBy;

        [Column]
        public long UserId
        {
            get { return _UserId; }
            set
            {
                _UserId = value;
                MarkColumnModified("UserId");
            }
        }

        private long _UserId;

    }

    [TableName("[dbo].[UserProfileView]")]
    [PrimaryKey("Id")]
    [ExplicitColumns]
    public partial class UserProfileView
        : Record<UserProfileView>
    {
        [Column]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                _CreatedBy = value;
                MarkColumnModified("CreatedBy");
            }
        }

        private string _CreatedBy;

        [Column]
        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set
            {
                _DateCreated = value;
                MarkColumnModified("DateCreated");
            }
        }

        private DateTime _DateCreated;

        [Column]
        public DateTime DateModified
        {
            get { return _DateModified; }
            set
            {
                _DateModified = value;
                MarkColumnModified("DateModified");
            }
        }

        private DateTime _DateModified;

        [Column]
        public long Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                MarkColumnModified("Id");
            }
        }

        private long _Id;

        [Column]
        public long ProfileId
        {
            get { return _ProfileId; }
            set
            {
                _ProfileId = value;
                MarkColumnModified("ProfileId");
            }
        }

        private long _ProfileId;

        [Column]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                _ModifiedBy = value;
                MarkColumnModified("ModifiedBy");
            }
        }

        private string _ModifiedBy;

        [Column]
        public string Sub
        {
            get { return _Sub; }
            set
            {
                _Sub = value;
                MarkColumnModified("Sub");
            }
        }

        private string _Sub;

        [Column]
        public string Username
        {
            get { return _Username; }
            set
            {
                _Username = value;
                MarkColumnModified("Username");
            }
        }

        private string _Username;

        [Column]
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                MarkColumnModified("Email");
            }
        }

        private string _Email;

        [Column]
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
                MarkColumnModified("FirstName");
            }
        }

        private string _FirstName;

        [Column]
        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
                MarkColumnModified("LastName");
            }
        }

        private string _LastName;

        [Column]
        public bool IsActivated
        {
            get { return _IsActivated; }
            set
            {
                _IsActivated = value;
                MarkColumnModified("IsActivated");
            }
        }

        private bool _IsActivated;

        [Column]
        public bool IsInvited
        {
            get { return _IsInvited; }
            set
            {
                _IsInvited = value;
                MarkColumnModified("IsInvited");
            }
        }

        private bool _IsInvited;

    }

    [TableName("[dbo].[Child]")]
    [PrimaryKey("Id")]
    [ExplicitColumns]
    public partial class Child
        : Record<Child>
    {
        [Column]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                _CreatedBy = value;
                MarkColumnModified("CreatedBy");
            }
        }

        private string _CreatedBy;

        [Column]
        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set
            {
                _DateCreated = value;
                MarkColumnModified("DateCreated");
            }
        }

        private DateTime _DateCreated;

        [Column]
        public DateTime DateModified
        {
            get { return _DateModified; }
            set
            {
                _DateModified = value;
                MarkColumnModified("DateModified");
            }
        }

        private DateTime _DateModified;

        [Column]
        public string DeviceLimits
        {
            get { return _DeviceLimits; }
            set
            {
                _DeviceLimits = value;
                MarkColumnModified("DeviceLimits");
            }
        }

        private string _DeviceLimits;

        [Column]
        public long Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                MarkColumnModified("Id");
            }
        }

        private long _Id;

        [Column]
        public string Username
        {
            get { return _Username; }
            set
            {
                _Username = value;
                MarkColumnModified("Username");
            }
        }

        private string _Username;

        [Column]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                _ModifiedBy = value;
                MarkColumnModified("ModifiedBy");
            }
        }

        private string _ModifiedBy;

        [Column]
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
                MarkColumnModified("FirstName");
            }
        }

        private string _FirstName;

        [Column]
        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
                MarkColumnModified("LastName");
            }
        }

        private string _LastName;

        [Column]
        public string MiddleName
        {
            get { return _MiddleName; }
            set
            {
                _MiddleName = value;
                MarkColumnModified("MiddleName");
            }
        }

        private string _MiddleName;

        [Column]
        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set
            {
                _IsDeleted = value;
                MarkColumnModified("IsDeleted");
            }
        }

        private bool _IsDeleted;

    }

    [TableName("[dbo].[DeviceTokenView]")]
    [PrimaryKey("Id")]
    [ExplicitColumns]
    public partial class DeviceTokenView
        : Record<DeviceTokenView>
    {
        [Column]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                _CreatedBy = value;
                MarkColumnModified("CreatedBy");
            }
        }

        private string _CreatedBy;

        [Column]
        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set
            {
                _DateCreated = value;
                MarkColumnModified("DateCreated");
            }
        }

        private DateTime _DateCreated;

        [Column]
        public DateTime DateModified
        {
            get { return _DateModified; }
            set
            {
                _DateModified = value;
                MarkColumnModified("DateModified");
            }
        }

        private DateTime _DateModified;

        [Column]
        public long Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                MarkColumnModified("Id");
            }
        }

        private long _Id;

        [Column]
        public string IdentifierHash
        {
            get { return _IdentifierHash; }
            set
            {
                _IdentifierHash = value;
                MarkColumnModified("IdentifierHash");
            }
        }

        private string _IdentifierHash;

        [Column]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                _ModifiedBy = value;
                MarkColumnModified("ModifiedBy");
            }
        }

        private string _ModifiedBy;

        [Column]
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                MarkColumnModified("Name");
            }
        }

        private string _Name;

        [Column]
        public string TokenString
        {
            get { return _TokenString; }
            set
            {
                _TokenString = value;
                MarkColumnModified("TokenString");
            }
        }

        private string _TokenString;

        [Column]
        public DateTime ValidTill
        {
            get { return _ValidTill; }
            set
            {
                _ValidTill = value;
                MarkColumnModified("ValidTill");
            }
        }

        private DateTime _ValidTill;

    }

    [TableName("[dbo].[ChildDeviceMapping]")]
    [PrimaryKey("Id")]
    [ExplicitColumns]
    public partial class ChildDeviceMapping
        : Record<ChildDeviceMapping>
    {
        [Column]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                _CreatedBy = value;
                MarkColumnModified("CreatedBy");
            }
        }

        private string _CreatedBy;

        [Column]
        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set
            {
                _DateCreated = value;
                MarkColumnModified("DateCreated");
            }
        }

        private DateTime _DateCreated;

        [Column]
        public DateTime DateModified
        {
            get { return _DateModified; }
            set
            {
                _DateModified = value;
                MarkColumnModified("DateModified");
            }
        }

        private DateTime _DateModified;

        [Column]
        public string DeviceLimits
        {
            get { return _DeviceLimits; }
            set
            {
                _DeviceLimits = value;
                MarkColumnModified("DeviceLimits");
            }
        }

        private string _DeviceLimits;

        [Column]
        public long Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                MarkColumnModified("Id");
            }
        }

        private long _Id;

        [Column]
        public long DeviceId
        {
            get { return _DeviceId; }
            set
            {
                _DeviceId = value;
                MarkColumnModified("DeviceId");
            }
        }

        private long _DeviceId;

        [Column]
        public long ChildId
        {
            get { return _ChildId; }
            set
            {
                _ChildId = value;
                MarkColumnModified("ChildId");
            }
        }

        private long _ChildId;

        [Column]
        public bool IsScreenTimeEnabled
        {
            get { return _IsScreenTimeEnabled; }
            set
            {
                _IsScreenTimeEnabled = value;
                MarkColumnModified("IsScreenTimeEnabled");
            }
        }

        private bool _IsScreenTimeEnabled;

        [Column]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                _ModifiedBy = value;
                MarkColumnModified("ModifiedBy");
            }
        }

        private string _ModifiedBy;

        [Column]
        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set
            {
                _IsDeleted = value;
                MarkColumnModified("IsDeleted");
            }
        }

        private bool _IsDeleted;

    }

}

