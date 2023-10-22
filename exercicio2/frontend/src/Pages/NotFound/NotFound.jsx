import "./NotFound.css";

import { Link } from "react-router-dom";

const NotFound = () => {
  return (
    <div id="notFound">
      <h1>Este link não existe</h1>
      <Link to="/">Voltar para página inicial</Link>
    </div>
  );
};

export default NotFound;
