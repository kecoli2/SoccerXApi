
# ⚽ SoccerX Veritabanı Dökümantasyonu

Bu dokümantasyon, SoccerX platformunun veritabanı yapısını açıklamaktadır.  
Her tablo için Türkçe açıklama, kolon isimleri, veri tipleri ve **İngilizce açıklamalar (description)** sağlanmıştır.

---

## 1. Users Tablosu

> Platforma kayıtlı kullanıcı bilgilerini içerir.

### Kolonlar

| Kolon Adı       | Veri Tipi         | Açıklama                                          |
|------------------|--------------------|---------------------------------------------------|
| Id               | UUID               | Unique identifier for the user                   |
| Username         | VARCHAR(50)        | Unique username                                  |
| Email            | VARCHAR(100)       | User's email address                             |
| PasswordHash     | TEXT               | Hashed password                                  |
| Role             | UserRole (enum)    | Role of the user (User, Editor, Admin)           |
| Status           | UserStatus (enum)  | Account status (Active, Banned)                  |
| BanEndDate       | TIMESTAMP          | Ban expiration date                              |
| ReferralUserId   | UUID               | ID of the referring user (nullable)              |
| FollowerCount    | INT                | Cached number of followers                       |
| CountryId        | UUID               | Reference to Countries table                     |
| CityId           | UUID               | Reference to Cities table                        |
| PostalCode       | VARCHAR(20)        | Optional postal code                             |
| Address          | TEXT               | Full address of the user                         |
| PhoneNumber      | VARCHAR(20)        | Unique phone number                              |
| AvatarUrl        | TEXT               | URL of the user's profile image (nullable)       |
| CreateDate       | TIMESTAMP          | Record creation date                             |
| UpdateDate       | TIMESTAMP (nullable) | Last update timestamp                         |
| IsDeleted        | BOOLEAN            | Soft delete flag (TRUE if deleted)               |

### Açıklama

- `Role` ve `Status` alanları PostgreSQL `ENUM` olarak tanımlanmıştır.
- `CountryId` ve `CityId` alanları ilgili coğrafi tablolara `FOREIGN KEY` olarak bağlıdır.
- `ReferralUserId`, kullanıcıların referans sistemi üzerinden birbirini davet etmesini sağlar.
- `AvatarUrl`, kullanıcının profil fotoğrafı linkini barındırır.
- `IsDeleted` alanı soft delete mekanizması için kullanılır.



---

## 2. Followers Tablosu

> Kullanıcıların birbirini takip etme ilişkilerini tutar.

### Kolonlar

| Kolon Adı   | Veri Tipi | Açıklama                        |
|-------------|-----------|----------------------------------|
| FollowerId  | UUID      | The ID of the user who follows  |
| FollowingId | UUID      | The ID of the followed user     |

### Açıklama
- `FollowerId` ve `FollowingId` birlikte birincil anahtardır.
- Her iki alan da `Users` tablosuna `FOREIGN KEY` olarak bağlıdır.

---

## 3. BlockedUsers Tablosu

> Kullanıcıların birbirini engelleme ilişkilerini tutar.

### Kolonlar

| Kolon Adı   | Veri Tipi | Açıklama                        |
|-------------|-----------|----------------------------------|
| BlockerId   | UUID      | The user who blocked            |
| BlockedId   | UUID      | The user who was blocked        |

### Açıklama
- `BlockerId` ve `BlockedId` birlikte birincil anahtardır.
- Her iki alan da `Users` tablosuna `FOREIGN KEY` olarak bağlıdır.

---

## 4. Transactions Tablosu

> Kullanıcılara ait para hareketlerini saklar.

### Kolonlar

| Kolon Adı       | Veri Tipi       | Açıklama                                          |
|------------------|------------------|---------------------------------------------------|
| Id               | UUID              | Unique identifier for the transaction            |
| UserId           | UUID              | Related user ID                                  |
| Amount           | DECIMAL(10,2)     | Amount of transaction                            |
| TransactionType  | TransactionType   | Type of the transaction                          |
| ReferenceId      | UUID              | Related entity (nullable)                        |
| CreateDate       | TIMESTAMP         | Creation timestamp                               |
| UpdateDate       | TIMESTAMP (nullable) | Last update timestamp                        |
| IsDeleted        | BOOLEAN           | Soft delete flag                                 |

