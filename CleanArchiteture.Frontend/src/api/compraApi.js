import { createRepositoryClient } from "./createRepositoryClient";

const client = createRepositoryClient("compras");

export const compraApi = {
  getByIdAsync: (id, cancellationToken) => client.getByIdAsync(id, cancellationToken),
  getAllAsync: (cancellationToken) => client.getAllAsync(cancellationToken),
  addAsync: (entity, cancellationToken) => client.addAsync(entity, cancellationToken),
  updateAsync: (id, entity, cancellationToken) => client.updateAsync(id, entity, cancellationToken),
  deleteAsync: (id, cancellationToken) => client.deleteAsync(id, cancellationToken),
  existsAsync: (id, cancellationToken) => client.existsAsync(id, cancellationToken)
};
