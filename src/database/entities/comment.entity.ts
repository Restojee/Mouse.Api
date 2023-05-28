import { UserEntity } from './user.entity';
import { MapEntity } from './map.entity';
import {
  BelongsTo,
  Column,
  ForeignKey,
  Table,
  Model
} from "sequelize-typescript";
import {
  InferAttributes,
  InferCreationAttributes
} from "sequelize";

@Table({ modelName: "comments" })
export class CommentEntity extends Model<InferAttributes<CommentEntity>, InferCreationAttributes<CommentEntity>> {

  @Column
  text: string;

  @Column
  @ForeignKey(() => UserEntity)
  userId: number;

  @Column
  @ForeignKey(() => MapEntity)
  mapId: number;

  @Column
  public createdAt: Date = new Date();

  @Column
  public updatedAt: Date = new Date();

  @BelongsTo(() => UserEntity)
  user: UserEntity;

  @BelongsTo(() => MapEntity)
  map: MapEntity;
}