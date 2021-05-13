drop database if exists Velomax;
CREATE DATABASE if not exists Velomax;
use Velomax;
CREATE TABLE `ligneProduit` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `nom` varchar(255) UNIQUE NOT NULL
);
CREATE TABLE `grandeur` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `nom` varchar(255) UNIQUE NOT NULL
);
CREATE TABLE `type` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `nom` varchar(255) UNIQUE NOT NULL
);
CREATE TABLE `velo` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `nom` varchar(255) NOT NULL,
  `prixUnitaire` float NOT NULL,
  `dateIntroduction` datetime NOT NULL,
  `dateDiscontinuation` datetime,
  `grandeurId` int,
  `ligneProduitId` int,
  `quantite` int NOT NULL DEFAULT 0
);
CREATE TABLE `piece` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `reference` varchar(255) NOT NULL,
  `description` varchar(255),
  `prixUnitaire` float NOT NULL,
  `dateIntroduction` datetime NOT NULL,
  `dateDiscontinuation` datetime,
  `typeId` int,
  `quantite` int NOT NULL DEFAULT 0
);
CREATE TABLE `veloPiece` (
  `idVelo` int,
  `idPiece` int,
  `quantite` int NOT NULL DEFAULT 1,
  PRIMARY KEY (`idVelo`, `idPiece`)
);
CREATE TABLE `commandePiece` (
  `idCommande` int,
  `idPiece` int,
  `quantite` int NOT NULL DEFAULT 1,
  PRIMARY KEY (`idCommande`, `idPiece`)
);
CREATE TABLE `commandeVelo` (
  `idCommande` int,
  `idVelo` int,
  `quantite` int NOT NULL DEFAULT 1,
  PRIMARY KEY (`idCommande`, `idVelo`)
);
CREATE TABLE `commande` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `dateCreation` datetime NOT NULL,
  `dateValidation` datetime,
  `dateExpedition` datetime,
  `rueLivraison` varchar(255) NOT NULL,
  `villeLivraison` varchar(255) NOT NULL,
  `codePostalLivraison` varchar(10) NOT NULL,
  `provinceLivraison` varchar(255) NOT NULL,
  `idClient` int,
  `prix` float NOT NULL,
  `statut` varchar(255) NOT NULL DEFAULT "En attente de validation",
  `delai` int NOT NULL DEFAULT 0
);
CREATE TABLE `client` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `estCompagnie` boolean NOT NULL DEFAULT 0,
  `siret` varchar(20),
  `nom` varchar(255) NOT NULL,
  `prenom` varchar(255),
  `mail` varchar(255) UNIQUE NOT NULL,
  `telephone` varchar(20),
  `rue` varchar(255) NOT NULL,
  `ville` varchar(255) NOT NULL,
  `codePostal` int NOT NULL,
  `province` varchar(255) NOT NULL,
  `estAbonne` boolean NOT NULL DEFAULT 0,
  `idAbonnement` int
);
CREATE TABLE `abonnement` (
  `id` int AUTO_INCREMENT,
  `idProgramme` int,
  `dateDebut` datetime NOT NULL,
  `dateFin` datetime NOT NULL,
  PRIMARY KEY (`id`, `idProgramme`)
);
CREATE TABLE `historiqueAbonnement` (
  `idClient` int,
  `idAbonnement` int,
  PRIMARY KEY (`idClient`, `idAbonnement`)
);
CREATE TABLE `programmeFidelio` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `description` varchar(255),
  `prix` float NOT NULL,
  `duree` int NOT NULL,
  `rabais` float NOT NULL
);
CREATE TABLE `fournisseur` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `siret` varchar(20) UNIQUE,
  `nom` varchar(255) NOT NULL,
  `nomContact` varchar(255) NOT NULL,
  `prenomContact` varchar(255) NOT NULL,
  `mailContact` varchar(255) NOT NULL,
  `rue` varchar(255) NOT NULL,
  `ville` varchar(255) NOT NULL,
  `codePostal` varchar(255) NOT NULL,
  `province` varchar(255) NOT NULL,
  `libelle` int
);
CREATE TABLE `fournisseurPiece` (
  `idFournisseur` int,
  `idPiece` int,
  `delai` int NOT NULL DEFAULT 0,
  `quantite` int NOT NULL DEFAULT 0,
  `noProduitFournisseur` int NOT NULL,
  PRIMARY KEY (`idFournisseur`, `idPiece`)
);
ALTER TABLE
  `velo`
