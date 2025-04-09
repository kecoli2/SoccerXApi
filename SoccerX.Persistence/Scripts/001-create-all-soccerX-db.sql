-- Gerekli extension
CREATE EXTENSION IF NOT EXISTS "pgcrypto";

-- ENUM Tanımları
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'userrole') THEN
        CREATE TYPE UserRole AS ENUM ('User', 'Admin');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'userstatus') THEN
        CREATE TYPE UserStatus AS ENUM ('Active', 'Banned');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'transactiontype') THEN
        CREATE TYPE TransactionType AS ENUM ('Deposit', 'Withdraw', 'Bet', 'Win');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'betslipstatus') THEN
        CREATE TYPE BetSlipStatus AS ENUM ('Pending', 'Won', 'Lost');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'paymentmethod') THEN
        CREATE TYPE PaymentMethod AS ENUM ('CreditCard', 'Crypto', 'BankTransfer');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'paymentstatus') THEN
        CREATE TYPE PaymentStatus AS ENUM ('Pending', 'Completed', 'Failed');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'auditaction') THEN
        CREATE TYPE AuditAction AS ENUM ('Create', 'Update', 'Delete');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'referralstatus') THEN
        CREATE TYPE ReferralStatus AS ENUM ('Pending', 'Approved', 'Rejected');
    END IF;
END
$$;

-- TABLE: Countries
CREATE TABLE IF NOT EXISTS Countries (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    Name VARCHAR(100) NOT NULL UNIQUE,
    CountryCode VARCHAR(10) NOT NULL UNIQUE,
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdateDate TIMESTAMP DEFAULT NULL,
    PhoneRegex VARCHAR(100),
    PhoneMask VARCHAR(100)
);

-- TABLE: Cities
CREATE TABLE IF NOT EXISTS Cities (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    Name VARCHAR(100) NOT NULL,
    CountryId UUID NOT NULL REFERENCES Countries(Id) ON DELETE CASCADE,
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdateDate TIMESTAMP DEFAULT NULL,
    CONSTRAINT Unique_City_Country UNIQUE (Name, CountryId)    
);

-- TABLE: Users
CREATE TABLE IF NOT EXISTS Users (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    Username VARCHAR(50) NOT NULL UNIQUE,
    Email VARCHAR(100) NOT NULL UNIQUE,
    PasswordHash TEXT NOT NULL,
    Role UserRole NOT NULL DEFAULT 'User',
    Status UserStatus NOT NULL DEFAULT 'Active',
    BanEndDate TIMESTAMP NULL,
    ReferralUserId UUID NULL REFERENCES Users(Id) ON DELETE SET NULL,
    FollowerCount INT NOT NULL DEFAULT 0,
    CountryId UUID NOT NULL REFERENCES Countries(Id) ON DELETE RESTRICT,
    CityId UUID NOT NULL REFERENCES Cities(Id) ON DELETE RESTRICT,
    PostalCode VARCHAR(20),
    Address TEXT NOT NULL,
    PhoneNumber VARCHAR(20) NOT NULL UNIQUE,
    AvatarUrl TEXT,
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdateDate TIMESTAMP DEFAULT NULL,
    IsDeleted BOOLEAN DEFAULT FALSE
);

-- TABLE: Followers
CREATE TABLE IF NOT EXISTS Followers (
    FollowerId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    FollowingId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    PRIMARY KEY (FollowerId, FollowingId)
);

-- TABLE: BlockedUsers
CREATE TABLE IF NOT EXISTS BlockedUsers (
    BlockerId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    BlockedId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    PRIMARY KEY (BlockerId, BlockedId)
);

-- TABLE: Transactions
CREATE TABLE IF NOT EXISTS Transactions (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UserId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    Amount DECIMAL(10,2) NOT NULL,
    TransactionType TransactionType NOT NULL,
    ReferenceId UUID NULL,
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdateDate TIMESTAMP DEFAULT NULL,
    IsDeleted BOOLEAN DEFAULT FALSE
);

-- TABLE: BetSlips
CREATE TABLE IF NOT EXISTS BetSlips (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UserId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    IsPremium BOOLEAN NOT NULL DEFAULT FALSE,
    LikeCount INT NOT NULL DEFAULT 0,
    CommentCount INT NOT NULL DEFAULT 0,
    Status BetSlipStatus NOT NULL DEFAULT 'Pending',
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdateDate TIMESTAMP DEFAULT NULL,
    IsDeleted BOOLEAN DEFAULT FALSE
);

-- TABLE: Comments
CREATE TABLE IF NOT EXISTS Comments (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UserId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    BetSlipId UUID NOT NULL REFERENCES BetSlips(Id) ON DELETE CASCADE,
    Content TEXT NOT NULL,
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdateDate TIMESTAMP DEFAULT NULL,
    IsDeleted BOOLEAN DEFAULT FALSE
);

-- TABLE: Likes
CREATE TABLE IF NOT EXISTS Likes (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UserId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    BetSlipId UUID NOT NULL REFERENCES BetSlips(Id) ON DELETE CASCADE,
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdateDate TIMESTAMP DEFAULT NULL,
    IsDeleted BOOLEAN DEFAULT FALSE
);

-- TABLE: Teams
CREATE TABLE IF NOT EXISTS Teams (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    Name VARCHAR(50) NOT NULL UNIQUE,
    Country VARCHAR(50) NOT NULL,
    Tags JSONB,
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdateDate TIMESTAMP DEFAULT NULL,
    IsDeleted BOOLEAN DEFAULT FALSE
);

-- TABLE: Notifications
CREATE TABLE IF NOT EXISTS Notifications (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UserId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    Message TEXT NOT NULL,
    IsRead BOOLEAN NOT NULL DEFAULT FALSE,
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdateDate TIMESTAMP DEFAULT NULL,
    IsDeleted BOOLEAN DEFAULT FALSE
);

-- TABLE: Subscriptions
CREATE TABLE IF NOT EXISTS Subscriptions (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    SubscriberId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    EditorId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    StartDate TIMESTAMP NOT NULL,
    EndDate TIMESTAMP NOT NULL,
    IsActive BOOLEAN NOT NULL DEFAULT TRUE,
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdateDate TIMESTAMP DEFAULT NULL,
    IsDeleted BOOLEAN DEFAULT FALSE
);

-- TABLE: Payments
CREATE TABLE IF NOT EXISTS Payments (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UserId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    PaymentMethod PaymentMethod NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    PaymentDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PaymentStatus PaymentStatus NOT NULL DEFAULT 'Pending',
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdateDate TIMESTAMP DEFAULT NULL,
    IsDeleted BOOLEAN DEFAULT FALSE
);

-- TABLE: AuditLog
CREATE TABLE IF NOT EXISTS AuditLog (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    EntityName VARCHAR(50) NOT NULL,
    EntityId UUID NOT NULL,
    Action AuditAction NOT NULL,
    PerformedBy UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    Timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    Details TEXT,
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdateDate TIMESTAMP DEFAULT NULL,
    IsDeleted BOOLEAN DEFAULT FALSE
);

-- TABLE: ReferralRewards
CREATE TABLE IF NOT EXISTS ReferralRewards (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UserId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    ReferrerId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    Amount DECIMAL(10,2) NOT NULL,
    Status ReferralStatus NOT NULL DEFAULT 'Pending',
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdateDate TIMESTAMP DEFAULT NULL,
    IsDeleted BOOLEAN DEFAULT FALSE
);