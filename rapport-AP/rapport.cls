\LoadClass[a4paper,12pt]{article} % Classe du document

%--------------------- Packages ------------------------

\RequirePackage[french]{babel} %Langue du document
\RequirePackage[utf8]{inputenc} %Caractères spéciaux
\RequirePackage[section]{placeins}%Pour placement de section
\RequirePackage[T1]{fontenc} %Quelques lettres qui sont pas inclus dans UTF-8
\RequirePackage{mathtools} %Paquet pour des équations et symboles mathématiques
\RequirePackage{siunitx} %Pour écrire avec la notation scientifique (Ex.: \num{2e+9})
\RequirePackage{float} %Pour placement d'images
\RequirePackage{graphicx} %Paquet pour insérer des images
\RequirePackage[justification=centering]{caption} %Pour les légendes centralisées
\RequirePackage{subcaption}
\RequirePackage{wallpaper}
\RequirePackage{nomencl}
\RequirePackage{xcolor}
\RequirePackage{enumitem}
\RequirePackage{titlesec}
%\makenomenclature
\RequirePackage{fancyhdr}
%\pagestyle{fancy}
%\fancyheadoffset{1cm}
%\setlength{\headheight}{2cm}
\RequirePackage{url}
\RequirePackage[hidelinks]{hyperref}%Paquet pour insérer légendes dans des sous-figures comme Figure 1a, 1b
\RequirePackage[left=2.5cm,right=2.5cm,top=2cm,bottom=3.5cm]{geometry} %Configuration de la page

%-------------------- Définir le nouveau niveau de section ----------------------

\titleformat{\paragraph}
  [hang] % Format de l'entête de section
  {\normalfont\normalsize\bfseries} % Style du texte
  {\theparagraph} % Numérotation
  {1em} % Espacement entre la numérotation et le titre
  {} % Code à exécuter avant le titre
\titlespacing*{\paragraph}
  {0pt} % Indentation de gauche
  {1.5ex plus .2ex minus .2ex} % Espacement avant
  {1em} % Espacement après

\titleformat{\subparagraph}
  [hang] % Format de l'entête de section
  {\normalfont\normalsize\bfseries} % Style du texte
  {\thesubparagraph} % Numérotation
  {1em} % Espacement entre la numérotation et le titre
  {} % Code à exécuter avant le titre
\titlespacing*{\subparagraph}
  {0pt} % Indentation de gauche
  {1.5ex plus .2ex minus .2ex} % Espacement avant
  {1em} % Espacement après

\setcounter{secnumdepth}{5} % Numérotation jusqu'au cinquième niveau
\setcounter{tocdepth}{5} % Inclure jusqu'au cinquième niveau dans la table des matières

\makeatletter
\renewcommand{\theparagraph}{\thesubsubsection.\arabic{paragraph}}
\renewcommand{\thesubparagraph}{\theparagraph.\arabic{subparagraph}}
\makeatother

% Customize the itemize list levels
\setlist[itemize,1]{label=--,left=0pt,labelsep=1em,itemsep=0pt,topsep=0pt}
\setlist[itemize,2]{label=$\circ$,left=2em,labelsep=1em,itemsep=0pt,topsep=0pt}
\setlist[itemize,3]{label=$\ast$,left=4em,labelsep=1em,itemsep=0pt,topsep=0pt}

%-------------------- Informations sur le rapport ----------------------


%-------------------- Informations sur le rapport ----------------------