### Açıklama
- `TransactionType` enum değeridir: `Deposit`, `Withdrawal`, `Subscription`, `BetSlipPurchase`.

---

## 5. BetSlips Tablosu

> Kullanıcılar tarafından oluşturulan futbol kuponlarını saklar.

### Kolonlar

| Kolon Adı     | Veri Tipi     | Açıklama                                          |
|----------------|----------------|---------------------------------------------------|
| Id             | UUID            | Unique identifier for the bet slip               |
| UserId         | UUID            | ID of the user who created the slip              |
| IsPremium      | BOOLEAN         | Whether it is a premium bet slip                 |
| LikeCount      | INT             | Cached number of likes                           |
| CommentCount   | INT             | Cached number of comments                        |
| Status         | BetSlipStatus   | Status of the bet slip (Pending, Won, Lost)      |
| CreateDate     | TIMESTAMP       | Creation timestamp                               |
| UpdateDate     | TIMESTAMP (nullable) | Last update timestamp                        |
| IsDeleted      | BOOLEAN         | Soft delete flag                                 |

### Açıklama
- `Status` enum değeridir ve kuponun durumunu belirtir.
- `LikeCount` ve `CommentCount` performans için cache'lenmiş sayılardır.



---

## 6. Comments Tablosu

> Kuponlara yapılan kullanıcı yorumlarını saklar.

### Kolonlar

| Kolon Adı   | Veri Tipi | Açıklama                                      |
|-------------|-----------|-----------------------------------------------|
| Id          | UUID      | Unique identifier for the comment             |
| UserId      | UUID      | ID of the user who wrote the comment          |
| BetSlipId   | UUID      | ID of the bet slip being commented on         |
| Content     | TEXT      | Content of the comment                        |
| CreateDate  | TIMESTAMP | Creation timestamp                            |
| UpdateDate  | TIMESTAMP (nullable) | Last update timestamp             |
| IsDeleted   | BOOLEAN   | Soft delete flag                              |

---

## 7. Likes Tablosu

> Kuponlara yapılan beğenileri saklar.

### Kolonlar

| Kolon Adı   | Veri Tipi | Açıklama                                    |
|-------------|-----------|---------------------------------------------|
| Id          | UUID      | Unique identifier for the like              |
| UserId      | UUID      | ID of the user who liked the bet slip       |
| BetSlipId   | UUID      | ID of the liked bet slip                    |
| CreateDate  | TIMESTAMP | Creation timestamp                          |
| UpdateDate  | TIMESTAMP (nullable) | Last update timestamp           |
| IsDeleted   | BOOLEAN   | Soft delete flag                            |

---

## 8. Teams Tablosu

> Takım bilgilerini ve sistemsel etiketleri saklar.

### Kolonlar

| Kolon Adı   | Veri Tipi     | Açıklama                                          |
|-------------|---------------|---------------------------------------------------|
| Id          | UUID          | Unique identifier for the team                   |
| Name        | VARCHAR(50)   | Name of the team                                 |
| Country     | VARCHAR(50)   | Country of the team                              |
| Tags        | JSONB         | System tags (e.g. league, AI rank, style)        |
| CreateDate  | TIMESTAMP     | Creation timestamp                               |
| UpdateDate  | TIMESTAMP (nullable) | Last update timestamp                   |
| IsDeleted   | BOOLEAN       | Soft delete flag                                 |

---

## 9. Notifications Tablosu

> Kullanıcılara gönderilen bildirimleri içerir.

### Kolonlar

| Kolon Adı   | Veri Tipi | Açıklama                                     |
|-------------|-----------|----------------------------------------------|
| Id          | UUID      | Unique identifier for the notification       |
| UserId      | UUID      | ID of the user who will receive notification |
| Message     | TEXT      | Notification message                         |
| IsRead      | BOOLEAN   | Whether the notification is read             |
| CreateDate  | TIMESTAMP | Creation timestamp                           |
| UpdateDate  | TIMESTAMP (nullable) | Last update timestamp             |
| IsDeleted   | BOOLEAN   | Soft delete flag                             |



---

## 10. Subscriptions Tablosu

> Premium ve editör aboneliklerini tutar.

### Kolonlar

