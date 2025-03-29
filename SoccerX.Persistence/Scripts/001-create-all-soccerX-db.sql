-- ENUM Tanımlamaları
CREATE TYPE UserRole AS ENUM ('User', 'Editor', 'Admin');
CREATE TYPE UserStatus AS ENUM ('Active', 'Banned');
CREATE TYPE TransactionType AS ENUM ('Deposit', 'Withdrawal', 'Subscription', 'BetSlipPurchase');
CREATE TYPE BetSlipStatus AS ENUM ('Pending', 'Won', 'Lost');
CREATE TYPE PaymentMethod AS ENUM ('CreditCard', 'PayPal', 'Crypto');
CREATE TYPE PaymentStatus AS ENUM ('Pending', 'Completed', 'Failed', 'Refunded');
CREATE TYPE ReferralStatus AS ENUM ('Pending', 'Paid');
CREATE TYPE AuditAction AS ENUM ('Create', 'Update', 'Delete', 'Restore');

-- Countries Table
CREATE TABLE Countries (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    Name VARCHAR(100) NOT NULL UNIQUE,
    CountryCode VARCHAR(10) NOT NULL UNIQUE,
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdateDate TIMESTAMP DEFAULT null
);

-- Cities Table
CREATE TABLE Cities (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    Name VARCHAR(100) NOT NULL,
    CountryId UUID NOT NULL REFERENCES Countries(Id) ON DELETE CASCADE,
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdateDate TIMESTAMP DEFAULT null,
    CONSTRAINT Unique_City_Country UNIQUE (Name, CountryId)
);

-- Users Table
CREATE TABLE Users (
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
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdateDate TIMESTAMP DEFAULT null,
    IsDeleted BOOLEAN DEFAULT FALSE
);

-- Followers Table
CREATE TABLE Followers (
    FollowerId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    FollowingId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    PRIMARY KEY (FollowerId, FollowingId)
);

-- BlockedUsers Table
CREATE TABLE BlockedUsers (
    BlockerId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    BlockedId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    PRIMARY KEY (BlockerId, BlockedId)
);

-- Transactions Table
CREATE TABLE Transactions (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UserId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    Amount DECIMAL(10,2) NOT NULL,
    TransactionType TransactionType NOT NULL,
    ReferenceId UUID NULL,
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdateDate TIMESTAMP DEFAULT null,
    IsDeleted BOOLEAN DEFAULT FALSE
);

-- BetSlips Table
CREATE TABLE BetSlips (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UserId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    IsPremium BOOLEAN NOT NULL DEFAULT FALSE,
    LikeCount INT NOT NULL DEFAULT 0,
    CommentCount INT NOT NULL DEFAULT 0,
    Status BetSlipStatus NOT NULL DEFAULT 'Pending',
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdateDate TIMESTAMP DEFAULT null,
    IsDeleted BOOLEAN DEFAULT FALSE
);

-- Comments Table
CREATE TABLE Comments (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UserId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    BetSlipId UUID NOT NULL REFERENCES BetSlips(Id) ON DELETE CASCADE,
    Content TEXT NOT NULL,
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdateDate TIMESTAMP DEFAULT null,
    IsDeleted BOOLEAN DEFAULT FALSE
);

-- Likes Table
CREATE TABLE Likes (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UserId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    BetSlipId UUID NOT NULL REFERENCES BetSlips(Id) ON DELETE CASCADE,
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdateDate TIMESTAMP DEFAULT null,
    IsDeleted BOOLEAN DEFAULT FALSE
);

-- Teams Table
CREATE TABLE Teams (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    Name VARCHAR(50) NOT NULL UNIQUE,
    Country VARCHAR(50) NOT NULL,
    Tags JSONB,
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdateDate TIMESTAMP DEFAULT null,
    IsDeleted BOOLEAN DEFAULT FALSE
);

-- Notifications Table
CREATE TABLE Notifications (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UserId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    Message TEXT NOT NULL,
    IsRead BOOLEAN NOT NULL DEFAULT FALSE,
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdateDate TIMESTAMP DEFAULT null,
    IsDeleted BOOLEAN DEFAULT FALSE
);

