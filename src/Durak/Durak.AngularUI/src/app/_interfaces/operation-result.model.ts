export interface OperationResult {
  ok: boolean;
  result: any;
  metadata: Metadata;
}

export interface Metadata {
  message: string;
}