ADD
  FOREIGN KEY (`grandeurId`) REFERENCES `grandeur` (`id`);
ALTER TABLE
  `velo`
ADD
  FOREIGN KEY (`ligneProduitId`) REFERENCES `ligneProduit` (`id`);
ALTER TABLE
  `piece`
ADD
  FOREIGN KEY (`typeId`) REFERENCES `type` (`id`);
ALTER TABLE
  `veloPiece`
ADD
  FOREIGN KEY (`idVelo`) REFERENCES `velo` (`id`);
ALTER TABLE
  `veloPiece`
ADD
  FOREIGN KEY (`idPiece`) REFERENCES `piece` (`id`);
ALTER TABLE
  `commandePiece`
ADD
  FOREIGN KEY (`idCommande`) REFERENCES `commande` (`id`);
ALTER TABLE
  `commandePiece`
ADD
  FOREIGN KEY (`idPiece`) REFERENCES `piece` (`id`);
ALTER TABLE
  `commandeVelo`
ADD
  FOREIGN KEY (`idCommande`) REFERENCES `commande` (`id`);
ALTER TABLE
  `commandeVelo`
ADD
  FOREIGN KEY (`idVelo`) REFERENCES `velo` (`id`);
ALTER TABLE
  `commande`
ADD
  FOREIGN KEY (`idClient`) REFERENCES `client` (`id`);
ALTER TABLE
  `client`
ADD
  FOREIGN KEY (`idAbonnement`) REFERENCES `abonnement` (`id`);
ALTER TABLE
  `abonnement`
ADD
  FOREIGN KEY (`idProgramme`) REFERENCES `programmeFidelio` (`id`);
ALTER TABLE
  `historiqueAbonnement`
ADD
  FOREIGN KEY (`idClient`) REFERENCES `client` (`id`);
ALTER TABLE
  `historiqueAbonnement`
ADD
  FOREIGN KEY (`idAbonnement`) REFERENCES `abonnement` (`id`);
ALTER TABLE
  `fournisseurPiece`
ADD
  FOREIGN KEY (`idFournisseur`) REFERENCES `fournisseur` (`id`);
ALTER TABLE
  `fournisseurPiece`
ADD
  FOREIGN KEY (`idPiece`) REFERENCES `piece` (`id`);
ALTER TABLE
  `piece`
ADD
  CONSTRAINT UNIQUE_REFERENCE UNIQUE(`reference`, `typeId`)
  
  
INSERT INTO
  `programmeFidelio` (`description`, `prix`, `duree`, `rabais`)
VALUES
  ('Fidélio', 15, 1, 0.05);
INSERT INTO
  `programmeFidelio` (`description`, `prix`, `duree`, `rabais`)
VALUES
  ('Fidélio Or', 25, 2, 0.08);
INSERT INTO
  `programmeFidelio` (`description`, `prix`, `duree`, `rabais`)
VALUES
  ('Fidélio Platine', 60, 2, 0.1);
INSERT INTO
  `programmeFidelio` (`description`, `prix`, `duree`, `rabais`)
VALUES
  ('Fidélio Max', 100, 3, 0.12);


-- Grandeur
INSERT INTO
  `grandeur` (`nom`)
VALUES
  ('Adultes');
INSERT INTO
  `grandeur` (`nom`)
VALUES
  ('Jeunes');
INSERT INTO
  `grandeur` (`nom`)
VALUES
  ('Hommes');
INSERT INTO
  `grandeur` (`nom`)
VALUES
  ('Dames');
INSERT INTO
  `grandeur` (`nom`)
VALUES
  ('Garçons');
INSERT INTO
  `grandeur` (`nom`)
VALUES
  ('Filles');


-- LigneProduit
INSERT INTO
  `ligneProduit` (`nom`)
VALUES
  ('VTT');
INSERT INTO
  `ligneProduit` (`nom`)
VALUES
  ('Vélo de course');
INSERT INTO
  `ligneProduit` (`nom`)
VALUES
  ('Classique');
INSERT INTO
  `ligneProduit` (`nom`)
VALUES
  ('BMX');
-- Type
INSERT INTO
  `type` (`nom`)
VALUES
  ('Cadre');
INSERT INTO
  `type` (`nom`)
VALUES
  ('Guidon');
