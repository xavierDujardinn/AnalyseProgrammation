PRAGMA foreign_keys = ON;

CREATE TABLE parties (
  partie_id    INTEGER PRIMARY KEY AUTOINCREMENT,
  nom          TEXT,
  seed         INTEGER,
  statut       TEXT,
  created_at   TEXT,
  updated_at   TEXT
);

CREATE TABLE joueurs (
  joueur_id    INTEGER PRIMARY KEY AUTOINCREMENT,
  partie_id    INTEGER NOT NULL,
  user_id      INTEGER,
  pseudo       TEXT,
  couleur      TEXT,
  ordre_tour   INTEGER,
  actif        INTEGER,
  created_at   TEXT,
  FOREIGN KEY (partie_id) REFERENCES parties(partie_id)
);

CREATE TABLE ressources_ref (
  ressource_id INTEGER PRIMARY KEY AUTOINCREMENT,
  code         TEXT UNIQUE NOT NULL,
  libelle      TEXT,
  unite        TEXT
);

CREATE TABLE joueurs_ressources (
  jr_id         INTEGER PRIMARY KEY AUTOINCREMENT,
  partie_id     INTEGER NOT NULL,
  joueur_id     INTEGER NOT NULL,
  ressource_id  INTEGER NOT NULL,
  qty           REAL,
  cap_max       REAL,
  FOREIGN KEY (partie_id) REFERENCES parties(partie_id),
  FOREIGN KEY (joueur_id) REFERENCES joueurs(joueur_id),
  FOREIGN KEY (ressource_id) REFERENCES ressources_ref(ressource_id)
);
CREATE UNIQUE INDEX idx_joueurs_ress ON joueurs_ressources (partie_id, joueur_id, ressource_id);

CREATE TABLE plateau_etats (
  plateau_id   INTEGER PRIMARY KEY AUTOINCREMENT,
  partie_id    INTEGER NOT NULL,
  tour         INTEGER NOT NULL,
  ascii_state  TEXT,
  checksum     TEXT,
  created_at   TEXT,
  FOREIGN KEY (partie_id) REFERENCES parties(partie_id)
);
CREATE UNIQUE INDEX idx_plateau_etat ON plateau_etats (partie_id, tour);

CREATE TABLE cases (
  case_id      INTEGER PRIMARY KEY AUTOINCREMENT,
  partie_id    INTEGER NOT NULL,
  x            INTEGER NOT NULL,
  y            INTEGER NOT NULL,
  biome        TEXT,
  decouverte   INTEGER,
  FOREIGN KEY (partie_id) REFERENCES parties(partie_id)
);
CREATE UNIQUE INDEX idx_cases ON cases (partie_id, x, y);

CREATE TABLE batiments_ref (
  batiment_id  INTEGER PRIMARY KEY AUTOINCREMENT,
  code         TEXT UNIQUE NOT NULL,
  libelle      TEXT,
  type         TEXT,
  niveau_max   INTEGER
);

CREATE TABLE batiments_niveaux (
  bn_id        INTEGER PRIMARY KEY AUTOINCREMENT,
  batiment_id  INTEGER NOT NULL,
  niveau       INTEGER NOT NULL,
  couts        TEXT,
  prod         TEXT,
  prerequis    TEXT,
  FOREIGN KEY (batiment_id) REFERENCES batiments_ref(batiment_id)
);
CREATE UNIQUE INDEX idx_bat_niveaux ON batiments_niveaux (batiment_id, niveau);

CREATE TABLE joueurs_batiments (
  jb_id        INTEGER PRIMARY KEY AUTOINCREMENT,
  partie_id    INTEGER NOT NULL,
  joueur_id    INTEGER NOT NULL,
  case_id      INTEGER NOT NULL,
  batiment_id  INTEGER NOT NULL,
  niveau       INTEGER,
  etat         TEXT,
  date_construction TEXT,
  FOREIGN KEY (partie_id) REFERENCES parties(partie_id),
  FOREIGN KEY (joueur_id) REFERENCES joueurs(joueur_id),
  FOREIGN KEY (batiment_id) REFERENCES batiments_ref(batiment_id),
  FOREIGN KEY (case_id) REFERENCES cases(case_id)
);
CREATE UNIQUE INDEX idx_jbat ON joueurs_batiments (partie_id, joueur_id, case_id, batiment_id);

CREATE TABLE population (
  population_id INTEGER PRIMARY KEY AUTOINCREMENT,
  partie_id     INTEGER NOT NULL,
  joueur_id     INTEGER NOT NULL,
  total         INTEGER,
  moral         INTEGER,
  besoins       TEXT,
  specialisations TEXT,
  FOREIGN KEY (partie_id) REFERENCES parties(partie_id),
  FOREIGN KEY (joueur_id) REFERENCES joueurs(joueur_id)
);
CREATE UNIQUE INDEX idx_population ON population (partie_id, joueur_id);