\newcommand{\cours}[1]{\renewcommand{\cours}{#1}}
\newcommand{\sujet}[1]{\renewcommand{\sujet}{#1}}
\newcommand{\titre}[1]{\renewcommand{\titre}{#1}}
\newcommand{\enseignant}[1]{\renewcommand{\enseignant}{#1}}
\newcommand{\eleves}[1]{\renewcommand{\eleves}{#1}}
\newcommand{\logo}[1]{\renewcommand{\logo}{#1}}
\newcommand{\dep}[1]{\renewcommand{\dep}{#1}}

\newcommand{\fairemarges}{
\makenomenclature
\pagestyle{fancy}
\fancyheadoffset{1cm}
\setlength{\headheight}{1.6cm}
\lhead{\includegraphics[scale=0.25]{\logo}} %Affichage de l'image au top de la page
\rhead{\nouppercase{\leftmark}}
\rfoot{\thepage}
\cfoot{\scriptsize }
\lfoot{\cours}
}

\makeatletter
\renewcommand{\l@section}{%
  \@dottedtocline{1}{1.5em}{2.3em}%
}
\renewcommand{\l@subsection}{%
  \@dottedtocline{2}{3.8em}{3.2em}%
}
\renewcommand{\l@subsubsection}{%
  \@dottedtocline{3}{6.1em}{4.1em}%
}
\makeatother

\newcommand{\fairepagedegarde}{
\begin{titlepage}

	\centering %Centraliser le contenu
	\includegraphics[width=0.3\textwidth]{\logo}\par\vspace{1cm} %Insertion du logo
	{\scshape\LARGE \dep \par} %Nom de l'université
	\vspace{1cm}%Espace de 1,5cm
	{\scshape\Large \cours \\ \sujet \\ \vspace{0.5cm} Rapport\par} %sous-titre
	\vspace{1cm}%Espace de 1cm
    \rule{\linewidth}{0.2 mm} \\[0.4 cm]
	{\huge\bfseries \titre \par} \
    \rule{\linewidth}{0.2 mm} \\[1.5 cm]
	%\vspace{1cm}%Espace de 1cm (retiré sinon pas assez de place pour la date avec bcp de noms) à remettre si peu de noms
    
	\begin{minipage}{0.5\textwidth} %Minipage pour faire deux colonnes
		\begin{flushleft} \large %Envoyer à gauche
		\emph{\textbf{Élèves :}}\\ %Pour le titre au dessus des noms à gauche
        \eleves\\ %Remplacer pour chacun
		\end{flushleft}
	\end{minipage}
	~
	\begin{minipage}{0.4\textwidth}
		\begin{flushright} \large
		\emph{\textbf{Enseignant :}} \\
		 \enseignant \\
		\end{flushright}
	\end{minipage}\\[2.5cm]
    
	\vfill
	{\large \today\par} %Affichage de la date

\end{titlepage}
%\begin{titlepage}


  %\begin{tikzpicture}[remember picture,overlay]
  %  \node[rotate=55, scale=13, text opacity=0.1, color=black]
  %    at (current page.center) {Confidentiel};
  %
%    \end{tikzpicture}
  
  %\vspace*{-1cm}

%	\centering % Centraliser le contenu
%	\includegraphics[width=0.2\textwidth]{\logo}\par\vspace{1cm} % Insertion du logo
%	{\scshape\LARGE Département technique de Namur \par} % Nom du département
%	\vspace{0.5cm} % Espace de 0.5 cm
%	{\scshape\Large Section Informatique\\ (Finalité Sécurité des systèmes) \par} % Section et finalité
%	\vspace{0.5cm} % Espace de 0.5 cm
%	{\scshape Année 2024 - 2025 \par} % Année
%	\vspace{1cm} % Espace de 1 cm
%    \includegraphics[width=0.5\textwidth]{img/surveillance3.png}
%    \vspace{0.7cm} % Espace de 0.7 cm
%    \vspace{0.2cm} % Espace de 1 cm
%   \rule{\linewidth}{0.5mm} \\[0.2 cm]
%	{\color[rgb]{0, 0.4, 0.8}\large\bfseries Analyse et surveillance des réseaux informatiques : une approche pratique et appliquée \par} % Titre
%    \rule{\linewidth}{0.5 mm} \\[1.5 cm]
%	\vspace{1cm} % Espace de 1 cm
	
%    {\Large Monis Lucas \par} % Nom de l'étudiant
	
%	\vfill
	% Affichage de la date
  
%\end{titlepage}
}

\newcommand{\tabledematieres}{
	\clearpage
	\pagestyle{fancy}
    \tableofcontents
    \newpage
	\clearpage
}

\newcommand{\insererfigure}[4]{
\begin{figure}[ht]
\centering
\includegraphics[height=#2]{#1}
\caption{#3}
\label{#4}
\end{figure}
}
