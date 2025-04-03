ALTER TABLE public.emailverifications
DROP COLUMN token;

ALTER TABLE public.emailverifications
ADD COLUMN code VARCHAR(6) NOT NULL;