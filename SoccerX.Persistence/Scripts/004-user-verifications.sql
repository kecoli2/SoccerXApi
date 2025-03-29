ALTER TABLE public.users
ADD COLUMN IsEmailConfirmed BOOLEAN DEFAULT FALSE;

CREATE TABLE public.emailverifications (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    userid UUID NOT NULL,
    token VARCHAR(255) NOT NULL,
    expiresat TIMESTAMP NOT NULL,
    isused BOOLEAN DEFAULT FALSE,
    createdate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_user_emailverification FOREIGN KEY (userid) REFERENCES public.users(id) ON DELETE CASCADE
);
