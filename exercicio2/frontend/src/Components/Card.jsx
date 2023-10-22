import "./Card.css";

import { Link } from "react-router-dom";

const Card = ({ usuario }) => {
  return (
    <div id="card">
      <h3>{usuario.nome}</h3>
      <Link to={`/pagamento/${usuario.id}`}>Pagamentos</Link>
    </div>
  );
};

export default Card;
