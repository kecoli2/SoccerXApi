-- Türkiye ülkesini ekle
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Türkiye', 'TR', '^\+90[ ]?5\d{2}[ ]?\d{3}[ ]?\d{2}[ ]?\d{2}$', '+90 (5##) ### ## ##')  RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES 
('Adana', (SELECT CountryId FROM inserted_country)),
('Adıyaman', (SELECT CountryId FROM inserted_country)),
('Afyonkarahisar', (SELECT CountryId FROM inserted_country)),
('Ağrı', (SELECT CountryId FROM inserted_country)),
('Amasya', (SELECT CountryId FROM inserted_country)),
('Ankara', (SELECT CountryId FROM inserted_country)),
('Antalya', (SELECT CountryId FROM inserted_country)),
('Artvin', (SELECT CountryId FROM inserted_country)),
('Aydın', (SELECT CountryId FROM inserted_country)),
('Balıkesir', (SELECT CountryId FROM inserted_country)),
('Bilecik', (SELECT CountryId FROM inserted_country)),
('Bingöl', (SELECT CountryId FROM inserted_country)),
('Bitlis', (SELECT CountryId FROM inserted_country)),
('Bolu', (SELECT CountryId FROM inserted_country)),
('Burdur', (SELECT CountryId FROM inserted_country)),
('Bursa', (SELECT CountryId FROM inserted_country)),
('Çanakkale', (SELECT CountryId FROM inserted_country)),
('Çankırı', (SELECT CountryId FROM inserted_country)),
('Çorum', (SELECT CountryId FROM inserted_country)),
('Denizli', (SELECT CountryId FROM inserted_country)),
('Diyarbakır', (SELECT CountryId FROM inserted_country)),
('Edirne', (SELECT CountryId FROM inserted_country)),
('Elazığ', (SELECT CountryId FROM inserted_country)),
('Erzincan', (SELECT CountryId FROM inserted_country)),
('Erzurum', (SELECT CountryId FROM inserted_country)),
('Eskişehir', (SELECT CountryId FROM inserted_country)),
('Gaziantep', (SELECT CountryId FROM inserted_country)),
('Giresun', (SELECT CountryId FROM inserted_country)),
('Gümüşhane', (SELECT CountryId FROM inserted_country)),
('Hakkari', (SELECT CountryId FROM inserted_country)),
('Hatay', (SELECT CountryId FROM inserted_country)),
('Isparta', (SELECT CountryId FROM inserted_country)),
('Mersin', (SELECT CountryId FROM inserted_country)),
('İstanbul', (SELECT CountryId FROM inserted_country)),
('İzmir', (SELECT CountryId FROM inserted_country)),
('Kars', (SELECT CountryId FROM inserted_country)),
('Kastamonu', (SELECT CountryId FROM inserted_country)),
('Kayseri', (SELECT CountryId FROM inserted_country)),
('Kırklareli', (SELECT CountryId FROM inserted_country)),
('Kırşehir', (SELECT CountryId FROM inserted_country)),
('Kocaeli', (SELECT CountryId FROM inserted_country)),
('Konya', (SELECT CountryId FROM inserted_country)),
('Kütahya', (SELECT CountryId FROM inserted_country)),
('Malatya', (SELECT CountryId FROM inserted_country)),
('Manisa', (SELECT CountryId FROM inserted_country)),
('Kahramanmaraş', (SELECT CountryId FROM inserted_country)),
('Mardin', (SELECT CountryId FROM inserted_country)),
('Muğla', (SELECT CountryId FROM inserted_country)),
('Muş', (SELECT CountryId FROM inserted_country)),
('Nevşehir', (SELECT CountryId FROM inserted_country)),
('Niğde', (SELECT CountryId FROM inserted_country)),
('Ordu', (SELECT CountryId FROM inserted_country)),
('Rize', (SELECT CountryId FROM inserted_country)),
('Sakarya', (SELECT CountryId FROM inserted_country)),
('Samsun', (SELECT CountryId FROM inserted_country)),
('Siirt', (SELECT CountryId FROM inserted_country)),
('Sinop', (SELECT CountryId FROM inserted_country)),
('Sivas', (SELECT CountryId FROM inserted_country)),
('Tekirdağ', (SELECT CountryId FROM inserted_country)),
('Tokat', (SELECT CountryId FROM inserted_country)),
('Trabzon', (SELECT CountryId FROM inserted_country)),
('Tunceli', (SELECT CountryId FROM inserted_country)),
('Şanlıurfa', (SELECT CountryId FROM inserted_country)),
('Uşak', (SELECT CountryId FROM inserted_country)),
('Van', (SELECT CountryId FROM inserted_country)),
('Yozgat', (SELECT CountryId FROM inserted_country)),
('Zonguldak', (SELECT CountryId FROM inserted_country)),
('Aksaray', (SELECT CountryId FROM inserted_country)),
('Bayburt', (SELECT CountryId FROM inserted_country)),
('Karaman', (SELECT CountryId FROM inserted_country)),
('Kırıkkale', (SELECT CountryId FROM inserted_country)),
('Batman', (SELECT CountryId FROM inserted_country)),
('Şırnak', (SELECT CountryId FROM inserted_country)),
('Bartın', (SELECT CountryId FROM inserted_country)),
('Ardahan', (SELECT CountryId FROM inserted_country)),
('Iğdır', (SELECT CountryId FROM inserted_country)),
('Yalova', (SELECT CountryId FROM inserted_country)),
('Karabük', (SELECT CountryId FROM inserted_country)),
('Kilis', (SELECT CountryId FROM inserted_country)),
('Osmaniye', (SELECT CountryId FROM inserted_country)),
('Düzce', (SELECT CountryId FROM inserted_country));

-- Germany
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Germany', 'DE', '^\+49[1-9][0-9]{1,14}$', '+49 (####) #######') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Berlin', (SELECT CountryId FROM inserted_country)),
('Hamburg', (SELECT CountryId FROM inserted_country)),
('Munich', (SELECT CountryId FROM inserted_country)),
('Cologne', (SELECT CountryId FROM inserted_country)),
('Frankfurt', (SELECT CountryId FROM inserted_country)),
('Stuttgart', (SELECT CountryId FROM inserted_country)),
('Düsseldorf', (SELECT CountryId FROM inserted_country)),
('Dortmund', (SELECT CountryId FROM inserted_country)),
('Essen', (SELECT CountryId FROM inserted_country)),
('Leipzig', (SELECT CountryId FROM inserted_country)),
('Bremen', (SELECT CountryId FROM inserted_country)),
('Dresden', (SELECT CountryId FROM inserted_country)),
('Hanover', (SELECT CountryId FROM inserted_country)),
('Nuremberg', (SELECT CountryId FROM inserted_country)),
('Duisburg', (SELECT CountryId FROM inserted_country)),
('Bochum', (SELECT CountryId FROM inserted_country)),
('Wuppertal', (SELECT CountryId FROM inserted_country)),
('Bielefeld', (SELECT CountryId FROM inserted_country)),
('Bonn', (SELECT CountryId FROM inserted_country)),
('Münster', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- France
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('France', 'FR', '^\+33[1-9][0-9]{8}$', '+33 (##) ########') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Paris', (SELECT CountryId FROM inserted_country)),
('Marseille', (SELECT CountryId FROM inserted_country)),
('Lyon', (SELECT CountryId FROM inserted_country)),
('Toulouse', (SELECT CountryId FROM inserted_country)),
('Nice', (SELECT CountryId FROM inserted_country)),
('Nantes', (SELECT CountryId FROM inserted_country)),
('Strasbourg', (SELECT CountryId FROM inserted_country)),
('Montpellier', (SELECT CountryId FROM inserted_country)),
('Bordeaux', (SELECT CountryId FROM inserted_country)),
('Lille', (SELECT CountryId FROM inserted_country)),
('Rennes', (SELECT CountryId FROM inserted_country)),
('Reims', (SELECT CountryId FROM inserted_country)),
('Le Havre', (SELECT CountryId FROM inserted_country)),
('Saint-Étienne', (SELECT CountryId FROM inserted_country)),
('Toulon', (SELECT CountryId FROM inserted_country)),
('Grenoble', (SELECT CountryId FROM inserted_country)),
('Dijon', (SELECT CountryId FROM inserted_country)),
('Angers', (SELECT CountryId FROM inserted_country)),
('Nîmes', (SELECT CountryId FROM inserted_country)),
('Villeurbanne', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Italy
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Italy', 'IT', '^\+39[0-9]{8,10}$', '+39 ### #######') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Rome', (SELECT CountryId FROM inserted_country)),
('Milan', (SELECT CountryId FROM inserted_country)),
('Naples', (SELECT CountryId FROM inserted_country)),
('Turin', (SELECT CountryId FROM inserted_country)),
('Palermo', (SELECT CountryId FROM inserted_country)),
('Genoa', (SELECT CountryId FROM inserted_country)),
('Bologna', (SELECT CountryId FROM inserted_country)),
('Florence', (SELECT CountryId FROM inserted_country)),
('Bari', (SELECT CountryId FROM inserted_country)),
('Catania', (SELECT CountryId FROM inserted_country)),
('Venice', (SELECT CountryId FROM inserted_country)),
('Verona', (SELECT CountryId FROM inserted_country)),
('Messina', (SELECT CountryId FROM inserted_country)),
('Padua', (SELECT CountryId FROM inserted_country)),
('Trieste', (SELECT CountryId FROM inserted_country)),
('Brescia', (SELECT CountryId FROM inserted_country)),
('Parma', (SELECT CountryId FROM inserted_country)),
('Taranto', (SELECT CountryId FROM inserted_country)),
('Modena', (SELECT CountryId FROM inserted_country)),
('Prato', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Spain
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Spain', 'ES', '^\+34[6-9][0-9]{8}$', '+34 ### ### ###') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Madrid', (SELECT CountryId FROM inserted_country)),
('Barcelona', (SELECT CountryId FROM inserted_country)),
('Valencia', (SELECT CountryId FROM inserted_country)),
('Seville', (SELECT CountryId FROM inserted_country)),
('Zaragoza', (SELECT CountryId FROM inserted_country)),
('Málaga', (SELECT CountryId FROM inserted_country)),
('Murcia', (SELECT CountryId FROM inserted_country)),
('Palma de Mallorca', (SELECT CountryId FROM inserted_country)),
('Las Palmas', (SELECT CountryId FROM inserted_country)),
('Bilbao', (SELECT CountryId FROM inserted_country)),
('Alicante', (SELECT CountryId FROM inserted_country)),
('Córdoba', (SELECT CountryId FROM inserted_country)),
('Valladolid', (SELECT CountryId FROM inserted_country)),
('Vigo', (SELECT CountryId FROM inserted_country)),
('Gijón', (SELECT CountryId FROM inserted_country)),
('A Coruña', (SELECT CountryId FROM inserted_country)),
('Vitoria-Gasteiz', (SELECT CountryId FROM inserted_country)),
('Granada', (SELECT CountryId FROM inserted_country)),
('Elche', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Poland
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Poland', 'PL', '^\+48[1-9][0-9]{8}$', '+48 ### ### ###') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Warsaw', (SELECT CountryId FROM inserted_country)),
('Kraków', (SELECT CountryId FROM inserted_country)),
('Łódź', (SELECT CountryId FROM inserted_country)),
('Wrocław', (SELECT CountryId FROM inserted_country)),
('Poznań', (SELECT CountryId FROM inserted_country)),
('Gdańsk', (SELECT CountryId FROM inserted_country)),
('Szczecin', (SELECT CountryId FROM inserted_country)),
('Bydgoszcz', (SELECT CountryId FROM inserted_country)),
('Lublin', (SELECT CountryId FROM inserted_country)),
('Katowice', (SELECT CountryId FROM inserted_country)),
('Białystok', (SELECT CountryId FROM inserted_country)),
('Gdynia', (SELECT CountryId FROM inserted_country)),
('Częstochowa', (SELECT CountryId FROM inserted_country)),
('Radom', (SELECT CountryId FROM inserted_country)),
('Sosnowiec', (SELECT CountryId FROM inserted_country)),
('Toruń', (SELECT CountryId FROM inserted_country)),
('Kielce', (SELECT CountryId FROM inserted_country)),
('Gliwice', (SELECT CountryId FROM inserted_country)),
('Zabrze', (SELECT CountryId FROM inserted_country)),
('Olsztyn', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Austria
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Austria', 'AT', '^\+43[1-9][0-9]{3,11}$', '+43 (###) ### ####') RETURNING Id AS CountryId
)
INSERT INTO Cities (Name, CountryId)
VALUES
('Vienna', (SELECT CountryId FROM inserted_country)),
('Graz', (SELECT CountryId FROM inserted_country)),
('Linz', (SELECT CountryId FROM inserted_country)),
('Salzburg', (SELECT CountryId FROM inserted_country)),
('Innsbruck', (SELECT CountryId FROM inserted_country)),
('Klagenfurt', (SELECT CountryId FROM inserted_country)),
('Villach', (SELECT CountryId FROM inserted_country)),
('Wels', (SELECT CountryId FROM inserted_country)),
('Sankt Pölten', (SELECT CountryId FROM inserted_country)),
('Dornbirn', (SELECT CountryId FROM inserted_country)),
('Wiener Neustadt', (SELECT CountryId FROM inserted_country)),
('Steyr', (SELECT CountryId FROM inserted_country)),
('Feldkirch', (SELECT CountryId FROM inserted_country)),
('Bregenz', (SELECT CountryId FROM inserted_country)),
('Leonding', (SELECT CountryId FROM inserted_country)),
('Klosterneuburg', (SELECT CountryId FROM inserted_country)),
('Baden', (SELECT CountryId FROM inserted_country)),
('Wolfsberg', (SELECT CountryId FROM inserted_country)),
('Leoben', (SELECT CountryId FROM inserted_country)),
('Krems an der Donau', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Belgium
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Belgium', 'BE', '^\+32[1-9][0-9]{7,8}$', '+32 ### ## ## ##') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Brussels', (SELECT CountryId FROM inserted_country)),
('Antwerp', (SELECT CountryId FROM inserted_country)),
('Ghent', (SELECT CountryId FROM inserted_country)),
('Charleroi', (SELECT CountryId FROM inserted_country)),
('Liège', (SELECT CountryId FROM inserted_country)),
('Anderlecht', (SELECT CountryId FROM inserted_country)),
('Bruges', (SELECT CountryId FROM inserted_country)),
('Namur', (SELECT CountryId FROM inserted_country)),
('Leuven', (SELECT CountryId FROM inserted_country)),
('Mons', (SELECT CountryId FROM inserted_country)),
('Aalst', (SELECT CountryId FROM inserted_country)),
('Mechelen', (SELECT CountryId FROM inserted_country)),
('La Louvière', (SELECT CountryId FROM inserted_country)),
('Kortrijk', (SELECT CountryId FROM inserted_country)),
('Hasselt', (SELECT CountryId FROM inserted_country)),
('Ostend', (SELECT CountryId FROM inserted_country)),
('Sint-Niklaas', (SELECT CountryId FROM inserted_country)),
('Tournai', (SELECT CountryId FROM inserted_country)),
('Genk', (SELECT CountryId FROM inserted_country)),
('Seraing', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Bulgaria
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Bulgaria', 'BG','^\+359[1-9][0-9]{8}$', '+359 (###) ### ###') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Sofia', (SELECT CountryId FROM inserted_country)),
('Plovdiv', (SELECT CountryId FROM inserted_country)),
('Varna', (SELECT CountryId FROM inserted_country)),
('Burgas', (SELECT CountryId FROM inserted_country)),
('Ruse', (SELECT CountryId FROM inserted_country)),
('Stara Zagora', (SELECT CountryId FROM inserted_country)),
('Pleven', (SELECT CountryId FROM inserted_country)),
('Sliven', (SELECT CountryId FROM inserted_country)),
('Dobrich', (SELECT CountryId FROM inserted_country)),
('Shumen', (SELECT CountryId FROM inserted_country)),
('Pernik', (SELECT CountryId FROM inserted_country)),
('Haskovo', (SELECT CountryId FROM inserted_country)),
('Yambol', (SELECT CountryId FROM inserted_country)),
('Blagoevgrad', (SELECT CountryId FROM inserted_country)),
('Veliko Tarnovo', (SELECT CountryId FROM inserted_country)),
('Gabrovo', (SELECT CountryId FROM inserted_country)),
('Vidin', (SELECT CountryId FROM inserted_country)),
('Asenovgrad', (SELECT CountryId FROM inserted_country)),
('Kazanlak', (SELECT CountryId FROM inserted_country)),
('Kyustendil', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Croatia
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Croatia', 'HR', '^\+385[1-9][0-9]{6,9}$', '+385 (##) ### ####') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Zagreb', (SELECT CountryId FROM inserted_country)),
('Split', (SELECT CountryId FROM inserted_country)),
('Rijeka', (SELECT CountryId FROM inserted_country)),
('Osijek', (SELECT CountryId FROM inserted_country)),
('Zadar', (SELECT CountryId FROM inserted_country)),
('Velika Gorica', (SELECT CountryId FROM inserted_country)),
('Pula', (SELECT CountryId FROM inserted_country)),
('Slavonski Brod', (SELECT CountryId FROM inserted_country)),
('Karlovac', (SELECT CountryId FROM inserted_country)),
('Varaždin', (SELECT CountryId FROM inserted_country)),
('Šibenik', (SELECT CountryId FROM inserted_country)),
('Dubrovnik', (SELECT CountryId FROM inserted_country)),
('Bjelovar', (SELECT CountryId FROM inserted_country)),
('Kaštela', (SELECT CountryId FROM inserted_country)),
('Samobor', (SELECT CountryId FROM inserted_country)),
('Vinkovci', (SELECT CountryId FROM inserted_country)),
('Koprivnica', (SELECT CountryId FROM inserted_country)),
('Čakovec', (SELECT CountryId FROM inserted_country)),
('Sisak', (SELECT CountryId FROM inserted_country)),
('Đakovo', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Czech Republic
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Czech Republic', 'CZ', '^\+420[0-9]{9}$', '+420 ### ### ###') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Prague', (SELECT CountryId FROM inserted_country)),
('Brno', (SELECT CountryId FROM inserted_country)),
('Ostrava', (SELECT CountryId FROM inserted_country)),
('Plzeň', (SELECT CountryId FROM inserted_country)),
('Liberec', (SELECT CountryId FROM inserted_country)),
('Olomouc', (SELECT CountryId FROM inserted_country)),
('Ústí nad Labem', (SELECT CountryId FROM inserted_country)),
('Hradec Králové', (SELECT CountryId FROM inserted_country)),
('Pardubice', (SELECT CountryId FROM inserted_country)),
('Zlín', (SELECT CountryId FROM inserted_country)),
('České Budějovice', (SELECT CountryId FROM inserted_country)),
('Příbram', (SELECT CountryId FROM inserted_country)),
('Třebíč', (SELECT CountryId FROM inserted_country)),
('Karlovy Vary', (SELECT CountryId FROM inserted_country)),
('Jihlava', (SELECT CountryId FROM inserted_country)),
('Teplice', (SELECT CountryId FROM inserted_country)),
('Děčín', (SELECT CountryId FROM inserted_country)),
('Chomutov', (SELECT CountryId FROM inserted_country)),
('Jablonec nad Nisou', (SELECT CountryId FROM inserted_country)),
('Prostějov', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Denmark
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Denmark', 'DK', '^\+45[0-9]{6,8}$', '+45 ## ## ## ##') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Copenhagen', (SELECT CountryId FROM inserted_country)),
('Aarhus', (SELECT CountryId FROM inserted_country)),
('Odense', (SELECT CountryId FROM inserted_country)),
('Aalborg', (SELECT CountryId FROM inserted_country)),
('Esbjerg', (SELECT CountryId FROM inserted_country)),
('Randers', (SELECT CountryId FROM inserted_country)),
('Kolding', (SELECT CountryId FROM inserted_country)),
('Horsens', (SELECT CountryId FROM inserted_country)),
('Vejle', (SELECT CountryId FROM inserted_country)),
('Roskilde', (SELECT CountryId FROM inserted_country)),
('Herning', (SELECT CountryId FROM inserted_country)),
('Silkeborg', (SELECT CountryId FROM inserted_country)),
('Næstved', (SELECT CountryId FROM inserted_country)),
('Fredericia', (SELECT CountryId FROM inserted_country)),
('Ballerup', (SELECT CountryId FROM inserted_country)),
('Viborg', (SELECT CountryId FROM inserted_country)),
('Køge', (SELECT CountryId FROM inserted_country)),
('Holstebro', (SELECT CountryId FROM inserted_country)),
('Taastrup', (SELECT CountryId FROM inserted_country)),
('Hørsholm', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Estonia
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Estonia', 'EE', '^\+372[0-9]{6,8}$', '+372 #### ###') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Tallinn', (SELECT CountryId FROM inserted_country)),
('Tartu', (SELECT CountryId FROM inserted_country)),
('Narva', (SELECT CountryId FROM inserted_country)),
('Pärnu', (SELECT CountryId FROM inserted_country)),
('Kohtla-Järve', (SELECT CountryId FROM inserted_country)),
('Viljandi', (SELECT CountryId FROM inserted_country)),
('Rakvere', (SELECT CountryId FROM inserted_country)),
('Maardu', (SELECT CountryId FROM inserted_country)),
('Sillamäe', (SELECT CountryId FROM inserted_country)),
('Kuressaare', (SELECT CountryId FROM inserted_country)),
('Valga', (SELECT CountryId FROM inserted_country)),
('Võru', (SELECT CountryId FROM inserted_country)),
('Haapsalu', (SELECT CountryId FROM inserted_country)),
('Jõhvi', (SELECT CountryId FROM inserted_country)),
('Paide', (SELECT CountryId FROM inserted_country)),
('Keila', (SELECT CountryId FROM inserted_country)),
('Elva', (SELECT CountryId FROM inserted_country)),
('Türi', (SELECT CountryId FROM inserted_country)),
('Rapla', (SELECT CountryId FROM inserted_country)),
('Kiviõli', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Finland
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Finland', 'FI', '^\+358[1-9][0-9]{7,9}$', '+358 ## ### ####') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Helsinki', (SELECT CountryId FROM inserted_country)),
('Espoo', (SELECT CountryId FROM inserted_country)),
('Tampere', (SELECT CountryId FROM inserted_country)),
('Vantaa', (SELECT CountryId FROM inserted_country)),
('Oulu', (SELECT CountryId FROM inserted_country)),
('Turku', (SELECT CountryId FROM inserted_country)),
('Jyväskylä', (SELECT CountryId FROM inserted_country)),
('Lahti', (SELECT CountryId FROM inserted_country)),
('Kuopio', (SELECT CountryId FROM inserted_country)),
('Pori', (SELECT CountryId FROM inserted_country)),
('Lappeenranta', (SELECT CountryId FROM inserted_country)),
('Kotka', (SELECT CountryId FROM inserted_country)),
('Joensuu', (SELECT CountryId FROM inserted_country)),
('Hämeenlinna', (SELECT CountryId FROM inserted_country)),
('Vaasa', (SELECT CountryId FROM inserted_country)),
('Rovaniemi', (SELECT CountryId FROM inserted_country)),
('Seinäjoki', (SELECT CountryId FROM inserted_country)),
('Mikkeli', (SELECT CountryId FROM inserted_country)),
('Kokkola', (SELECT CountryId FROM inserted_country)),
('Hyvinkää', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Greece
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Greece', 'GR', '^\+30[1-9][0-9]{9}$', '+30 ### ### ####') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Athens', (SELECT CountryId FROM inserted_country)),
('Thessaloniki', (SELECT CountryId FROM inserted_country)),
('Patras', (SELECT CountryId FROM inserted_country)),
('Heraklion', (SELECT CountryId FROM inserted_country)),
('Larissa', (SELECT CountryId FROM inserted_country)),
('Volos', (SELECT CountryId FROM inserted_country)),
('Rhodes', (SELECT CountryId FROM inserted_country)),
('Ioannina', (SELECT CountryId FROM inserted_country)),
('Chania', (SELECT CountryId FROM inserted_country)),
('Chalcis', (SELECT CountryId FROM inserted_country)),
('Serres', (SELECT CountryId FROM inserted_country)),
('Alexandroupoli', (SELECT CountryId FROM inserted_country)),
('Kavala', (SELECT CountryId FROM inserted_country)),
('Kalamata', (SELECT CountryId FROM inserted_country)),
('Trikala', (SELECT CountryId FROM inserted_country)),
('Veria', (SELECT CountryId FROM inserted_country)),
('Agrinio', (SELECT CountryId FROM inserted_country)),
('Drama', (SELECT CountryId FROM inserted_country)),
('Xanthi', (SELECT CountryId FROM inserted_country)),
('Komotini', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Hungary
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Hungary', 'HU', '^\+36[1-9][0-9]{7,8}$', '+36 (##) ### ###') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Budapest', (SELECT CountryId FROM inserted_country)),
('Debrecen', (SELECT CountryId FROM inserted_country)),
('Szeged', (SELECT CountryId FROM inserted_country)),
('Miskolc', (SELECT CountryId FROM inserted_country)),
('Pécs', (SELECT CountryId FROM inserted_country)),
('Győr', (SELECT CountryId FROM inserted_country)),
('Nyíregyháza', (SELECT CountryId FROM inserted_country)),
('Kecskemét', (SELECT CountryId FROM inserted_country)),
('Székesfehérvár', (SELECT CountryId FROM inserted_country)),
('Szombathely', (SELECT CountryId FROM inserted_country)),
('Szolnok', (SELECT CountryId FROM inserted_country)),
('Tatabánya', (SELECT CountryId FROM inserted_country)),
('Érd', (SELECT CountryId FROM inserted_country)),
('Kaposvár', (SELECT CountryId FROM inserted_country)),
('Békéscsaba', (SELECT CountryId FROM inserted_country)),
('Zalaegerszeg', (SELECT CountryId FROM inserted_country)),
('Eger', (SELECT CountryId FROM inserted_country)),
('Nagykanizsa', (SELECT CountryId FROM inserted_country)),
('Dunaújváros', (SELECT CountryId FROM inserted_country)),
('Hódmezővásárhely', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Ireland
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Ireland', 'IE', '^\+353[1-9][0-9]{8}$', '+353 ## ### ####') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Dublin', (SELECT CountryId FROM inserted_country)),
('Cork', (SELECT CountryId FROM inserted_country)),
('Limerick', (SELECT CountryId FROM inserted_country)),
('Galway', (SELECT CountryId FROM inserted_country)),
('Waterford', (SELECT CountryId FROM inserted_country)),
('Drogheda', (SELECT CountryId FROM inserted_country)),
('Dundalk', (SELECT CountryId FROM inserted_country)),
('Swords', (SELECT CountryId FROM inserted_country)),
('Bray', (SELECT CountryId FROM inserted_country)),
('Navan', (SELECT CountryId FROM inserted_country)),
('Ennis', (SELECT CountryId FROM inserted_country)),
('Tralee', (SELECT CountryId FROM inserted_country)),
('Carlow', (SELECT CountryId FROM inserted_country)),
('Newbridge', (SELECT CountryId FROM inserted_country)),
('Portlaoise', (SELECT CountryId FROM inserted_country)),
('Balbriggan', (SELECT CountryId FROM inserted_country)),
('Letterkenny', (SELECT CountryId FROM inserted_country)),
('Kilkenny', (SELECT CountryId FROM inserted_country)),
('Clonmel', (SELECT CountryId FROM inserted_country)),
('Sligo', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Latvia
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Latvia', 'LV', '^\+371[0-9]{8}$', '+371 #### ####') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Riga', (SELECT CountryId FROM inserted_country)),
('Daugavpils', (SELECT CountryId FROM inserted_country)),
('Liepāja', (SELECT CountryId FROM inserted_country)),
('Jelgava', (SELECT CountryId FROM inserted_country)),
('Jūrmala', (SELECT CountryId FROM inserted_country)),
('Ventspils', (SELECT CountryId FROM inserted_country)),
('Rēzekne', (SELECT CountryId FROM inserted_country)),
('Valmiera', (SELECT CountryId FROM inserted_country)),
('Ogre', (SELECT CountryId FROM inserted_country)),
('Jēkabpils', (SELECT CountryId FROM inserted_country)),
('Salaspils', (SELECT CountryId FROM inserted_country)),
('Tukums', (SELECT CountryId FROM inserted_country)),
('Cēsis', (SELECT CountryId FROM inserted_country)),
('Sigulda', (SELECT CountryId FROM inserted_country)),
('Bauska', (SELECT CountryId FROM inserted_country)),
('Saldus', (SELECT CountryId FROM inserted_country)),
('Kuldīga', (SELECT CountryId FROM inserted_country)),
('Talsi', (SELECT CountryId FROM inserted_country)),
('Madona', (SELECT CountryId FROM inserted_country)),
('Līvāni', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Lithuania
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Lithuania', 'LT', '^\+370[0-9]{8}$', '+370 (###) #####') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Vilnius', (SELECT CountryId FROM inserted_country)),
('Kaunas', (SELECT CountryId FROM inserted_country)),
('Klaipėda', (SELECT CountryId FROM inserted_country)),
('Šiauliai', (SELECT CountryId FROM inserted_country)),
('Panevėžys', (SELECT CountryId FROM inserted_country)),
('Alytus', (SELECT CountryId FROM inserted_country)),
('Marijampolė', (SELECT CountryId FROM inserted_country)),
('Mažeikiai', (SELECT CountryId FROM inserted_country)),
('Jonava', (SELECT CountryId FROM inserted_country)),
('Utena', (SELECT CountryId FROM inserted_country)),
('Kėdainiai', (SELECT CountryId FROM inserted_country)),
('Telšiai', (SELECT CountryId FROM inserted_country)),
('Visaginas', (SELECT CountryId FROM inserted_country)),
('Tauragė', (SELECT CountryId FROM inserted_country)),
('Ukmergė', (SELECT CountryId FROM inserted_country)),
('Plungė', (SELECT CountryId FROM inserted_country)),
('Šilutė', (SELECT CountryId FROM inserted_country)),
('Radviliškis', (SELECT CountryId FROM inserted_country)),
('Palanga', (SELECT CountryId FROM inserted_country)),
('Druskininkai', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Luxembourg
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Luxembourg', 'LU', '^\+50[0-9]{6,10}$', '+50 ### ### ###') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Luxembourg City', (SELECT CountryId FROM inserted_country)),
('Esch-sur-Alzette', (SELECT CountryId FROM inserted_country)),
('Differdange', (SELECT CountryId FROM inserted_country)),
('Dudelange', (SELECT CountryId FROM inserted_country)),
('Ettelbruck', (SELECT CountryId FROM inserted_country)),
('Diekirch', (SELECT CountryId FROM inserted_country)),
('Strassen', (SELECT CountryId FROM inserted_country)),
('Bertrange', (SELECT CountryId FROM inserted_country)),
('Belvaux', (SELECT CountryId FROM inserted_country)),
('Mamer', (SELECT CountryId FROM inserted_country)),
('Soleuvre', (SELECT CountryId FROM inserted_country)),
('Wiltz', (SELECT CountryId FROM inserted_country)),
('Grevenmacher', (SELECT CountryId FROM inserted_country)),
('Remich', (SELECT CountryId FROM inserted_country)),
('Mondorf-les-Bains', (SELECT CountryId FROM inserted_country)),
('Rodange', (SELECT CountryId FROM inserted_country)),
('Bettembourg', (SELECT CountryId FROM inserted_country)),
('Schifflange', (SELECT CountryId FROM inserted_country)),
('Pétange', (SELECT CountryId FROM inserted_country)),
('Hesperange', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Malta
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Malta', 'MA', '^\+55[0-9]{6,10}$', '+55 ### ### ###') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Valletta', (SELECT CountryId FROM inserted_country)),
('Birkirkara', (SELECT CountryId FROM inserted_country)),
('Mosta', (SELECT CountryId FROM inserted_country)),
('Qormi', (SELECT CountryId FROM inserted_country)),
('Sliema', (SELECT CountryId FROM inserted_country)),
('San Ġwann', (SELECT CountryId FROM inserted_country)),
('Żabbar', (SELECT CountryId FROM inserted_country)),
('Naxxar', (SELECT CountryId FROM inserted_country)),
('St. Pauls Bay', (SELECT CountryId FROM inserted_country)),
('Fgura', (SELECT CountryId FROM inserted_country)),
('Żejtun', (SELECT CountryId FROM inserted_country)),
('Marsaskala', (SELECT CountryId FROM inserted_country)),
('Paola', (SELECT CountryId FROM inserted_country)),
('Attard', (SELECT CountryId FROM inserted_country)),
('Gżira', (SELECT CountryId FROM inserted_country)),
('Birżebbuġa', (SELECT CountryId FROM inserted_country)),
('Tarxien', (SELECT CountryId FROM inserted_country)),
('Żebbuġ', (SELECT CountryId FROM inserted_country)),
('Rabat', (SELECT CountryId FROM inserted_country)),
('Floriana', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Netherlands
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Netherlands', 'NL', '^\+31[1-9][0-9]{8}$', '+31 ## ### ####') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Amsterdam', (SELECT CountryId FROM inserted_country)),
('Rotterdam', (SELECT CountryId FROM inserted_country)),
('The Hague', (SELECT CountryId FROM inserted_country)),
('Utrecht', (SELECT CountryId FROM inserted_country)),
('Eindhoven', (SELECT CountryId FROM inserted_country)),
('Tilburg', (SELECT CountryId FROM inserted_country)),
('Groningen', (SELECT CountryId FROM inserted_country)),
('Almere', (SELECT CountryId FROM inserted_country)),
('Breda', (SELECT CountryId FROM inserted_country)),
('Nijmegen', (SELECT CountryId FROM inserted_country)),
('Enschede', (SELECT CountryId FROM inserted_country)),
('Apeldoorn', (SELECT CountryId FROM inserted_country)),
('Haarlem', (SELECT CountryId FROM inserted_country)),
('Arnhem', (SELECT CountryId FROM inserted_country)),
('Zaanstad', (SELECT CountryId FROM inserted_country)),
('Amersfoort', (SELECT CountryId FROM inserted_country)),
('Haarlemmermeer', (SELECT CountryId FROM inserted_country)),
('Zwolle', (SELECT CountryId FROM inserted_country)),
('s-Hertogenbosch', (SELECT CountryId FROM inserted_country)),
('Leiden', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Portugal
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Portugal', 'PT', '^\+351[1-9][0-9]{8}$', '+351 ### ### ###') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Lisbon', (SELECT CountryId FROM inserted_country)),
('Porto', (SELECT CountryId FROM inserted_country)),
('Vila Nova de Gaia', (SELECT CountryId FROM inserted_country)),
('Amadora', (SELECT CountryId FROM inserted_country)),
('Braga', (SELECT CountryId FROM inserted_country)),
('Agualva-Cacém', (SELECT CountryId FROM inserted_country)),
('Coimbra', (SELECT CountryId FROM inserted_country)),
('Queluz', (SELECT CountryId FROM inserted_country)),
('Funchal', (SELECT CountryId FROM inserted_country)),
('Setúbal', (SELECT CountryId FROM inserted_country)),
('Almada', (SELECT CountryId FROM inserted_country)),
('Rio Tinto', (SELECT CountryId FROM inserted_country)),
('Aveiro', (SELECT CountryId FROM inserted_country)),
('Barreiro', (SELECT CountryId FROM inserted_country)),
('Monsanto', (SELECT CountryId FROM inserted_country)),
('Évora', (SELECT CountryId FROM inserted_country)),
('Leiria', (SELECT CountryId FROM inserted_country)),
('Guimarães', (SELECT CountryId FROM inserted_country)),
('Cascais', (SELECT CountryId FROM inserted_country)),
('Oeiras', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Romania
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Romania', 'RO', '^\+40[1-9][0-9]{8}$', '+40 ## ### ####') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Bucharest', (SELECT CountryId FROM inserted_country)),
('Cluj-Napoca', (SELECT CountryId FROM inserted_country)),
('Timișoara', (SELECT CountryId FROM inserted_country)),
('Iași', (SELECT CountryId FROM inserted_country)),
('Constanța', (SELECT CountryId FROM inserted_country)),
('Craiova', (SELECT CountryId FROM inserted_country)),
('Brașov', (SELECT CountryId FROM inserted_country)),
('Galați', (SELECT CountryId FROM inserted_country)),
('Ploiești', (SELECT CountryId FROM inserted_country)),
('Oradea', (SELECT CountryId FROM inserted_country)),
('Brăila', (SELECT CountryId FROM inserted_country)),
('Arad', (SELECT CountryId FROM inserted_country)),
('Pitești', (SELECT CountryId FROM inserted_country)),
('Sibiu', (SELECT CountryId FROM inserted_country)),
('Bacău', (SELECT CountryId FROM inserted_country)),
('Târgu Mureș', (SELECT CountryId FROM inserted_country)),
('Baia Mare', (SELECT CountryId FROM inserted_country)),
('Buzău', (SELECT CountryId FROM inserted_country)),
('Botoșani', (SELECT CountryId FROM inserted_country)),
('Satu Mare', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Slovakia
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Slovakia', 'SK', '^\+421[0-9]{6,10}$', '+421 ### ### ###') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Bratislava', (SELECT CountryId FROM inserted_country)),
('Košice', (SELECT CountryId FROM inserted_country)),
('Prešov', (SELECT CountryId FROM inserted_country)),
('Žilina', (SELECT CountryId FROM inserted_country)),
('Nitra', (SELECT CountryId FROM inserted_country)),
('Banská Bystrica', (SELECT CountryId FROM inserted_country)),
('Trnava', (SELECT CountryId FROM inserted_country)),
('Martin', (SELECT CountryId FROM inserted_country)),
('Trenčín', (SELECT CountryId FROM inserted_country)),
('Poprad', (SELECT CountryId FROM inserted_country)),
('Prievidza', (SELECT CountryId FROM inserted_country)),
('Zvolen', (SELECT CountryId FROM inserted_country)),
('Považská Bystrica', (SELECT CountryId FROM inserted_country)),
('Nové Zámky', (SELECT CountryId FROM inserted_country)),
('Michalovce', (SELECT CountryId FROM inserted_country)),
('Spišská Nová Ves', (SELECT CountryId FROM inserted_country)),
('Komárno', (SELECT CountryId FROM inserted_country)),
('Levice', (SELECT CountryId FROM inserted_country)),
('Humenné', (SELECT CountryId FROM inserted_country)),
('Liptovský Mikuláš', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));
-- Slovenia
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Slovenia', 'SL', '^\+386[0-9]{6,10}$', '+386 ### ### ###') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Ljubljana', (SELECT CountryId FROM inserted_country)),
('Maribor', (SELECT CountryId FROM inserted_country)),
('Celje', (SELECT CountryId FROM inserted_country)),
('Kranj', (SELECT CountryId FROM inserted_country)),
('Velenje', (SELECT CountryId FROM inserted_country)),
('Koper', (SELECT CountryId FROM inserted_country)),
('Novo Mesto', (SELECT CountryId FROM inserted_country)),
('Ptuj', (SELECT CountryId FROM inserted_country)),
('Trbovlje', (SELECT CountryId FROM inserted_country)),
('Kamnik', (SELECT CountryId FROM inserted_country)),
('Nova Gorica', (SELECT CountryId FROM inserted_country)),
('Murska Sobota', (SELECT CountryId FROM inserted_country)),
('Jesenice', (SELECT CountryId FROM inserted_country)),
('Domžale', (SELECT CountryId FROM inserted_country)),
('Izola', (SELECT CountryId FROM inserted_country)),
('Kočevje', (SELECT CountryId FROM inserted_country)),
('Postojna', (SELECT CountryId FROM inserted_country)),
('Ravne na Koroškem', (SELECT CountryId FROM inserted_country)),
('Sežana', (SELECT CountryId FROM inserted_country)),
('Brežice', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Sweden
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Sweden', 'SE', '^\+46[1-9][0-9]{6,9}$', '+46 (##) ### ## ##') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Stockholm', (SELECT CountryId FROM inserted_country)),
('Gothenburg', (SELECT CountryId FROM inserted_country)),
('Malmö', (SELECT CountryId FROM inserted_country)),
('Uppsala', (SELECT CountryId FROM inserted_country)),
('Västerås', (SELECT CountryId FROM inserted_country)),
('Örebro', (SELECT CountryId FROM inserted_country)),
('Linköping', (SELECT CountryId FROM inserted_country)),
('Helsingborg', (SELECT CountryId FROM inserted_country)),
('Jönköping', (SELECT CountryId FROM inserted_country)),
('Norrköping', (SELECT CountryId FROM inserted_country)),
('Lund', (SELECT CountryId FROM inserted_country)),
('Umeå', (SELECT CountryId FROM inserted_country)),
('Gävle', (SELECT CountryId FROM inserted_country)),
('Borås', (SELECT CountryId FROM inserted_country)),
('Eskilstuna', (SELECT CountryId FROM inserted_country)),
('Södertälje', (SELECT CountryId FROM inserted_country)),
('Karlstad', (SELECT CountryId FROM inserted_country)),
('Täby', (SELECT CountryId FROM inserted_country)),
('Växjö', (SELECT CountryId FROM inserted_country)),
('Halmstad', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Norway
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Norway', 'NO', '^\+66[0-9]{6,10}$', '+66 ### ### ###') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Oslo', (SELECT CountryId FROM inserted_country)),
('Bergen', (SELECT CountryId FROM inserted_country)),
('Trondheim', (SELECT CountryId FROM inserted_country)),
('Stavanger', (SELECT CountryId FROM inserted_country)),
('Drammen', (SELECT CountryId FROM inserted_country)),
('Fredrikstad', (SELECT CountryId FROM inserted_country)),
('Kristiansand', (SELECT CountryId FROM inserted_country)),
('Sandnes', (SELECT CountryId FROM inserted_country)),
('Tromsø', (SELECT CountryId FROM inserted_country)),
('Sarpsborg', (SELECT CountryId FROM inserted_country)),
('Skien', (SELECT CountryId FROM inserted_country)),
('Ålesund', (SELECT CountryId FROM inserted_country)),
('Haugesund', (SELECT CountryId FROM inserted_country)),
('Tønsberg', (SELECT CountryId FROM inserted_country)),
('Moss', (SELECT CountryId FROM inserted_country)),
('Bodø', (SELECT CountryId FROM inserted_country)),
('Arendal', (SELECT CountryId FROM inserted_country)),
('Hamar', (SELECT CountryId FROM inserted_country)),
('Lillehammer', (SELECT CountryId FROM inserted_country)),
('Molde', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Switzerland
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Switzerland', 'CH', '^\+41[1-9][0-9]{8}$', '+41 ## ### ####') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Zurich', (SELECT CountryId FROM inserted_country)),
('Geneva', (SELECT CountryId FROM inserted_country)),
('Basel', (SELECT CountryId FROM inserted_country)),
('Bern', (SELECT CountryId FROM inserted_country)),
('Lausanne', (SELECT CountryId FROM inserted_country)),
('Lucerne', (SELECT CountryId FROM inserted_country)),
('St. Gallen', (SELECT CountryId FROM inserted_country)),
('Lugano', (SELECT CountryId FROM inserted_country)),
('Biel/Bienne', (SELECT CountryId FROM inserted_country)),
('Thun', (SELECT CountryId FROM inserted_country)),
('Köniz', (SELECT CountryId FROM inserted_country)),
('La Chaux-de-Fonds', (SELECT CountryId FROM inserted_country)),
('Fribourg', (SELECT CountryId FROM inserted_country)),
('Schaffhausen', (SELECT CountryId FROM inserted_country)),
('Chur', (SELECT CountryId FROM inserted_country)),
('Neuchâtel', (SELECT CountryId FROM inserted_country)),
('Vernier', (SELECT CountryId FROM inserted_country)),
('Uster', (SELECT CountryId FROM inserted_country)),
('Sion', (SELECT CountryId FROM inserted_country)),
('Yverdon-les-Bains', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Cyprus
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Cyprus', 'CY', '^\+66[0-9]{6,10}$', '+66 ### ### ###') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Nicosia', (SELECT CountryId FROM inserted_country)),
('Limassol', (SELECT CountryId FROM inserted_country)),
('Larnaca', (SELECT CountryId FROM inserted_country)),
('Famagusta', (SELECT CountryId FROM inserted_country)),
('Paphos', (SELECT CountryId FROM inserted_country)),
('Kyrenia', (SELECT CountryId FROM inserted_country)),
('Aglandjia', (SELECT CountryId FROM inserted_country)),
('Strovolos', (SELECT CountryId FROM inserted_country)),
('Lakatamia', (SELECT CountryId FROM inserted_country)),
('Dhali', (SELECT CountryId FROM inserted_country)),
('Paralimni', (SELECT CountryId FROM inserted_country)),
('Ypsonas', (SELECT CountryId FROM inserted_country)),
('Geroskipou', (SELECT CountryId FROM inserted_country)),
('Aradippou', (SELECT CountryId FROM inserted_country)),
('Dali', (SELECT CountryId FROM inserted_country)),
('Kato Polemidia', (SELECT CountryId FROM inserted_country)),
('Latsia', (SELECT CountryId FROM inserted_country)),
('Livadia', (SELECT CountryId FROM inserted_country)),
('Ayia Napa', (SELECT CountryId FROM inserted_country)),
('Polis', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Iceland
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Iceland', 'IC', '^\+77[0-9]{6,10}$', '+77 ### ### ###') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Reykjavík', (SELECT CountryId FROM inserted_country)),
('Kópavogur', (SELECT CountryId FROM inserted_country)),
('Hafnarfjörður', (SELECT CountryId FROM inserted_country)),
('Akureyri', (SELECT CountryId FROM inserted_country)),
('Reykjanesbær', (SELECT CountryId FROM inserted_country)),
('Garðabær', (SELECT CountryId FROM inserted_country)),
('Mosfellsbær', (SELECT CountryId FROM inserted_country)),
('Árborg', (SELECT CountryId FROM inserted_country)),
('Akranes', (SELECT CountryId FROM inserted_country)),
('Fjarðabyggð', (SELECT CountryId FROM inserted_country)),
('Selfoss', (SELECT CountryId FROM inserted_country)),
('Hveragerði', (SELECT CountryId FROM inserted_country)),
('Borgarnes', (SELECT CountryId FROM inserted_country)),
('Egilsstaðir', (SELECT CountryId FROM inserted_country)),
('Ísafjörður', (SELECT CountryId FROM inserted_country)),
('Vestmannaeyjar', (SELECT CountryId FROM inserted_country)),
('Sauðárkrókur', (SELECT CountryId FROM inserted_country)),
('Húsavík', (SELECT CountryId FROM inserted_country)),
('Dalvík', (SELECT CountryId FROM inserted_country)),
('Blönduós', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Liechtenstein
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Liechtenstein', 'LI', '^\+83[0-9]{6,10}$', '+83 ### ### ###') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Vaduz', (SELECT CountryId FROM inserted_country)),
('Schaan', (SELECT CountryId FROM inserted_country)),
('Balzers', (SELECT CountryId FROM inserted_country)),
('Triesen', (SELECT CountryId FROM inserted_country)),
('Eschen', (SELECT CountryId FROM inserted_country)),
('Mauren', (SELECT CountryId FROM inserted_country)),
('Triesenberg', (SELECT CountryId FROM inserted_country)),
('Ruggell', (SELECT CountryId FROM inserted_country)),
('Gamprin', (SELECT CountryId FROM inserted_country)),
('Schellenberg', (SELECT CountryId FROM inserted_country)),
('Planken', (SELECT CountryId FROM inserted_country)),
('Masescha', (SELECT CountryId FROM inserted_country)),
('Malbun', (SELECT CountryId FROM inserted_country)),
('Steg', (SELECT CountryId FROM inserted_country)),
('Nendeln', (SELECT CountryId FROM inserted_country)),
('Bendern', (SELECT CountryId FROM inserted_country)),
('Gaflei', (SELECT CountryId FROM inserted_country)),
('Hinterschellenberg', (SELECT CountryId FROM inserted_country)),
('Vorderprufatscheng', (SELECT CountryId FROM inserted_country)),
('Rotenboden', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Andorra
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Andorra', 'AN', '^\+77[0-9]{6,10}$', '+77 ### ### ###') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Andorra la Vella', (SELECT CountryId FROM inserted_country)),
('Escaldes-Engordany', (SELECT CountryId FROM inserted_country)),
('Encamp', (SELECT CountryId FROM inserted_country)),
('Sant Julià de Lòria', (SELECT CountryId FROM inserted_country)),
('La Massana', (SELECT CountryId FROM inserted_country)),
('Canillo', (SELECT CountryId FROM inserted_country)),
('Ordino', (SELECT CountryId FROM inserted_country)),
('Arinsal', (SELECT CountryId FROM inserted_country)),
('Pas de la Casa', (SELECT CountryId FROM inserted_country)),
('El Tarter', (SELECT CountryId FROM inserted_country)),
('Soldeu', (SELECT CountryId FROM inserted_country)),
('Les Escaldes', (SELECT CountryId FROM inserted_country)),
('Llorts', (SELECT CountryId FROM inserted_country)),
('Anyós', (SELECT CountryId FROM inserted_country)),
('Sispony', (SELECT CountryId FROM inserted_country)),
('Pal', (SELECT CountryId FROM inserted_country)),
('Aixovall', (SELECT CountryId FROM inserted_country)),
('Erts', (SELECT CountryId FROM inserted_country)),
('L Aldosa', (SELECT CountryId FROM inserted_country)),
('Ransol', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- San Marino
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('San Marino', 'SA', '^\+80[0-9]{6,10}$', '+80 ### ### ###') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('San Marino', (SELECT CountryId FROM inserted_country)),
('Serravalle', (SELECT CountryId FROM inserted_country)),
('Borgo Maggiore', (SELECT CountryId FROM inserted_country)),
('Domagnano', (SELECT CountryId FROM inserted_country)),
('Fiorentino', (SELECT CountryId FROM inserted_country)),
('Acquaviva', (SELECT CountryId FROM inserted_country)),
('Chiesanuova', (SELECT CountryId FROM inserted_country)),
('Faetano', (SELECT CountryId FROM inserted_country)),
('Montegiardino', (SELECT CountryId FROM inserted_country)),
('Cailungo', (SELECT CountryId FROM inserted_country)),
('Murata', (SELECT CountryId FROM inserted_country)),
('Falciano', (SELECT CountryId FROM inserted_country)),
('Fiorina', (SELECT CountryId FROM inserted_country)),
('Valdragone', (SELECT CountryId FROM inserted_country)),
('Galazzano', (SELECT CountryId FROM inserted_country)),
('Lesignano', (SELECT CountryId FROM inserted_country)),
('Rovereta', (SELECT CountryId FROM inserted_country)),
('Serravalle Dogana', (SELECT CountryId FROM inserted_country)),
('Torre', (SELECT CountryId FROM inserted_country)),
('Montalbo', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));

-- Monaco
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('Monaco', 'MO', '^\+76[0-9]{6,10}$', '+76 ### ### ###') RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('Monaco', (SELECT CountryId FROM inserted_country)),
('Monte Carlo', (SELECT CountryId FROM inserted_country)),
('La Condamine', (SELECT CountryId FROM inserted_country)),
('Fontvieille', (SELECT CountryId FROM inserted_country)),
('Moneghetti', (SELECT CountryId FROM inserted_country)),
('Les Révoires', (SELECT CountryId FROM inserted_country)),
('Larvotto', (SELECT CountryId FROM inserted_country)),
('Saint Michel', (SELECT CountryId FROM inserted_country)),
('La Rousse', (SELECT CountryId FROM inserted_country)),
('Le Portier', (SELECT CountryId FROM inserted_country)),
('Moulins', (SELECT CountryId FROM inserted_country)),
('Sainte-Dévote', (SELECT CountryId FROM inserted_country)),
('Jardin Exotique', (SELECT CountryId FROM inserted_country)),
('Beausoleil', (SELECT CountryId FROM inserted_country)),
('La Colle', (SELECT CountryId FROM inserted_country)),
('La Gare', (SELECT CountryId FROM inserted_country)),
('Les Moneghetti', (SELECT CountryId FROM inserted_country)),
('Condamine', (SELECT CountryId FROM inserted_country)),
('Spélugues', (SELECT CountryId FROM inserted_country)),
('Vallon de la Rousse', (SELECT CountryId FROM inserted_country)),
('Other', (SELECT CountryId FROM inserted_country));