CREATE TABLE recherches_ref (
  recherche_id INTEGER PRIMARY KEY AUTOINCREMENT,
  code         TEXT UNIQUE NOT NULL,
  libelle      TEXT,
  prerequis    TEXT,
  effets       TEXT
);

CREATE TABLE joueurs_recherches (
  jr_id        INTEGER PRIMARY KEY AUTOINCREMENT,
  partie_id    INTEGER NOT NULL,
  joueur_id    INTEGER NOT NULL,
  recherche_id INTEGER NOT NULL,
  etat         TEXT,
  progression  REAL,
  FOREIGN KEY (partie_id) REFERENCES parties(partie_id),
  FOREIGN KEY (joueur_id) REFERENCES joueurs(joueur_id),
  FOREIGN KEY (recherche_id) REFERENCES recherches_ref(recherche_id)
);
CREATE UNIQUE INDEX idx_joueurs_recherche ON joueurs_recherches (partie_id, joueur_id, recherche_id);

CREATE TABLE zones (
  zone_id      INTEGER PRIMARY KEY AUTOINCREMENT,
  partie_id    INTEGER NOT NULL,
  coord        TEXT NOT NULL,
  type         TEXT,
  danger       INTEGER,
  decouvert_par INTEGER,
  FOREIGN KEY (partie_id) REFERENCES parties(partie_id),
  FOREIGN KEY (decouvert_par) REFERENCES joueurs(joueur_id)
);
CREATE UNIQUE INDEX idx_zones ON zones (partie_id, coord);

CREATE TABLE explorations (
  exploration_id INTEGER PRIMARY KEY AUTOINCREMENT,
  partie_id      INTEGER NOT NULL,
  joueur_id      INTEGER NOT NULL,
  zone_id        INTEGER NOT NULL,
  statut         TEXT,
  resultats      TEXT,
  created_at     TEXT,
  FOREIGN KEY (partie_id) REFERENCES parties(partie_id),
  FOREIGN KEY (joueur_id) REFERENCES joueurs(joueur_id),
  FOREIGN KEY (zone_id) REFERENCES zones(zone_id)
);
CREATE UNIQUE INDEX idx_explorations ON explorations (partie_id, joueur_id, zone_id);

CREATE TABLE evenements_ref (
  event_id     INTEGER PRIMARY KEY AUTOINCREMENT,
  code         TEXT UNIQUE NOT NULL,
  libelle      TEXT,
  type         TEXT,
  effets       TEXT,
  triggers     TEXT
);

CREATE TABLE evenements_instan (
  eveninst_id  INTEGER PRIMARY KEY AUTOINCREMENT,
  partie_id    INTEGER NOT NULL,
  tour         INTEGER NOT NULL,
  event_id     INTEGER NOT NULL,
  cible_type   TEXT,
  cible_id     INTEGER,
  decision     TEXT,
  resolu       INTEGER,
  created_at   TEXT,
  FOREIGN KEY (partie_id) REFERENCES parties(partie_id),
  FOREIGN KEY (event_id) REFERENCES evenements_ref(event_id)
);
CREATE UNIQUE INDEX idx_eveninstan ON evenements_instan (partie_id, tour, event_id);

CREATE TABLE actions_log (
  log_id      INTEGER PRIMARY KEY AUTOINCREMENT,
  partie_id   INTEGER NOT NULL,
  joueur_id   INTEGER,
  type        TEXT,
  payload     TEXT,
  ts          TEXT,
  FOREIGN KEY (partie_id) REFERENCES parties(partie_id),
  FOREIGN KEY (joueur_id) REFERENCES joueurs(joueur_id)
);

CREATE TABLE sauvegardes (
  sauvegarde_id INTEGER PRIMARY KEY AUTOINCREMENT,
  partie_id     INTEGER NOT NULL,
  slot         INTEGER NOT NULL,
  nom         TEXT,
  tour          INTEGER NOT NULL,
  snapshot      TEXT NOT NULL,
  created_at    TEXT,
  temps        INTEGER,
  FOREIGN KEY (partie_id) REFERENCES parties(partie_id)
);

CREATE TABLE parametres_globaux (
  param_id     INTEGER PRIMARY KEY AUTOINCREMENT,
  partie_id    INTEGER NOT NULL,
  map_width    INTEGER,
  map_height   INTEGER,
  world_seed   INTEGER,
  max_turns    INTEGER,
  starting_resources TEXT,
  max_joueurs INTEGER,
  FOREIGN KEY (partie_id) REFERENCES parties(partie_id)
);
