CREATE OR REPLACE FUNCTION set_updated_date()
RETURNS TRIGGER AS $$
BEGIN
   NEW.updatedate := NOW();
   RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_set_updated_date_countries
BEFORE UPDATE ON countries
FOR EACH ROW
EXECUTE FUNCTION set_updated_date();

CREATE TRIGGER trg_set_updated_date_cities
BEFORE UPDATE ON cities
FOR EACH ROW
EXECUTE FUNCTION set_updated_date();

CREATE TRIGGER trg_set_updated_date_users
BEFORE UPDATE ON users
FOR EACH ROW
EXECUTE FUNCTION set_updated_date();

CREATE TRIGGER trg_set_updated_date_transactions
BEFORE UPDATE ON transactions
FOR EACH ROW
EXECUTE FUNCTION set_updated_date();

CREATE TRIGGER trg_set_updated_date_comments
BEFORE UPDATE ON comments
FOR EACH ROW
EXECUTE FUNCTION set_updated_date();

CREATE TRIGGER trg_set_updated_date_likes
BEFORE UPDATE ON likes
FOR EACH ROW
EXECUTE FUNCTION set_updated_date();

CREATE TRIGGER trg_set_updated_date_teams
BEFORE UPDATE ON teams
FOR EACH ROW
EXECUTE FUNCTION set_updated_date();

CREATE TRIGGER trg_set_updated_date_notifications
BEFORE UPDATE ON notifications
FOR EACH ROW
EXECUTE FUNCTION set_updated_date();

CREATE TRIGGER trg_set_updated_date_subscriptions
BEFORE UPDATE ON subscriptions
FOR EACH ROW
EXECUTE FUNCTION set_updated_date();

CREATE TRIGGER trg_set_updated_date_payments
BEFORE UPDATE ON payments
FOR EACH ROW
EXECUTE FUNCTION set_updated_date();

CREATE TRIGGER trg_set_updated_date_auditlog
BEFORE UPDATE ON auditlog
FOR EACH ROW
EXECUTE FUNCTION set_updated_date();

CREATE OR REPLACE FUNCTION increment_rowversion()
  RETURNS TRIGGER AS $$
BEGIN
  NEW.rowversion := OLD.rowversion + 1;
  RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_users_rowversion
  BEFORE UPDATE ON users
  FOR EACH ROW
  EXECUTE FUNCTION increment_rowversion();