-- Subscriptions Table
CREATE TABLE Subscriptions (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    SubscriberId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    EditorId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    StartDate TIMESTAMP NOT NULL,
    EndDate TIMESTAMP NOT NULL,
    IsActive BOOLEAN NOT NULL DEFAULT TRUE,
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdateDate TIMESTAMP DEFAULT null,
    IsDeleted BOOLEAN DEFAULT FALSE
);

-- Payments Table
CREATE TABLE Payments (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UserId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    PaymentMethod PaymentMethod NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    PaymentDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PaymentStatus PaymentStatus NOT NULL DEFAULT 'Pending',
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdateDate TIMESTAMP DEFAULT null,
    IsDeleted BOOLEAN DEFAULT FALSE
);

-- AuditLog Table
CREATE TABLE AuditLog (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    EntityName VARCHAR(50) NOT NULL,
    EntityId UUID NOT NULL,
    Action AuditAction NOT NULL,
    PerformedBy UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    Timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    Details TEXT,
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdateDate TIMESTAMP DEFAULT null,
    IsDeleted BOOLEAN DEFAULT FALSE
);

-- ReferralRewards Table
CREATE TABLE ReferralRewards (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UserId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    ReferrerId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    Amount DECIMAL(10,2) NOT NULL,
    Status ReferralStatus NOT NULL DEFAULT 'Pending',
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdateDate TIMESTAMP DEFAULT null,
    IsDeleted BOOLEAN DEFAULT FALSE
);

-- Türkiye ülkesini ekle
INSERT INTO public.countries (id, name, countrycode, createdate, updatedate)
VALUES (
    gen_random_uuid(),
    'Türkiye',
    '+90',
    CURRENT_TIMESTAMP,
    null
);

-- Türkiye'ye ait şehirleri ekle
DO $$
DECLARE
    turkey_id UUID;
BEGIN
    SELECT id INTO turkey_id FROM public.countries WHERE name = 'Türkiye';

    IF turkey_id IS NOT NULL THEN
        INSERT INTO public.cities (id, name, countryid, createdate, updatedate)
        VALUES 
            (gen_random_uuid(), 'Adana', turkey_id, CURRENT_TIMESTAMP, null),
            (gen_random_uuid(), 'Adıyaman', turkey_id, CURRENT_TIMESTAMP, null),
            (gen_random_uuid(), 'Afyonkarahisar', turkey_id, CURRENT_TIMESTAMP, null),
            (gen_random_uuid(), 'Ağrı', turkey_id, CURRENT_TIMESTAMP, null),
            (gen_random_uuid(), 'Amasya', turkey_id, CURRENT_TIMESTAMP, null),
            (gen_random_uuid(), 'Ankara', turkey_id, CURRENT_TIMESTAMP, null),
            (gen_random_uuid(), 'Antalya', turkey_id, CURRENT_TIMESTAMP, null),
            (gen_random_uuid(), 'Artvin', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Aydın', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Balıkesir', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Bilecik', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Bingöl', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Bitlis', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Bolu', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Burdur', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Bursa', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Çanakkale', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Çankırı', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Çorum', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Denizli', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Diyarbakır', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Edirne', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Elazığ', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Erzincan', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Erzurum', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Eskişehir', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Gaziantep', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Giresun', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Gümüşhane', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Hakkari', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Hatay', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Isparta', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Mersin', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'İstanbul', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'İzmir', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Kars', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Kastamonu', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Kayseri', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Kırklareli', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Kırşehir', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Kocaeli', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Konya', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Kütahya', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Malatya', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Manisa', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Kahramanmaraş', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Mardin', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Muğla', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Muş', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Nevşehir', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Niğde', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Ordu', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Rize', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Sakarya', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Samsun', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Siirt', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Sinop', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Sivas', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Tekirdağ', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Tokat', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Trabzon', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Tunceli', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Şanlıurfa', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Uşak', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Van', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Yozgat', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Zonguldak', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Aksaray', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Bayburt', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Karaman', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Kırıkkale', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Batman', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Şırnak', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Bartın', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Ardahan', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Iğdır', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Yalova', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Karabük', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Kilis', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Osmaniye', turkey_id, CURRENT_TIMESTAMP,null),
            (gen_random_uuid(), 'Düzce', turkey_id, CURRENT_TIMESTAMP,null);
    END IF;
END $$;