INSERT INTO
  `type` (`nom`)
VALUES
  ('Freins');
INSERT INTO
  `type` (`nom`)
VALUES
  ('Selle');
INSERT INTO
  `type` (`nom`)
VALUES
  ('Dérailleur Avant');
INSERT INTO
  `type` (`nom`)
VALUES
  ('Dérailleur Arrière');
INSERT INTO
  `type` (`nom`)
VALUES
  ('Roue avant');
INSERT INTO
  `type` (`nom`)
VALUES
  ('Roue arrière ');
INSERT INTO
  `type` (`nom`)
VALUES
  ('Réflecteurs');
INSERT INTO
  `type` (`nom`)
VALUES
  ('Pédalier');
INSERT INTO
  `type` (`nom`)
VALUES
  ('Ordinateur');
INSERT INTO
  `type` (`nom`)
VALUES
  ('Panier');
-- Velo
INSERT INTO
  `velo` (
    `nom`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `grandeurId`,
    `ligneProduitId`,
    `quantite`
  )
VALUES
  (
    'Kilimandjaro',
    569,
    CURRENT_TIMESTAMP,
    null,
    1,
    1,
    0
  );
INSERT INTO
  `velo` (
    `nom`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `grandeurId`,
    `ligneProduitId`,
    `quantite`
  )
VALUES
  (
    'NorthPole',
    329,
    CURRENT_TIMESTAMP,
    null,
    1,
    1,
    0
  );
INSERT INTO
  `velo` (
    `nom`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `grandeurId`,
    `ligneProduitId`,
    `quantite`
  )
VALUES
  (
    'MontBlanc',
    399,
    CURRENT_TIMESTAMP,
    null,
    2,
    1,
    0
  );
INSERT INTO
  `velo` (
    `nom`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `grandeurId`,
    `ligneProduitId`,
    `quantite`
  )
VALUES
  (
    'Hooligan',
    199,
    CURRENT_TIMESTAMP,
    null,
    2,
    1,
    0
  );
INSERT INTO
  `velo` (
    `nom`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `grandeurId`,
    `ligneProduitId`,
    `quantite`
  )
VALUES
  ('Orléans', 229, CURRENT_TIMESTAMP, null, 3, 2, 0);
INSERT INTO
  `velo` (
    `nom`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `grandeurId`,
    `ligneProduitId`,
    `quantite`
  )
VALUES
  ('Orléans', 229, CURRENT_TIMESTAMP, null, 4, 2, 0);
INSERT INTO
  `velo` (
    `nom`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `grandeurId`,
    `ligneProduitId`,
    `quantite`
  )
VALUES
  ('BlueJay', 349, CURRENT_TIMESTAMP, null, 3, 2, 0);
INSERT INTO
  `velo` (
    `nom`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `grandeurId`,
    `ligneProduitId`,
    `quantite`
  )
VALUES
  ('BlueJay', 349, CURRENT_TIMESTAMP, null, 4, 2, 0);
INSERT INTO
  `velo` (
    `nom`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `grandeurId`,
    `ligneProduitId`,
    `quantite`
  )
VALUES
  (
    'Trail Explorer',
    129,
    CURRENT_TIMESTAMP,
    null,
    5,
    3,
    0
  );
INSERT INTO
  `velo` (
    `nom`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `grandeurId`,
    `ligneProduitId`,
    `quantite`
  )
VALUES
  (
    'Trail Explorer',
    129,
    CURRENT_TIMESTAMP,
    null,
    6,
    3,
    0
  );
INSERT INTO
  `velo` (
    `nom`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `grandeurId`,
    `ligneProduitId`,
    `quantite`
  )
VALUES
  (
    'Night Hawk',
    189,
    CURRENT_TIMESTAMP,
    null,
    2,
    3,
    0
  );
INSERT INTO
  `velo` (
    `nom`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `grandeurId`,
    `ligneProduitId`,
    `quantite`
  )
VALUES
  (
    'Tierra Verde',
    199,
    CURRENT_TIMESTAMP,
    null,
    3,
    3,
    0
  );
INSERT INTO
  `velo` (
    `nom`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `grandeurId`,
    `ligneProduitId`,
    `quantite`
  )
VALUES
  (
    'Tierra Verde',
    199,
    CURRENT_TIMESTAMP,
    null,
    4,
    3,
    0
  );
