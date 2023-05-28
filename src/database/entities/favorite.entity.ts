import { UserEntity } from './user.entity';
import { MapEntity } from './map.entity';
import {
    BelongsTo,
    Column,
    ForeignKey,
    Model,
    Table
} from "sequelize-typescript";
import {InferAttributes, InferCreationAttributes} from "sequelize";

@Table({ modelName: "favorites" })
export class FavoriteEntity extends Model<InferAttributes<FavoriteEntity>, InferCreationAttributes<FavoriteEntity>> {

    @Column
    @ForeignKey(() => MapEntity)
    mapId: number;

    @Column
    @ForeignKey(() => UserEntity)
    userId: number;

    @Column
    public createdAt: Date = new Date();

    @Column
    public updatedAt: Date = new Date();

    @BelongsTo(() => UserEntity)
    user: UserEntity;

    @BelongsTo(() => MapEntity)
    map: MapEntity;
}
