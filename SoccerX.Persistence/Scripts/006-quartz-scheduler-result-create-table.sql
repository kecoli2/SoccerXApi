-- Enum type tanımı (eğer daha önce oluşturulmadıysa)
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'scheduler_result_enum') THEN
        CREATE TYPE scheduler_result_enum AS ENUM ('Ok', 'Error');
    END IF;
END$$;

-- Tablo oluşturma
CREATE TABLE schedulerresult (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    jobkey VARCHAR(255) NOT NULL,
    jobgroup VARCHAR(255) NOT NULL,
    jobdescription TEXT NOT NULL,
    result scheduler_result_enum NOT NULL,
    resultdetail TEXT,
    startdate TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    enddate TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    workingtime INTERVAL NOT NULL,
    userid UUID
);