INSERT INTO
  `velo` (
    `nom`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `grandeurId`,
    `ligneProduitId`,
    `quantite`
  )
VALUES
  (
    'Mud Zinger I',
    279,
    CURRENT_TIMESTAMP,
    null,
    2,
    4,
    0
  );
INSERT INTO
  `velo` (
    `nom`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `grandeurId`,
    `ligneProduitId`,
    `quantite`
  )
VALUES
  (
    'Mud Zinger II',
    359,
    CURRENT_TIMESTAMP,
    null,
    1,
    4,
    0
  );
-- Piece
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('C32', '', 10, CURRENT_TIMESTAMP, null, 1, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('C34', '', 10, CURRENT_TIMESTAMP, null, 1, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('C76', '', 10, CURRENT_TIMESTAMP, null, 1, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('C43', '', 10, CURRENT_TIMESTAMP, null, 1, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('C44f', '', 10, CURRENT_TIMESTAMP, null, 1, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('C43f', '', 10, CURRENT_TIMESTAMP, null, 1, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('C01', '', 10, CURRENT_TIMESTAMP, null, 1, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('C02', '', 10, CURRENT_TIMESTAMP, null, 1, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('C15', '', 10, CURRENT_TIMESTAMP, null, 1, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('C87', '', 10, CURRENT_TIMESTAMP, null, 1, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('C87f', '', 10, CURRENT_TIMESTAMP, null, 1, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('C25', '', 10, CURRENT_TIMESTAMP, null, 1, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('C26', '', 10, CURRENT_TIMESTAMP, null, 1, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('G7', '', 10, CURRENT_TIMESTAMP, null, 2, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('G9', '', 10, CURRENT_TIMESTAMP, null, 2, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('G12', '', 10, CURRENT_TIMESTAMP, null, 2, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('F3', '', 10, CURRENT_TIMESTAMP, null, 3, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('F9', '', 10, CURRENT_TIMESTAMP, null, 3, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('S88', '', 10, CURRENT_TIMESTAMP, null, 4, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('S37', '', 10, CURRENT_TIMESTAMP, null, 4, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('S35', '', 10, CURRENT_TIMESTAMP, null, 4, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('S02', '', 10, CURRENT_TIMESTAMP, null, 4, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('S03', '', 10, CURRENT_TIMESTAMP, null, 4, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('S36', '', 10, CURRENT_TIMESTAMP, null, 4, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('S34', '', 10, CURRENT_TIMESTAMP, null, 4, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('S87', '', 10, CURRENT_TIMESTAMP, null, 4, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('DV133', '', 10, CURRENT_TIMESTAMP, null, 5, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('DV17', '', 10, CURRENT_TIMESTAMP, null, 5, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('DV87', '', 10, CURRENT_TIMESTAMP, null, 5, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('DV57', '', 10, CURRENT_TIMESTAMP, null, 5, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('DV15', '', 10, CURRENT_TIMESTAMP, null, 5, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('DV41', '', 10, CURRENT_TIMESTAMP, null, 5, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('DV132', '', 10, CURRENT_TIMESTAMP, null, 5, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('DR56', '', 10, CURRENT_TIMESTAMP, null, 6, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('DR87', '', 10, CURRENT_TIMESTAMP, null, 6, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('DR86', '', 10, CURRENT_TIMESTAMP, null, 6, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('DR23', '', 10, CURRENT_TIMESTAMP, null, 6, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('DR76', '', 10, CURRENT_TIMESTAMP, null, 6, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('DR52', '', 10, CURRENT_TIMESTAMP, null, 6, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('R45', '', 10, CURRENT_TIMESTAMP, null, 7, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('R48', '', 10, CURRENT_TIMESTAMP, null, 7, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('R12', '', 10, CURRENT_TIMESTAMP, null, 7, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('R19', '', 10, CURRENT_TIMESTAMP, null, 7, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('R1', '', 10, CURRENT_TIMESTAMP, null, 7, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('R11', '', 10, CURRENT_TIMESTAMP, null, 7, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('R44', '', 10, CURRENT_TIMESTAMP, null, 7, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('R46', '', 10, CURRENT_TIMESTAMP, null, 8, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('R47', '', 10, CURRENT_TIMESTAMP, null, 8, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('R32', '', 10, CURRENT_TIMESTAMP, null, 8, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('R18', '', 10, CURRENT_TIMESTAMP, null, 8, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('R2', '', 10, CURRENT_TIMESTAMP, null, 8, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('R12', '', 10, CURRENT_TIMESTAMP, null, 8, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('R02', '', 10, CURRENT_TIMESTAMP, null, 9, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('R09', '', 10, CURRENT_TIMESTAMP, null, 9, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('R10', '', 10, CURRENT_TIMESTAMP, null, 9, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('P12', '', 10, CURRENT_TIMESTAMP, null, 10, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('P34', '', 10, CURRENT_TIMESTAMP, null, 10, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('P1', '', 10, CURRENT_TIMESTAMP, null, 10, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('P15', '', 10, CURRENT_TIMESTAMP, null, 10, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('O2', '', 10, CURRENT_TIMESTAMP, null, 11, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('O4', '', 10, CURRENT_TIMESTAMP, null, 11, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('S01', '', 10, CURRENT_TIMESTAMP, null, 12, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('S05', '', 10, CURRENT_TIMESTAMP, null, 12, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('S74', '', 10, CURRENT_TIMESTAMP, null, 12, 0);
INSERT INTO
  `piece` (
    `reference`,
    `description`,
    `prixUnitaire`,
    `dateIntroduction`,
    `dateDiscontinuation`,
    `typeId`,
    `quantite`
  )
VALUES
  ('S73', '', 10, CURRENT_TIMESTAMP, null, 12, 0);


-- VeloPiece
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (1, 1, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (2, 2, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (3, 3, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (3, 4, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (4, 5, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (5, 6, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (4, 7, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (6, 8, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (8, 9, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (7, 10, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (9, 11, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (10, 12, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (11, 13, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (12, 14, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (13, 15, 1);

INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (14, 1, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (14, 2, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (14, 3, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (14, 4, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (15, 5, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (15, 6, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (15, 7, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (15, 8, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (16, 9, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (16, 10, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (16, 11, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (16, 12, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (16, 13, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (14, 14, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (14, 15, 1);

INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (17, 1, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (17, 2, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (17, 3, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (17, 4, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (18, 5, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (18, 6, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (18, 7, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (18, 8, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (18, 11, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (18, 12, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (17, 13, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (17, 14, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (17, 15, 1);



INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (19, 1, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (19, 2, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (19, 3, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (19, 4, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (20, 5, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (21, 6, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (20, 7, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (21, 8, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (23, 9, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (22, 10, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (24, 11, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (24, 12, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (25, 13, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (26, 14, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (26, 15, 1);



INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (27, 1, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (28, 2, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (28, 3, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (29, 4, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (30, 5, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (30, 6, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (30, 7, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (30, 8, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (31, 11, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (32, 12, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (32, 13, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (33, 14, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (27, 15, 1);



INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (34, 1, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (35, 2, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (35, 3, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (36, 4, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (36, 5, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (36, 6, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (35, 7, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (35, 8, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (37, 11, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (38, 12, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (38, 13, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (39, 14, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (39, 15, 1);




INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (40, 1, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (41, 2, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (41, 3, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (42, 4, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (43, 5, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (43, 6, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (43, 7, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (43, 8, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (44, 9, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (44, 10, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (45, 11, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (45, 12, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (45, 13, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (46, 14, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (46, 15, 1);



INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (47, 1, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (48, 2, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (48, 3, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (49, 4, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (50, 5, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (50, 6, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (50, 7, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (50, 8, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (51, 9, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (51, 10, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (52, 11, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (52, 12, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (52, 13, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (48, 14, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (48, 15, 1);



INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (53, 5, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (53, 6, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (53, 7, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (53, 8, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (54, 9, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (54, 10, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (55, 11, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (55, 12, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (55, 13, 1);




INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (56, 1, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (56, 2, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (56, 3, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (56, 4, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (57, 5, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (57, 6, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (57, 7, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (57, 8, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (58, 9, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (58, 10, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (59, 11, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (59, 12, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (59, 13, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (56, 14, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (56, 15, 1);



INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (60, 1, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (60, 3, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (61, 7, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (61, 8, 1);


INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (62, 9, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (63, 10, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (64, 11, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (64, 12, 1);
INSERT INTO
  `veloPiece` (`idPiece`, `idVelo`, `quantite`)
VALUES
  (65, 13, 1);


-- Abonnement
INSERT INTO
  `abonnement` (`idProgramme`, `dateDebut`, `dateFin`)
VALUES
  (
    4,
    '2020-06-15 :00:00:00',
    '2023-06-15 :00:00:00'
  );
INSERT INTO
  `abonnement` (`idProgramme`, `dateDebut`, `dateFin`)
VALUES
  (
    3,
    '2020-01-01 :00:00:00',
    '2022-01-01 :00:00:00'
  );
  INSERT INTO
  `abonnement` (`idProgramme`, `dateDebut`, `dateFin`)
VALUES
  (
    2,
    '2020-01-01 :00:00:00',
    '2022-01-01 :00:00:00'
  );
INSERT INTO
  `abonnement` (
    `idProgramme`,
    `dateDebut`,
    `dateFin`
  )
VALUES
  (
    1,
    '2021-05-07 :00:00:00',
    '2022-05-07 :00:00:00'
  );
-- Client
INSERT INTO
  `client` (
    `estCompagnie`,
    `siret`,
    `nom`,
    `prenom`,
    `mail`,
    `telephone`,
    `rue`,
    `ville`,
    `codePostal`,
    `province`,
    `estAbonne`,
    `idAbonnement`
  )
VALUES
  (
    false,
    null,
    "Gabison",
    "Yoan",
    "yoan.gabison@edu.devinci.fr",
    "0665700736",
    "48 rue des blancs champs",
    "Bagnolet",
    "93170",
    "France",
    true,
    1
  );
INSERT INTO
  `client` (
    `estCompagnie`,
    `siret`,
    `nom`,
    `prenom`,
    `mail`,
    `telephone`,
    `rue`,
    `ville`,
    `codePostal`,
    `province`,
    `estAbonne`,
    `idAbonnement`
  )
VALUES
  (
    false,
    null,
    "Goffinon",
    "Clément",
    "clement.goffinon@edu.devinci.fr",
    "0648980738",
    "86ter avenue de Rigny",
    "Bry-sur-Marne",
    "99015",
    "France",
    true,
    2
  );
INSERT INTO
  `client` (
    `estCompagnie`,
    `siret`,
    `nom`,
    `prenom`,
    `mail`,
    `telephone`,
    `rue`,
    `ville`,
    `codePostal`,
    `province`,
    `estAbonne`,
    `idAbonnement`
  )
VALUES
  (
    false,
    null,
    "Lancrenon",
    "Thomas",
    "thomas.lancrenon@edu.devinci.fr",
    "0658485908",
    "12 rue Paul Eluard",
    "Charenton",
    "94220",
    "France",
    true,
    3
  );
INSERT INTO
  `client` (
    `estCompagnie`,
    `siret`,
    `nom`,
    `prenom`,
    `mail`,
    `telephone`,
    `rue`,
    `ville`,
    `codePostal`,
    `province`,
    `estAbonne`
  )
VALUES
  (
    true,
    40285022600018,
    "ESILV",
    null,
    "esilv@devinci.fr",
    "0122334455",
    "12 avenue Léonard de Vinci",
    "Courbevoie",
    "92400",
    "France",
    false
  );
INSERT INTO
  `client` (
    `estCompagnie`,
    `siret`,
    `nom`,
    `prenom`,
    `mail`,
    `telephone`,
    `rue`,
    `ville`,
    `codePostal`,
    `province`,
    `estAbonne`,
    `idAbonnement`
  )
VALUES
  (
    false,
    null,
    "Lahlou",
    "Abdelkrim",
    "abdelkrim.lahlou@edu.devinci.fr",
    "0626785688",
    "1 avenue Léonard de Vinci",
    "Courbevoie",
    "92400",
    "France",
    true,
    4
  );
INSERT INTO
  `client` (
    `estCompagnie`,
    `siret`,
    `nom`,
    `prenom`,
    `mail`,
    `telephone`,
    `rue`,
    `ville`,
    `codePostal`,
    `province`,
    `estAbonne`
  )
VALUES
  (
    false,
    null,
    "Safwan",
    "Chendeb",
    "safwan.chendeb@edu.devinci.fr",
    "0629795989",
    "2 avenue Léonard de Vinci",
    "Courbevoie",
    "92400",
    "France",
    false
  );



  INSERT INTO
  `commande` (
    `dateCreation`,
    `dateValidation`,
    `dateExpedition`,
    `rueLivraison`,
    `villeLivraison`,
    `codePostalLivraison`,
    `provinceLivraison`,
    `idClient`,
    `prix`,
    `statut`,
    `delai`
  )
VALUES
  (
    '2021-05-07 00:00:00',
    '2021-06-10 00:00:00',
    '2021-06-15 00:00:00',
    '5 Avenue Anatole France',
    'Paris',
    '75007',
    'Île-de-France',
    1,
    139,
    'livré',
    2
  );

  INSERT INTO
  `commande` (
    `dateCreation`,
    `dateValidation`,
    `dateExpedition`,
    `rueLivraison`,
    `villeLivraison`,
    `codePostalLivraison`,
    `provinceLivraison`,
    `idClient`,
    `prix`,
    `statut`,
    `delai`
  )
VALUES
  (
    '2022-01-17 00:00:00',
    '2022-02-11 00:00:00',
    '2022-02-20 00:00:00',
    '99 rue Zinedine Zidane',
    'Paris',
    '75027',
    'Île-de-France',
    2,
    19,
    'en attente',
    1
  );

  INSERT INTO
  `commande` (
    `dateCreation`,
    `dateValidation`,
    `dateExpedition`,
    `rueLivraison`,
    `villeLivraison`,
    `codePostalLivraison`,
    `provinceLivraison`,
    `idClient`,
    `prix`,
    `statut`,
    `delai`
  )
VALUES
  (
    '2021-05-26 00:00:00',
    '2021-06-30 00:00:00',
    '2021-07-03 00:00:00',
    '6 avenue Charles de Gaulle',
    'Paris',
    '75117',
    'Île-de-France',
    1,
    289,
    'en attente',
    5
  );

INSERT INTO
`commandePiece`(
  `idCommande`,
  `idPiece`,
  `quantite`
)
VALUES
(
  1,
  1,
  13
);

INSERT INTO
`commandePiece`(
  `idCommande`,
  `idPiece`,
  `quantite`
)
VALUES
(
  1,
  19,
  13
);

INSERT INTO
`commandePiece`(
  `idCommande`,
  `idPiece`,
  `quantite`
)
VALUES
(
  2,
  10,
  4
);

INSERT INTO
`commandePiece`(
  `idCommande`,
  `idPiece`,
  `quantite`
)
VALUES
(
  3,
  23,
  18
);

INSERT INTO
`commandeVelo`(
  `idCommande`,
  `idVelo`,
  `quantite`
)
VALUES
(
  1,
  1,
  1
);

INSERT INTO
`commandeVelo`(
  `idCommande`,
  `idVelo`,
  `quantite`
)
VALUES
(
  2,
  3,
  7
);

INSERT INTO
`commandeVelo`(
  `idCommande`,
  `idVelo`,
  `quantite`
)
VALUES
(
  2,
  2,
  6
);

INSERT INTO
`commandeVelo`(
  `idCommande`,
  `idVelo`,
  `quantite`
)
VALUES
(
  3,
  1,
  13
);

INSERT INTO
`commandeVelo`(
  `idCommande`,
  `idVelo`,
  `quantite`
)
VALUES
(
  1,
  15,
  2
);

-- fournisseur 
INSERT INTO
`fournisseur`(
  `siret`,
  `nom`,
  `nomContact`,
  `prenomContact`,
  `mailContact`,
  `rue`,
  `ville`,
  `codePostal`,
  `province`,
  `libelle`
)
VALUES
(
  12345678912345,
  'VeloRide',
  'Maurice',
  'Gautier',
  'maurice.gautier@gmail.fr',
  '3 rue des cerfs',
  'Angers',
  49100,
  'Maine-et-Loire',
  2
);

INSERT INTO
`fournisseur`(
  `siret`,
  `nom`,
  `nomContact`,
  `prenomContact`,
  `mailContact`,
  `rue`,
  `ville`,
  `codePostal`,
  `province`,
  `libelle`
)
VALUES
(
  12398753722991,
  'VC',
  'Jeanne',
  'Bonaparte',
  'jeanne.bonaparte@gmail.fr',
  '45 rue de la paix',
  'Strasbourg',
  67200,
  'Bas-Rhin',
  1
);

INSERT INTO
`fournisseur`(
  `siret`,
  `nom`,
  `nomContact`,
  `prenomContact`,
  `mailContact`,
  `rue`,
  `ville`,
  `codePostal`,
  `province`,
  `libelle`
)
VALUES
(
  82342170912444,
  'RideAway',
  'Philibert',
  'Bourges',
  'philibert.bourges@gmail.fr',
  '22 avenue de la réussite',
  'Boulognes',
  92100,
  'Haut-de-France',
  3
);

-- fournisseur pièce
INSERT INTO
`fournisseurPiece`(
  `idFournisseur`,
  `idPiece`,
  `delai`,
  `quantite`,
  `noProduitFournisseur`
)
VALUES
(
  1,
  11,
  8,
  23,
  4
);

INSERT INTO
`fournisseurPiece`(
  `idFournisseur`,
  `idPiece`,
  `delai`,
  `quantite`,
  `noProduitFournisseur`
)
VALUES
(
  2,
  34,
  2,
  50,
  2
);

INSERT INTO
`fournisseurPiece`(
  `idFournisseur`,
  `idPiece`,
  `delai`,
  `quantite`,
  `noProduitFournisseur`
)
VALUES
(
  2,
  7,
  30,
  9,
  4
);

INSERT INTO
`fournisseurPiece`(
  `idFournisseur`,
  `idPiece`,
  `delai`,
  `quantite`,
  `noProduitFournisseur`
)
VALUES
(
  3,
  45,
  1,
  60,
  3
);

INSERT INTO
`fournisseurPiece`(
  `idFournisseur`,
  `idPiece`,
  `delai`,
  `quantite`,
  `noProduitFournisseur`
)
VALUES
(
  3,
  2,
  7,
  14,
  2
);

INSERT INTO
`fournisseurPiece`(
  `idFournisseur`,
  `idPiece`,
  `delai`,
  `quantite`,
  `noProduitFournisseur`
)
VALUES
(
  1,
  23,
  5,
  30,
  1
);

INSERT INTO
`fournisseurPiece`(
  `idFournisseur`,
  `idPiece`,
  `delai`,
  `quantite`,
  `noProduitFournisseur`
)
VALUES
(
  1,
  19,
  4,
  71,
  3
);

INSERT INTO
`fournisseurPiece`(
  `idFournisseur`,
  `idPiece`,
  `delai`,
  `quantite`,
  `noProduitFournisseur`
)
VALUES
(
  1,
  36,
  5,
  5,
  5
);

INSERT INTO
`fournisseurPiece`(
  `idFournisseur`,
  `idPiece`,
  `delai`,
  `quantite`,
  `noProduitFournisseur`
)
VALUES
(
  1,
  26,
  16,
  3,
  2
);

INSERT INTO
`fournisseurPiece`(
  `idFournisseur`,
  `idPiece`,
  `delai`,
  `quantite`,
  `noProduitFournisseur`
)
VALUES
(
  3,
  41,
  18,
  9,
  1
);

INSERT INTO
`fournisseurPiece`(
  `idFournisseur`,
  `idPiece`,
  `delai`,
  `quantite`,
  `noProduitFournisseur`
)
VALUES
(
  1,
  44,
  4,
  40,
  3
);

INSERT INTO
`fournisseurPiece`(
  `idFournisseur`,
  `idPiece`,
  `delai`,
  `quantite`,
  `noProduitFournisseur`
)
VALUES
(
  2,
  10,
  8,
  22,
  4
);

INSERT INTO
`fournisseurPiece`(
  `idFournisseur`,
  `idPiece`,
  `delai`,
  `quantite`,
  `noProduitFournisseur`
)
VALUES
(
  2,
  1,
  1,
  100,
  4
);

-- historique abonnement
INSERT INTO
`historiqueAbonnement`(
  `idClient`,
  `idAbonnement`
)
VALUES
(
  2,
  4
);

INSERT INTO
`historiqueAbonnement`(
  `idClient`,
  `idAbonnement`
)
VALUES
(
  1,
  4
);

INSERT INTO
`historiqueAbonnement`(
  `idClient`,
  `idAbonnement`
)
VALUES
(
  3,
  2
);

INSERT INTO
`historiqueAbonnement`(
  `idClient`,
  `idAbonnement`
)
VALUES
(
  4,
  1
);

INSERT INTO
`historiqueAbonnement`(
  `idClient`,
  `idAbonnement`
)
VALUES
(
  6,
  3
);

INSERT INTO
`historiqueAbonnement`(
  `idClient`,
  `idAbonnement`
)
VALUES
(
  5,
  1
);