| Kolon Adı     | Veri Tipi | Açıklama                                           |
|----------------|-----------|----------------------------------------------------|
| Id             | UUID      | Unique identifier for the subscription            |
| SubscriberId   | UUID      | User ID who subscribed                            |
| EditorId       | UUID      | Editor or premium content provider ID             |
| StartDate      | TIMESTAMP | Subscription start date                           |
| EndDate        | TIMESTAMP | Subscription end date                             |
| IsActive       | BOOLEAN   | Whether the subscription is currently active      |
| CreateDate     | TIMESTAMP | Creation timestamp                                |
| UpdateDate     | TIMESTAMP (nullable) | Last update timestamp                 |
| IsDeleted      | BOOLEAN   | Soft delete flag                                  |

---

## 11. Payments Tablosu

> Kullanıcıların ödeme kayıtlarını içerir.

### Kolonlar

| Kolon Adı       | Veri Tipi        | Açıklama                                         |
|------------------|------------------|--------------------------------------------------|
| Id               | UUID             | Unique identifier for the payment               |
| UserId           | UUID             | ID of the user who made the payment             |
| PaymentMethod    | PaymentMethod    | Payment method (CreditCard, PayPal, Crypto)     |
| Amount           | DECIMAL(10,2)    | Amount paid                                     |
| PaymentDate      | TIMESTAMP        | Date of the payment                             |
| PaymentStatus    | PaymentStatus    | Status of the payment (Pending, Completed, etc.)|
| CreateDate       | TIMESTAMP        | Creation timestamp                              |
| UpdateDate       | TIMESTAMP (nullable) | Last update timestamp                     |
| IsDeleted        | BOOLEAN          | Soft delete flag                                |

---

## 12. AuditLog Tablosu

> Sistem ve kullanıcı işlemlerini takip eder.

### Kolonlar

| Kolon Adı     | Veri Tipi     | Açıklama                                        |
|----------------|---------------|-------------------------------------------------|
| Id             | UUID          | Unique identifier for the audit record         |
| EntityName     | VARCHAR(50)   | Name of the affected entity                    |
| EntityId       | UUID          | ID of the affected record                      |
| Action         | AuditAction   | Type of action (Create, Update, Delete, etc.)  |
| PerformedBy    | UUID          | ID of the user who performed the action        |
| Timestamp      | TIMESTAMP     | Timestamp of the action                        |
| Details        | TEXT          | Additional details                             |
| CreateDate     | TIMESTAMP     | Creation timestamp                             |
| UpdateDate     | TIMESTAMP (nullable) | Last update timestamp                   |
| IsDeleted      | BOOLEAN       | Soft delete flag                               |

---

## 13. ReferralRewards Tablosu

> Referans sistemi üzerinden kazanılan ödülleri saklar.

### Kolonlar

| Kolon Adı     | Veri Tipi     | Açıklama                                  |
|----------------|---------------|-------------------------------------------|
| Id             | UUID          | Unique identifier for the reward          |
| UserId         | UUID          | ID of the invited user                    |
| ReferrerId     | UUID          | ID of the user who gave the referral      |
| Amount         | DECIMAL(10,2) | Amount of the reward                      |
| Status         | ReferralStatus| Status of the reward (Pending, Paid)      |
| CreateDate     | TIMESTAMP     | Creation timestamp                        |
| UpdateDate     | TIMESTAMP (nullable) | Last update timestamp               |
| IsDeleted      | BOOLEAN       | Soft delete flag                          |

---

## 14. Countries Tablosu

> Ülke bilgilerini ve telefon kodlarını içerir.

### Kolonlar

| Kolon Adı     | Veri Tipi     | Açıklama                                 |
|----------------|---------------|------------------------------------------|
| Id             | UUID          | Unique identifier for the country        |
| Name           | VARCHAR(100)  | Country name                             |
| CountryCode    | VARCHAR(10)   | International dialing code (e.g. +90)    |
| CreateDate     | TIMESTAMP     | Creation timestamp                       |
| UpdateDate     | TIMESTAMP (nullable) | Last update timestamp              |

---

## 15. Cities Tablosu

> Şehir bilgilerini saklar.

### Kolonlar

| Kolon Adı     | Veri Tipi     | Açıklama                                 |
|----------------|---------------|------------------------------------------|
| Id             | UUID          | Unique identifier for the city           |
| Name           | VARCHAR(100)  | Name of the city                         |
| CountryId      | UUID          | Reference to the Countries table         |
| CreateDate     | TIMESTAMP     | Creation timestamp                       |
| UpdateDate     | TIMESTAMP (nullable) | Last update timestamp              |

