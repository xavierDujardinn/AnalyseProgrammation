-- fichier sql pour sqlite

PRAGMA foreign_keys = ON;  -- important pour activer les FK [web:3][web:2]

CREATE TABLE IF NOT EXISTS roles (
  role TEXT PRIMARY KEY
);

CREATE TABLE IF NOT EXISTS users (
  username TEXT PRIMARY KEY,
  role     TEXT NOT NULL,
  status   TEXT,
  FOREIGN KEY (role) REFERENCES roles(role)
    ON UPDATE CASCADE
    ON DELETE RESTRICT
);

CREATE TABLE IF NOT EXISTS roles_permissions (
  role       TEXT NOT NULL,
  permission TEXT NOT NULL,
  PRIMARY KEY (role, permission),
  FOREIGN KEY (role) REFERENCES roles(role)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS idx_users_role ON users(role);
CREATE INDEX IF NOT EXISTS idx_roles_permissions_role ON roles_permissions(role);
